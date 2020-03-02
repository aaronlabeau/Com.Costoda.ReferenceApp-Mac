using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using CoreImage;
using Foundation;
using Newtonsoft.Json;

namespace Com.Costoda.ReferenceApp.Mac.Authentication
{
    public class MacEndpoint 
		: NSObject
    {
        [JsonIgnore]
        public string Title
        {
            get 
			{ 
                switch (this.Environment)
                {
                    case CoreLib.Authentication.Environment.Dev:
                        return "Dev";
                    case CoreLib.Authentication.Environment.Mock:
                        return "Mock";
                    case CoreLib.Authentication.Environment.QA:
                        return "QA";
                    case CoreLib.Authentication.Environment.UAT:
                        return "UAT";
                    case CoreLib.Authentication.Environment.Production:
                    default: 
                        return "Production";
				}
			}
        }

        [JsonIgnore]
        public bool IsGroup 
		{
            get { return (Endpoints.Count > 0); }
		}

        public IList<MacEndpoint> Endpoints = new List<MacEndpoint>();

        //Required by ADAL
        //
        public bool EnableLogging { get; set; }
        public string KeychainSecurityGroup { get; set; }
        public string Name { get; set; }
        public string PackageName => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleIdentifier").ToString(); 
        public string ClientId { get; set; }
        public string Authority { get; set; }
        public string RedirectUri { get; set; }
        public string ResourceId { get; set; }
        public string ResourceBaseUri { get; set; }
        public string ExtraParameters { get; set; }
        public string [] Scopes { get; set; }
        public string TeamId { get; set; }
        public string SecurityGroup { get; set; }
        public CoreLib.Authentication.Environment Environment { get; set; }

        public static CoreLib.Authentication.Environment GetEnvironment(string value)
        {
            switch (value)
            {
                case "Dev":
                    return CoreLib.Authentication.Environment.Dev;
                case "Mock":
                    return CoreLib.Authentication.Environment.Mock;
                case "QA":
                    return CoreLib.Authentication.Environment.QA;
                case "UAT":
                    return CoreLib.Authentication.Environment.UAT;
                case "Production":
                default:
                    return CoreLib.Authentication.Environment.Production;
            }
        }

        public static string GetEnvironment(CoreLib.Authentication.Environment environment)
        {
            switch (environment)
            {
                case CoreLib.Authentication.Environment.Dev:
                    return "Dev";
                case CoreLib.Authentication.Environment.Mock:
                    return "Mock";
                case CoreLib.Authentication.Environment.QA:
                    return "QA";
                case CoreLib.Authentication.Environment.UAT:
                    return "UAT";
                case CoreLib.Authentication.Environment.Production:
                default:
                    return "Production";
            }
        }

        public static MacEndpoint GetMacEndpoint(CoreLib.Authentication.Endpoint endpoint)
        {
            return new MacEndpoint
            {
                EnableLogging = endpoint.EnableLogging,
                ClientId = endpoint.ClientId,
                Authority = endpoint.Authority,
                Environment = endpoint.Environment,
                Name = endpoint.Name,
                RedirectUri = endpoint.RedirectUri,
                ResourceId = endpoint.ResourceId,
                ResourceBaseUri = endpoint.ResourceBaseUri,
                ExtraParameters = endpoint.ExtraParameters,
                TeamId = endpoint.TeamId,
                Scopes = endpoint.Scopes,
                SecurityGroup = endpoint.SecurityGroup
            };
		}

        public CoreLib.Authentication.Endpoint ToEndpoint()
        {
            return new CoreLib.Authentication.Endpoint 
			{
                EnableLogging = true,
                ClientId = this.ClientId,
                Authority = this.Authority,
                Environment = this.Environment,
                Name = this.Name,
                RedirectUri = this.RedirectUri,
                ResourceBaseUri = this.ResourceBaseUri,
                ResourceId = this.ResourceId,
                ExtraParameters = this.ExtraParameters,
                TeamId = this.TeamId,
                Scopes = this.Scopes,
                SecurityGroup = this.SecurityGroup
            };
		} 
    }
}
