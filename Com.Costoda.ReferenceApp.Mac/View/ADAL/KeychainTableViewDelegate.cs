using System;
using System.Collections;
using System.Collections.Generic;
using AppKit;
using Foundation;
using Security;

namespace Com.Costoda.ReferenceApp.Mac.View.ADAL
{
    public class KeychainTableViewDelegate : 
		NSTableViewDelegate
    {
        private const string CellIdentifier = "KeychainCell";

        private const string ServiceColumn = "Service";
        private const string AccountColumn = "Account";
        private const string CreationDateColumn = "Creation Date";
        private const string AccessGroupColumn = "Access Group";

        private KeychainTableViewDataSource _dataSource; 

        public KeychainTableViewDelegate(KeychainTableViewDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [Export("tableView:viewForTableColumn:row:")]
        public override NSView GetViewForItem(
			NSTableView tableView, 
			NSTableColumn tableColumn, 
			nint row)
        {
            NSTextField view = null;
            if (tableView != null && tableColumn != null)
            { 
				view = tableView.MakeView(CellIdentifier, this) as NSTextField;
                if (view == null)
                {
                    view = new NSTextField
                    {
                        Identifier = CellIdentifier,
						BackgroundColor = NSColor.Clear,
						Bordered = false,
						Selectable = false,
						Editable = false
					};
				}

                //set the value based on the column
                switch (tableColumn.Title)
                {
                    case ServiceColumn:
                        view.StringValue = _dataSource.KeychainValues[(int)row].Service;
                        break;
                    case AccountColumn:
                        view.StringValue = _dataSource.KeychainValues[(int)row].Account;
                        break;
                    case CreationDateColumn: 
                        view.StringValue = _dataSource.KeychainValues[(int)row].CreationDate.ToString();
                        break;
					default:
                        view.StringValue = _dataSource.KeychainValues[(int)row].AccessGroup;
                        break;
				}
			}
            return view;
        }

    }

    public class KeychainTableViewDataSource :
        NSTableViewDataSource
    {
        public IList<SecRecord> KeychainValues { get; set; } 

		public KeychainTableViewDataSource()
        {
            KeychainValues = new List<SecRecord>();
		}

        [Export("numberOfRowsInTableView:")]
        public override nint GetRowCount(NSTableView tableView)
        {
            return KeychainValues.Count;
        }
    }
}
