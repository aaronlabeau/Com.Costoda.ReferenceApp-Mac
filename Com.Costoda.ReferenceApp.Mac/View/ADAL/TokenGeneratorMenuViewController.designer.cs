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
	[Register ("TokenGeneratorMenuViewController")]
	partial class TokenGeneratorMenuViewController
	{
		[Outlet]
		AppKit.NSView containerView { get; set; }

		[Outlet]
		AppKit.NSOutlineView ovEnvironments { get; set; }

		[Action ("trashPressed:")]
		partial void trashPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (ovEnvironments != null) {
				ovEnvironments.Dispose ();
				ovEnvironments = null;
			}
		}
	}
}
