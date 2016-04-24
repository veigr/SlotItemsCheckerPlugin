using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;

namespace SlotItemsCheckerPlugin.Models
{
	public class SlotItemState
	{
		public SlotItemEquipType Master { get; }
		public int SlotItemCount { get; }
		public int UnsetslotCount { get; }
		public int EquippedCount { get; }
		public bool IsValid => this.SlotItemCount == this.UnsetslotCount + this.EquippedCount;
		
		public SlotItemState(SlotItemEquipType master, int slotItemCount, int unsetslotCount, int equippedCount)
		{
			this.Master = master;
			this.SlotItemCount = slotItemCount;
			this.UnsetslotCount = unsetslotCount;
			this.EquippedCount = equippedCount;
		}
	}
}
