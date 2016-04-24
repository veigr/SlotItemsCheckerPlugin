using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;
using Livet;
using SlotItemsCheckerPlugin.Models;

namespace SlotItemsCheckerPlugin.ViewModels
{
	public class SlotItemStateViewModel : ViewModel
	{
		private readonly SlotItemState state;
		public SlotItemEquipType Master => this.state.Master;
		public int SlotItemCount => this.state.SlotItemCount;
		public int UnsetslotCount => this.state.UnsetslotCount;
		public int EquippedCount => this.state.EquippedCount;
		public bool IsValid => this.state.IsValid;
		public bool IsInvalid => !this.state.IsValid;

		public SlotItemStateViewModel(SlotItemState state)
		{
			this.state = state;
		}
	}
}
