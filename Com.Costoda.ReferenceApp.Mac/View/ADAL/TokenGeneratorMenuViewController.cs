
using System;

using Foundation;
using AppKit;
using Com.Costoda.ReferenceApp.CoreLib.Messenging;
using Com.Costoda.ReferenceApp.Mac.Authentication;
using Com.Costoda.ReferenceApp.Mac.View.ADAL;

namespace Com.Costoda.ReferenceApp.Mac
{
	public partial class TokenGeneratorMenuViewController
		: NSViewController
	{

		public static string IdentifierKey = "TokenGeneratorMenuViewController";

		private const string DESELECTENDPOINTMESSAGEKEY = nameof(DESELECTENDPOINTMESSAGEKEY);
		private Action DeselectEndpointToken;

		private const string SAVEENDPOINTMESSAGEKEY = nameof(SAVEENDPOINTMESSAGEKEY);
		private Action<MacEndpoint> SaveEndpointToken;

		public TokenGeneratorMenuViewController(IntPtr handle) 
		: base (handle)
		{
			DeselectEndpointToken = DeselectEndpoint;
			SaveEndpointToken = SaveEndpoint;

			Messaging.Instance.Subscribe(
				Messages.DeselectEndpoint,
				DESELECTENDPOINTMESSAGEKEY,
				DeselectEndpointToken);

			Messaging.Instance.Subscribe(
				Messages.SaveMacendpoint,
				SAVEENDPOINTMESSAGEKEY,
				SaveEndpointToken);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ovEnvironments.DataSource = new TokenGeneratorADALOutlineViewDataSource() { CurrentEndpoints = MacEndpointService.Instance.CurrentEndpoints };
			ovEnvironments.Delegate = new TokenGeneratorADALOutlineViewDelegate();
		}

		private void SaveEndpoint(MacEndpoint macEndpoint)
		{
			MacEndpointService.Instance.AddEndpoint(macEndpoint);
			ovEnvironments.ReloadData();
		}

		private void DeselectEndpoint()
		{
			ovEnvironments.DeselectAll(this);
		}


		partial void trashPressed(NSObject sender)
        {
        }
    }
}
