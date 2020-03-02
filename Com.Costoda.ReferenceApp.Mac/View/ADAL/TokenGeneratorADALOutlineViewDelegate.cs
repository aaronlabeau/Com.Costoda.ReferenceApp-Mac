using System;
using System.Collections.Generic;
using System.Linq;

using AppKit;
using Foundation;

using Com.Costoda.ReferenceApp.Mac.Authentication;
using Com.Costoda.ReferenceApp.CoreLib.Messenging;

namespace Com.Costoda.ReferenceApp.Mac.View.ADAL
{
    public class TokenGeneratorADALOutlineViewDelegate
        : NSOutlineViewDelegate
    {
        private const string _cellIdentifier = "TokenGenEndpointCell";
        private const string _nameColumn = "Name";
        private const string _environmentColumn = "Environment";

        public TokenGeneratorADALOutlineViewDelegate()
        {
        }

        [Export("outlineView:viewForTableColumn:item:")]
        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var view = (NSTextField)outlineView.MakeView(_cellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField
                {
                    Identifier = _cellIdentifier,
                    BackgroundColor = NSColor.Clear,
                    Bordered = false,
                    Selectable = false,
                    Editable = false
                };
            }
            var endpoint = item as MacEndpoint;
            if (endpoint.IsGroup)
            {
				view.StringValue = endpoint.Title;
            }
            else
            {
				view.StringValue = endpoint.Name;
            }
            return view;
        }

        [Export("outlineView:shouldSelectItem:")]
        public override bool ShouldSelectItem(
			NSOutlineView outlineView, 
			NSObject item)
        {
            var endpoint = item as MacEndpoint;
            if (endpoint.IsGroup)
            {
                return false;
			}
            else 
			{
                //send message
                Messaging.Instance.Publish(Messages.ChangeEndpointSelection, endpoint);
                return true;
			}
        }
    }

    public class TokenGeneratorADALOutlineViewDataSource
        : NSOutlineViewDataSource
    {
        public IList<MacEndpoint> CurrentEndpoints = new List<MacEndpoint>();

        [Export("outlineView:numberOfChildrenOfItem:")]
        public override nint GetChildrenCount(
			NSOutlineView outlineView, 
			NSObject item)
        {
			if (item == null)
            {
                return CurrentEndpoints.Count();
			}
            else
			{
                return ((MacEndpoint)item).Endpoints.Count;
			}
        }

        [Export("outlineView:child:ofItem:")]
        public override NSObject GetChild(
			NSOutlineView outlineView, 
			nint childIndex, 
			NSObject item)
        {
			if (item == null)
            {
                return CurrentEndpoints[(int)childIndex];
			}
            else
            {
                return ((MacEndpoint)item).Endpoints.ElementAt((int)childIndex);
			}
        }

        [Export("outlineView:isItemExpandable:")]
        public override bool ItemExpandable(NSOutlineView outlineView, NSObject item)
        {
            if (item == null)
            {
                return CurrentEndpoints[0].IsGroup;
			}
            else 
			{
                return ((MacEndpoint)item).IsGroup;
			}
        }
    }
}
