using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using MetroTrilithon.Mvvm;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace SlotItemsCheckerPlugin.Models
{
	public class UnsetSlotItems : MetroTrilithon.Mvvm.Notifier
	{
		public IDictionary<SlotItemEquipType, int[]> Items { get; private set; }
		
		internal void UpdateItems(dynamic rawdata)
		{
			var master = KanColleClient.Current.Master.SlotItemEquipTypes.ToArray();
			this.Items = master.ToDictionary(
				x => x.Value,
				x =>
				{
					var unsetslot = rawdata[$"api_slottype{x.Key}"] as JArray;
					if (unsetslot == null || !unsetslot.HasValues) return new int[0];
					return unsetslot.Select(t => t.ToObject<int>()).ToArray();
				});
			this.RaisePropertyChanged(nameof(this.Items));
		}
	}
}
