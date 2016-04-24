using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using SlotItemsCheckerPlugin.Models;
using MetroTrilithon.Mvvm;
using Grabacr07.KanColleWrapper;

namespace SlotItemsCheckerPlugin.ViewModels
{
	public class SlotItemsCheckerViewModel : ViewModel
	{
		public SlotItemStateViewModel[] States { get; private set; }

		public int SlotItemsTotalCount => KanColleClient.Current.Homeport.Itemyard.SlotItemsCount;

		public int UnsetslotBasedTotalCount => SlotItemsChecker.Instance.EquippedPlusUnsetslotCount;

		public SlotItemsCheckerViewModel()
		{
			SlotItemsChecker.Instance.Subscribe(
				nameof(SlotItemsChecker.SlotItemStates),
				this.UpdateStates,
				false);
		}

		private void UpdateStates()
		{
			this.States = SlotItemsChecker.Instance.SlotItemStates.Select(x => new SlotItemStateViewModel(x)).ToArray();
			this.RaisePropertyChanged(nameof(this.States));
		}
	}
}
