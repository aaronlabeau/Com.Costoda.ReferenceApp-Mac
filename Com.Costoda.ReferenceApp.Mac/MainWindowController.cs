using System;

using Foundation;
using AppKit;
using Com.Costoda.ReferenceApp.CoreLib.Messenging;

namespace Com.Costoda.ReferenceApp.Mac
{
	public partial class MainWindowController 
		: NSWindowController
	{
        public MainWindowController (IntPtr handle) : base (handle) 
		{
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        partial void authPressed(NSObject sender)
        {
            Messaging.Instance.Publish(Messages.ChangeViewControllerAuthentication);
        }

        partial void groupsPressed(NSObject sender)
        {
            Messaging.Instance.Publish(Messages.ChangeViewControllerGroups);
        }

        partial void tasksPressed(NSObject sender)
        {
            Messaging.Instance.Publish(Messages.ChangeViewControllerSharePoint);
        }

        partial void userPressed(NSObject sender)
        {
            Messaging.Instance.Publish(Messages.ChangeViewControllerUserProfile);
        }
    }
}
