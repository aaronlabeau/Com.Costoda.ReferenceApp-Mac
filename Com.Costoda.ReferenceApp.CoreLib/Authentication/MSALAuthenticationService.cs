using System;
using Microsoft.Identity.Client;

namespace Com.Costoda.ReferenceApp.CoreLib.Authentication
{
    public class MSALAuthenticationService
    {
        public static IPublicClientApplication PublicClientApplication = null;
        private readonly Endpoint _endpoint;

		public MSALAuthenticationService(Endpoint endpoint)
        {
            _endpoint = endpoint;
            PublicClientApplication = PublicClientApplicationBuilder.Create(_endpoint.ClientId)
                    .WithRedirectUri($"msal{_endpoint.PackageName}://auth")
                    .Build();
        }
    }
}
