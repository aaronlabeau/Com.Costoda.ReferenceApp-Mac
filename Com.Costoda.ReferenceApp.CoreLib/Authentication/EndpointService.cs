using System;
using System.Collections;
using System.Collections.Generic;

namespace Com.Costoda.ReferenceApp.CoreLib.Authentication
{
    public class EndpointService
    {
        #region Singleton

        private EndpointService() 
		{
            InitializeEndpoints();
		}

        private readonly static Lazy<EndpointService> _instance = new Lazy<EndpointService>(() => new EndpointService());

        public static EndpointService Instance => _instance.Value;

        #endregion

        public IList<Endpoint> CurrentEndpoints { get; set; }

        private void InitializeEndpoints() 
		{
            //TODO read this from disk 
            CurrentEndpoints = new List<Endpoint>
            {
                new Endpoint
                {
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    Authority = "https://login.microsoftonline.com/common",
                    RedirectUri = "INSERT_REDIRECT_URI_HERE",
                    ResourceId = "INSERT_RESOURCEID_HERE",
                    Name = "Sample App Prod" ,
                    EnableLogging = true,
                    TeamId="",
                    SecurityGroup = "",
                    Environment = Environment.Production,
                    ExtraParameters = "",
                    KeychainSecurityGroup = "",
                    Scopes = new string[] { "User.Read" },
                    ResourceBaseUri = ""
                },
                new Endpoint
                {
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    Authority = "https://login.microsoftonline.com/common",
                    RedirectUri = "INSERT_REDIRECT_URI_HERE",
                    ResourceId = "INSERT_RESOURCEID_HERE",
                    Name = "Sample App Dev" ,
                    EnableLogging = true,
                    TeamId="",
                    SecurityGroup = "",
                    Environment = Environment.Dev,
                    ExtraParameters = "",
                    KeychainSecurityGroup = "",
                    Scopes = new string[] { "User.Read" },
                    ResourceBaseUri = ""
                },
                new Endpoint
                {
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    Authority = "https://login.microsoftonline.com/common",
                    RedirectUri = "INSERT_REDIRECT_URI_HERE",
                    ResourceId = "INSERT_RESOURCEID_HERE",
                    Name = "Sample App 2 Dev" ,
                    EnableLogging = true,
                    TeamId="",
                    SecurityGroup = "",
                    Environment = Environment.Dev,
                    ExtraParameters = "",
                    KeychainSecurityGroup = "",
                    Scopes = new string[] { "User.Read" },
                    ResourceBaseUri = ""
                },
                new Endpoint
                {
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    Authority = "https://login.microsoftonline.com/common",
                    RedirectUri = "INSERT_REDIRECT_URI_HERE",
                    ResourceId = "INSERT_RESOURCEID_HERE",
                    Name = "Sample App QA" ,
                    EnableLogging = true,
                    TeamId="",
                    SecurityGroup = "",
                    Environment = Environment.QA,
                    ExtraParameters = "",
                    KeychainSecurityGroup = "",
                    Scopes = new string[] { "User.Read" },
                    ResourceBaseUri = ""
                },
                new Endpoint
                {
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    Authority = "https://login.microsoftonline.com/common",
                    RedirectUri = "INSERT_REDIRECT_URI_HERE",
                    ResourceId = "INSERT_RESOURCEID_HERE",
                    Name = "Sample App UAT" ,
                    EnableLogging = true,
                    TeamId="",
                    SecurityGroup = "",
                    Environment = Environment.UAT,
                    ExtraParameters = "",
                    KeychainSecurityGroup = "",
                    Scopes = new string[] { "User.Read" },
                    ResourceBaseUri = ""
                }
            };
		}

        public void AddEndpoint(Endpoint endpoint)
        {
            CurrentEndpoints.Add(endpoint);
		}
    }
}
