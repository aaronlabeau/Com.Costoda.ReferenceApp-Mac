using System;
using System.Linq;

using System.Runtime.CompilerServices;

using AppKit;
using Foundation;

using Com.Costoda.ReferenceApp.CoreLib.Messenging;

namespace Com.Costoda.ReferenceApp.Mac
{
    public partial class SplitViewController
        : NSSplitViewController
    {
        private const string LOADUSERPROFILEMESSAGEKEY = nameof(LOADUSERPROFILEMESSAGEKEY);
        private Action LoadUserProfileToken;

        private const string LOADGROUPSMESSAGEKEY = nameof(LOADGROUPSMESSAGEKEY);
        private Action LoadGroupsToken;

        private const string LOADAUTHMESSAGEKEY = nameof(LOADAUTHMESSAGEKEY);
        private Action LoadAuthToken;

        private const string LOADSHAREPOINTMESSAGEKEY = nameof(LOADSHAREPOINTMESSAGEKEY);
        private Action LoadSharePointToken;


        public SplitViewController(IntPtr handle)
        : base(handle)
        {
            SetupMessagingQueue();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        private void SetupMessagingQueue()
        {
            //map tokens to methods
            LoadUserProfileToken = SwitchViewControllerUserProfile;
            LoadAuthToken = SwitchViewControllerAuth;
            LoadGroupsToken = SwitchViewControllerGroups;
            LoadSharePointToken = SwitchViewControllerSharePoint;

            //subscribe to messages
            Messaging.Instance.Subscribe(
                Messages.ChangeViewControllerAuthentication,
                LOADAUTHMESSAGEKEY,
                LoadAuthToken);

            Messaging.Instance.Subscribe(
                Messages.ChangeViewControllerGroups,
                LOADGROUPSMESSAGEKEY,
                LoadGroupsToken);

            Messaging.Instance.Subscribe(
                Messages.ChangeViewControllerUserProfile,
                LOADUSERPROFILEMESSAGEKEY,
                LoadUserProfileToken);

            Messaging.Instance.Subscribe(
                Messages.ChangeViewControllerSharePoint,
                LOADSHAREPOINTMESSAGEKEY,
                LoadSharePointToken);

        }
        private void SwitchViewControllerGroups()
        {
            SwapSplitViewItem(0, GetViewControllerByKey(GraphGroupsMenuViewController.IdentifierKey));
            SwapSplitViewItem(1, GetViewControllerByKey(GraphGroupsViewController.IdentifierKey));
        }

        private void SwitchViewControllerUserProfile()
        {
            SwapSplitViewItem(0, GetViewControllerByKey(GraphUserProfileMasterViewController.IdentifierKey));
            SwapSplitViewItem(1, GetViewControllerByKey(GraphUserProfileViewController.IdentifierKey));
        }

        private void SwitchViewControllerAuth()
        {
            SwapSplitViewItem(0, GetViewControllerByKey(TokenGeneratorMenuViewController.IdentifierKey));
            SwapSplitViewItem(1, GetViewControllerByKey(TokenGeneratorADALViewController.IdentifierKey));

        }
        private void SwitchViewControllerSharePoint() 
        {
            SwapSplitViewItem(0, GetViewControllerByKey(GraphProjectMenuViewController.IdentifierKey));

            SwapSplitViewItem(1, GetViewControllerByKey(SharepointProjectViewController.IdentifierKey));

		}
        private NSViewController GetViewControllerByKey(string key)
        {
            NSViewController vc = null;

            if (this.SplitViewItems.Any() && this.SplitViewItems.Length == 2)
            {
				vc = this.Storyboard.InstantiateControllerWithIdentifier(key) 
					as NSViewController;
            }
            return vc; 
        }

        private void SwapSplitViewItem(int position, NSViewController vc)
        {
            try
            {
				if (vc != null && position < 2)
				{
                    var sv = this.SplitViewItems[position];
                    if (sv != null)
                    {
                        var splitView = new NSSplitViewItem { ViewController = vc };

                        this.InsertSplitViewItem(splitView, position);
                        this.RemoveSplitViewItem(sv);

                        //clean up memory
                        sv.Dispose();
                        sv = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} \n\nStack Trace: {ex.StackTrace}");
            }
        }
    }
}
