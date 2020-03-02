using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using AppKit;

using Com.Costoda.ReferenceApp.CoreLib.Authentication;
using Com.Costoda.ReferenceApp.Mac.View.ADAL;
using Com.Costoda.ReferenceApp.Mac.Keychain;
using Com.Costoda.ReferenceApp.Mac.Authentication;
using Com.Costoda.ReferenceApp.CoreLib.Messenging;

namespace Com.Costoda.ReferenceApp.Mac
{
	public partial class TokenGeneratorADALViewController 
		: NSViewController, 
	      INSTabViewDelegate
	{
        private const string ENDPOINTSELECTIONMESSAGEKEY = nameof(ENDPOINTSELECTIONMESSAGEKEY);
        private Action<MacEndpoint> SetEndpointSelectedToken;

        private bool _isSecureText;
        private string _errorLog;

        //private CacheToken _currentToken;
        private MacEndpoint _currentEndpoint;

		public static string IdentifierKey = "TokenGeneratorADALViewController";

        private KeychainTableViewDataSource _keychainTvDataSource;

		public TokenGeneratorADALViewController (IntPtr handle) : base (handle) 
		{
            //subscribe to messages
            SetEndpointSelectedToken = SetSelectedEndpoint;

            //handle loading endpoint from NSOutlineView
            Messaging.Instance.Subscribe(
                Messages.ChangeEndpointSelection,
                ENDPOINTSELECTIONMESSAGEKEY,
                SetEndpointSelectedToken);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _isSecureText = true;
            _errorLog = string.Empty;

            //hide real text fields by default
            this.tfApplicationId.Hidden = true;
            this.tfAuthority.Hidden = true;
            this.tfRedirectUri.Hidden = true;
            this.tfResourceId.Hidden = true;

            tabView.Delegate = this;

            tvKeychain.DataSource = _keychainTvDataSource;
            tvKeychain.Delegate = new KeychainTableViewDelegate(_keychainTvDataSource);

            cbEnvironments.UsesDataSource = true;
            cbEnvironments.DataSource = new ComboBoxDataSource();
        }

        [Export("tabView:didSelectTabViewItem:")]
		public void DidSelect(NSTabView tabView, NSTabViewItem item)
		{
            if (tabView != null && item != null)
            { 
                switch (item.Label)
                {
                    case "Logs":
                        {
                            tvPiiLogs.Value = string.Empty;
                            tvPiiLogs.Value = _errorLog;
						}
                        break;
                    case "Raw Token":
                        {
                            tvRawToken.Value = string.Empty;
                            //tvRawToken.Value = (_currentToken != null) ? _currentToken.Token : string.Empty;
						}
                        break;
					case "Token":
                        { 
						}
                        break;
                    case "Cache":
                        { 
						}
                        break;
					case "Keychain":
                        { 
                            if (_currentEndpoint != null)
                            {
                                //_keychainTvDataSource.KeychainValues = KeychainService.Instance.GetRecordsFromKeychain($"{_currentEndpoint.TeamId}.{_currentEndpoint.KeychainSecurityGroup}");
                                //tvKeychain.ReloadData();
							}
						}
                        break;
                    default:
                        { 
                            //auth window - do nothing
						}
                        break;
                }
			}
		}

        partial void SaveForm(NSObject sender)
        {
            try
            {
                if (_currentEndpoint == null ||
                    _currentEndpoint?.Environment == null &&
                    !string.IsNullOrEmpty(tfName.StringValue))
                {
                    //var selectedEnvironment = cbEnvironments.SelectedValue;
                    //if (selectedEnvironment != null)
                    //{
                        MacEndpoint newEndpoint = new MacEndpoint()
                        {
                            Environment = CoreLib.Authentication.Environment.Dev 
                        };
                        newEndpoint.Name = tfName.StringValue;
                        newEndpoint.ExtraParameters = tfExtraParameters.StringValue;

                        if (_isSecureText)
                        {
                            newEndpoint.ClientId = stfApplicationId.StringValue;
                            newEndpoint.Authority = stfAuthority.StringValue;
                            newEndpoint.EnableLogging = true;
                            newEndpoint.RedirectUri = stfRedirectUri.StringValue;
                            newEndpoint.ResourceId = stfResourceId.StringValue;
                        }
                        else
                        {
                            newEndpoint.ClientId = tfApplicationId.StringValue;
                            newEndpoint.Authority = tfAuthority.StringValue;
                            newEndpoint.EnableLogging = true;
                            newEndpoint.RedirectUri = tfRedirectUri.StringValue;
                            newEndpoint.ResourceId = tfResourceId.StringValue;
                        }

                        Messaging.Instance.Publish<MacEndpoint>(Messages.SaveMacendpoint, newEndpoint);

						_currentEndpoint = newEndpoint;
                    }
                    else
                    {
                        //todo update existing endpoint
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message} Stacktrace: {ex.StackTrace}");
			}
		}

        private void SetSelectedEndpoint(MacEndpoint item)
        {
            _currentEndpoint = item;
            //TODO update textfields
            if (_isSecureText)
            {
                stfApplicationId.StringValue = item.ClientId;
                stfAuthority.StringValue = item.Authority;
                stfRedirectUri.StringValue = item.RedirectUri;
                stfResourceId.StringValue = item.ResourceId;
                tfExtraParameters.StringValue = item.ExtraParameters;
            }
            else 
			{

                tfApplicationId.StringValue = item.ClientId;
                tfAuthority.StringValue = item.Authority;
                tfRedirectUri.StringValue = item.RedirectUri;
                tfResourceId.StringValue = item.ResourceId; 
            }

            var index = MacEndpointService.Instance.GetEnvironments().IndexOf(_currentEndpoint.Title);
            cbEnvironments.SelectItem(index);
            tfName.StringValue = item.Name;
            tfExtraParameters.StringValue = item.ExtraParameters;
		}

        partial void KeychainSearchGroup(NSObject sender)
        {
            if (!string.IsNullOrEmpty(sfKeychainGroup.StringValue))
            {
                _keychainTvDataSource.KeychainValues = KeychainService.Instance.GetRecordsFromKeychain(sfKeychainGroup.StringValue);
                tvKeychain.ReloadData();
            }
        }

        partial void ClearForm(NSObject sender)
        {
            this.tfName.StringValue = string.Empty;
            this.stfApplicationId.StringValue = string.Empty;
            this.tfApplicationId.StringValue = string.Empty;
            this.stfAuthority.StringValue = string.Empty;
            this.tfAuthority.StringValue = string.Empty;
            this.tfExtraParameters.StringValue = string.Empty;
            this.stfRedirectUri.StringValue = string.Empty;
            this.stfResourceId.StringValue = string.Empty;
            this.tfName.StringValue = string.Empty;
            this.tfRedirectUri.StringValue = string.Empty;
            this.tfResourceId.StringValue = string.Empty;

            _currentEndpoint = null;

            Messaging.Instance.Publish(Messages.DeselectEndpoint);
        }

        partial void ShowHideSecureText(NSObject sender)
        {
            _isSecureText = !_isSecureText;

            if (_isSecureText)
            {
                //set secure text field values
                stfApplicationId.StringValue = tfApplicationId.StringValue;
                stfAuthority.StringValue = tfAuthority.StringValue;
                stfRedirectUri.StringValue = tfRedirectUri.StringValue;
                stfResourceId.StringValue = tfResourceId.StringValue;

                //toggle inscure fields
                tfApplicationId.Hidden = true;
                tfAuthority.Hidden = true;
                tfRedirectUri.Hidden = true;
                tfResourceId.Hidden = true;

                stfApplicationId.Hidden = false;
                stfAuthority.Hidden = false;
                stfRedirectUri.Hidden = false;
                stfResourceId.Hidden = false;

                tfApplicationId.StringValue = string.Empty;
                tfAuthority.StringValue = string.Empty;
                tfRedirectUri.StringValue = string.Empty;
                tfResourceId.StringValue = string.Empty;

                btnShowHideSecureText.Title = "Show Text";
			}
            else
            {
                tfApplicationId.StringValue = stfApplicationId.StringValue;
                tfAuthority.StringValue = stfAuthority.StringValue;
                tfRedirectUri.StringValue = stfRedirectUri.StringValue;
                tfResourceId.StringValue = stfResourceId.StringValue;

                tfApplicationId.Hidden = false;
                tfAuthority.Hidden = false;
                tfRedirectUri.Hidden = false;
                tfResourceId.Hidden = false;

                stfApplicationId.Hidden = true;
                stfAuthority.Hidden = true;
                stfRedirectUri.Hidden = true;
                stfResourceId.Hidden = true;

                stfApplicationId.StringValue = string.Empty;
                stfAuthority.StringValue = string.Empty;
                stfRedirectUri.StringValue = string.Empty;
                stfResourceId.StringValue = string.Empty;
                
                btnShowHideSecureText.Title = "Hide Text";
			}
        }

        partial void GetToken(NSObject sender)
        {
            //save the form for any changes to take affect
            tfStatus.StringValue = "Saving form changes";
            SaveForm(sender);

            tfStatus.StringValue = "Getting Token";
            var authService = new Authentication.MSALAuthenticationService(_currentEndpoint);
            InvokeOnMainThread(async () => 
			{ 
				var result = await authService.Authenticate();
                this.tvPiiLogs.Value = result.Item2;
                if (result.Item1 != null && !string.IsNullOrEmpty(result.Item1.AccessToken))
				{
                    tfStatus.StringValue = "Token Retrived";
                    tvRawToken.SetValueForKey(new NSString(result.Item1.AccessToken), new NSString("string"));
				}
                else 
				{ 
                    tfStatus.StringValue = "Error - see logs";
				}
			});

        }
    }

    public class ComboBoxDataSource 
		: NSComboBoxDataSource
    {
		public IList<string> Environments { get; set; }

        public ComboBoxDataSource()
        {
            Environments = MacEndpointService.Instance.GetEnvironments();
		}

        public override nint ItemCount(NSComboBox comboBox)
        {
            return Environments.Count;	
        }

        [Export("comboBox:objectValueForItemAtIndex:")]
        public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
        {
            return new NSString(Environments[(int)index]);
        }

        public override nint IndexOfItem(NSComboBox comboBox, string value)
        {
            return Environments.IndexOf(value); 
        }
    }
}
