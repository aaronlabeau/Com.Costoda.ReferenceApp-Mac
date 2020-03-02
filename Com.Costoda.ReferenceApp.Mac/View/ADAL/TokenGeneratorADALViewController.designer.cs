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
	[Register ("TokenGeneratorADALViewController")]
	partial class TokenGeneratorADALViewController
	{
		[Outlet]
		AppKit.NSButton btnShowHideSecureText { get; set; }

		[Outlet]
		AppKit.NSComboBox cbEnvironments { get; set; }

		[Outlet]
		AppKit.NSOutlineView ovJtwToken { get; set; }

		[Outlet]
		AppKit.NSSearchField sfKeychainGroup { get; set; }

		[Outlet]
		AppKit.NSSecureTextField stfApplicationId { get; set; }

		[Outlet]
		AppKit.NSSecureTextField stfAuthority { get; set; }

		[Outlet]
		AppKit.NSSecureTextField stfRedirectUri { get; set; }

		[Outlet]
		AppKit.NSSecureTextField stfResourceId { get; set; }

		[Outlet]
		AppKit.NSTabView tabView { get; set; }

		[Outlet]
		AppKit.NSTextField tfApplicationId { get; set; }

		[Outlet]
		AppKit.NSTextField tfAuthority { get; set; }

		[Outlet]
		AppKit.NSTextField tfExtraParameters { get; set; }

		[Outlet]
		AppKit.NSTextField tfName { get; set; }

		[Outlet]
		AppKit.NSTextField tfRedirectUri { get; set; }

		[Outlet]
		AppKit.NSTextField tfResourceId { get; set; }

		[Outlet]
		AppKit.NSTextField tfStatus { get; set; }

		[Outlet]
		AppKit.NSTableHeaderView thvJwtToken { get; set; }

		[Outlet]
		AppKit.NSTableHeaderView tvhKeyChain { get; set; }

		[Outlet]
		AppKit.NSTableView tvKeychain { get; set; }

		[Outlet]
		AppKit.NSTextView tvPiiLogs { get; set; }

		[Outlet]
		AppKit.NSTextView tvRawToken { get; set; }

		[Action ("ClearForm:")]
		partial void ClearForm (Foundation.NSObject sender);

		[Action ("ClearLogs:")]
		partial void ClearLogs (Foundation.NSObject sender);

		[Action ("ClearTokens:")]
		partial void ClearTokens (Foundation.NSObject sender);

		[Action ("GetToken:")]
		partial void GetToken (Foundation.NSObject sender);

		[Action ("KeychainSearchGroup:")]
		partial void KeychainSearchGroup (Foundation.NSObject sender);

		[Action ("SaveForm:")]
		partial void SaveForm (Foundation.NSObject sender);

		[Action ("ShowHideSecureText:")]
		partial void ShowHideSecureText (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cbEnvironments != null) {
				cbEnvironments.Dispose ();
				cbEnvironments = null;
			}

			if (btnShowHideSecureText != null) {
				btnShowHideSecureText.Dispose ();
				btnShowHideSecureText = null;
			}

			if (ovJtwToken != null) {
				ovJtwToken.Dispose ();
				ovJtwToken = null;
			}

			if (sfKeychainGroup != null) {
				sfKeychainGroup.Dispose ();
				sfKeychainGroup = null;
			}

			if (stfApplicationId != null) {
				stfApplicationId.Dispose ();
				stfApplicationId = null;
			}

			if (stfAuthority != null) {
				stfAuthority.Dispose ();
				stfAuthority = null;
			}

			if (stfRedirectUri != null) {
				stfRedirectUri.Dispose ();
				stfRedirectUri = null;
			}

			if (stfResourceId != null) {
				stfResourceId.Dispose ();
				stfResourceId = null;
			}

			if (tabView != null) {
				tabView.Dispose ();
				tabView = null;
			}

			if (tfApplicationId != null) {
				tfApplicationId.Dispose ();
				tfApplicationId = null;
			}

			if (tfAuthority != null) {
				tfAuthority.Dispose ();
				tfAuthority = null;
			}

			if (tfExtraParameters != null) {
				tfExtraParameters.Dispose ();
				tfExtraParameters = null;
			}

			if (tfName != null) {
				tfName.Dispose ();
				tfName = null;
			}

			if (tfRedirectUri != null) {
				tfRedirectUri.Dispose ();
				tfRedirectUri = null;
			}

			if (tfResourceId != null) {
				tfResourceId.Dispose ();
				tfResourceId = null;
			}

			if (tfStatus != null) {
				tfStatus.Dispose ();
				tfStatus = null;
			}

			if (thvJwtToken != null) {
				thvJwtToken.Dispose ();
				thvJwtToken = null;
			}

			if (tvhKeyChain != null) {
				tvhKeyChain.Dispose ();
				tvhKeyChain = null;
			}

			if (tvKeychain != null) {
				tvKeychain.Dispose ();
				tvKeychain = null;
			}

			if (tvPiiLogs != null) {
				tvPiiLogs.Dispose ();
				tvPiiLogs = null;
			}

			if (tvRawToken != null) {
				tvRawToken.Dispose ();
				tvRawToken = null;
			}
		}
	}
}
