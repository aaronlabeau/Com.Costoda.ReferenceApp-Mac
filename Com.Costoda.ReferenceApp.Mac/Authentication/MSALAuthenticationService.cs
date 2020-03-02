using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppKit;
using Microsoft.Identity.Client;

namespace Com.Costoda.ReferenceApp.Mac.Authentication
{
    public class MSALAuthenticationService
    {
        public static IPublicClientApplication MsalClientApplication = null;
        private readonly MacEndpoint _endpoint;

        private StringBuilder _logs = new StringBuilder(); 

        public MSALAuthenticationService(MacEndpoint endpoint)
        {
            _endpoint = endpoint;

            MsalClientApplication = PublicClientApplicationBuilder
				.Create(_endpoint.ClientId)
                .WithAuthority(_endpoint.Authority)
                .WithLogging(
					Logging, 
					LogLevel.Verbose, 
					enablePiiLogging: true, 
					enableDefaultPlatformLogging: true)
				.WithRedirectUri(
					$"msauth.{_endpoint.PackageName}://auth")
				.Build();
        }


        public async Task<Tuple<AuthenticationResult, string>> Authenticate()
        {
            _logs.Clear();

            AuthenticationResult result = null;
            IEnumerable<IAccount> accounts = null;

			try
            {
                accounts = await MsalClientApplication
									.GetAccountsAsync();

                var account = accounts.FirstOrDefault();

                result = await MsalClientApplication
								.AcquireTokenSilent(_endpoint.Scopes, account)
								.ExecuteAsync(); 

            }
			catch(MsalUiRequiredException uiException)
            {
#if DEBUG
                Console.WriteLine($"{uiException.Message}");
#endif
				try
                {
                    result = await MsalClientApplication
						.AcquireTokenInteractive(_endpoint.Scopes)
						.WithParentActivityOrWindow(NSApplication.SharedApplication.MainWindow)
						.ExecuteAsync();
				}
                catch(Exception ex)
                { 
#if DEBUG
					Console.WriteLine($"{ex.Message} Stack Trace: {ex.StackTrace}");
#endif
				}

            }

            return new Tuple<AuthenticationResult, string>(result, _logs.ToString());
		}

        private void Logging(LogLevel level, string message, bool containsPii)
        {
            _logs.Append($"{DateTimeOffset.Now} :: MSAL Log Level: {level} {containsPii} Message: {message}");
		}
    }
}
