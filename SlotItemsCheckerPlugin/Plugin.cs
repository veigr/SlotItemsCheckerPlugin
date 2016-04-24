using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotItemsCheckerPlugin
{
	[Export(typeof(IPlugin))]
	[Export(typeof(ITool))]
	[ExportMetadata("Title", "SlotItemsChecker")]
	[ExportMetadata("Description", "ユーザーが所持している装備の状態を確認するプラグインです。")]
	[ExportMetadata("Version", "1.0.0")]
	[ExportMetadata("Author", "@veigr")]
	[ExportMetadata("Guid", "F020794F-ED3E-4681-8226-625F3B0A3437")]
	public class Plugin : IPlugin, ITool
	{
		private static ViewModels.SlotItemsCheckerViewModel vm;

		public string Name => "SlotItemsChecker";

		public object View => new Views.SlotItemsCheckerView { DataContext = vm };

		public void Initialize()
		{
			vm = new ViewModels.SlotItemsCheckerViewModel();
		}
	}
}
