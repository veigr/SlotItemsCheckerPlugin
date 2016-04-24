using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;
using System.Reactive.Linq;
using MetroTrilithon.Mvvm;
using Newtonsoft.Json.Linq;

namespace SlotItemsCheckerPlugin.Models
{
	public class SlotItemsChecker : MetroTrilithon.Mvvm.Notifier
	{
		public static SlotItemsChecker Instance { get; } = new SlotItemsChecker();

		public SlotItemState[] SlotItemStates { get; private set; }

		private UnsetSlotItems UnsetSlotItems { get; } = new UnsetSlotItems();

		public int EquippedPlusUnsetslotCount { get; private set; }

		private SlotItemsChecker()
		{
			KanColleClient.Current.Subscribe(nameof(KanColleClient.IsStarted), () =>
			{
				KanColleClient.Current.Homeport.Organization
					.Subscribe(nameof(Organization.Ships), this.Update, false);
			}, false);

			KanColleClient.Current.Proxy
				.api_get_member_require_info
				.Select(s => JObject.Parse(s.Response.BodyAsString.Substring(7)) as dynamic)
				.Subscribe(s => this.UnsetSlotItems.UpdateItems(s.api_data.api_unsetslot));
		}

		private void Update()
		{
			if (this.SlotItemStates != null) return;

			var userItems = KanColleClient.Current.Homeport.Itemyard.SlotItems
				.GroupBy(x => x.Value.Info.EquipType);
			var ships = KanColleClient.Current.Homeport.Organization.Ships;
			var equippedItems = ships
				.SelectMany(s => s.Value.Slots)
				.Concat(ships.Select(s => s.Value.ExSlot))
				.Where(s => s.Equipped)
				.Select(s => s.Item)
				.GroupBy(x => x.Info.EquipType);
			this.SlotItemStates = KanColleClient.Current.Master.SlotItemEquipTypes
				.Select(x => new SlotItemState(
					x.Value,
					userItems.FirstOrDefault(g => g.Key == x.Value)?.Count() ?? 0,
					this.UnsetSlotItems.Items[x.Value].Count(),
					equippedItems.FirstOrDefault(g => g.Key == x.Value)?.Count() ?? 0
				)).ToArray();
			this.EquippedPlusUnsetslotCount = equippedItems.SelectMany(x => x).Count()
				+ this.UnsetSlotItems.Items.SelectMany(x => x.Value).Count();

			this.RaisePropertyChanged(nameof(this.SlotItemStates));
			this.RaisePropertyChanged(nameof(this.EquippedPlusUnsetslotCount));
		}
	}
}
