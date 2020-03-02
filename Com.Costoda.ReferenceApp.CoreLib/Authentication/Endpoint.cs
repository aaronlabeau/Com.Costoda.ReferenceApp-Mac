using System;
namespace Com.Costoda.ReferenceApp.CoreLib.Authentication
{
    public class Endpoint
    {
        //
        //Required by ADAL
        //
        public bool EnableLogging { get; set; }
        public string KeychainSecurityGroup { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string PackageName => Xamarin.Essentials.AppInfo.PackageName; 
        public string Authority { get; set; }
        public string RedirectUri { get; set; }
        public string ResourceId { get; set; }
        public string ResourceBaseUri { get; set; }
        public string ExtraParameters { get; set; }
		public string[] Scopes { get; set; }

        public string TeamId { get; set; }
        public string SecurityGroup { get; set; }
        public Environment Environment { get; set; }
    }

    public enum Environment
    { 
        Dev,
        QA,
        UAT,
        Production,
        Mock
    }
}
