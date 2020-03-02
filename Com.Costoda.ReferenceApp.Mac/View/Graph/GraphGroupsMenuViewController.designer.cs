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
	[Register ("GraphGroupsMenuViewController")]
	partial class GraphGroupsMenuViewController
	{
		[Outlet]
		AppKit.NSOutlineView ovGroups { get; set; }

		[Action ("btnDeletePressed:")]
		partial void btnDeletePressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ovGroups != null) {
				ovGroups.Dispose ();
				ovGroups = null;
			}
		}
	}
}
