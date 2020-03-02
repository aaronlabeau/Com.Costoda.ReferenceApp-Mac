// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Com.Costoda.ReferenceApp.Mac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		AppKit.NSSearchField searchField { get; set; }

		[Action ("authPressed:")]
		partial void authPressed (Foundation.NSObject sender);

		[Action ("groupsPressed:")]
		partial void groupsPressed (Foundation.NSObject sender);

		[Action ("tasksPressed:")]
		partial void tasksPressed (Foundation.NSObject sender);

		[Action ("userPressed:")]
		partial void userPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (searchField != null) {
				searchField.Dispose ();
				searchField = null;
			}
		}
	}
}
