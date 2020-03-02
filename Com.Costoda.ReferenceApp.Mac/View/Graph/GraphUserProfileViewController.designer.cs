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
	[Register ("GraphUserProfileViewController")]
	partial class GraphUserProfileViewController
	{
		[Outlet]
		AppKit.NSTextField tfBio { get; set; }

		[Outlet]
		AppKit.NSTextField tfDepartment { get; set; }

		[Outlet]
		AppKit.NSTextField tfName { get; set; }

		[Outlet]
		AppKit.NSTextField tfTitle { get; set; }

		[Outlet]
		AppKit.NSImageView userPicture { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tfDepartment != null) {
				tfDepartment.Dispose ();
				tfDepartment = null;
			}

			if (tfName != null) {
				tfName.Dispose ();
				tfName = null;
			}

			if (tfTitle != null) {
				tfTitle.Dispose ();
				tfTitle = null;
			}

			if (userPicture != null) {
				userPicture.Dispose ();
				userPicture = null;
			}

			if (tfBio != null) {
				tfBio.Dispose ();
				tfBio = null;
			}
		}
	}
}
