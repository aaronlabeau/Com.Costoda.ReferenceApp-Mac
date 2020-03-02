using System;
using System.Collections.Generic;
using System.Linq;
using Com.Costoda.ReferenceApp.CoreLib.Authentication;
using Com.Costoda.ReferenceApp.Mac.IO;
using CoreImage;
using Newtonsoft.Json;

namespace Com.Costoda.ReferenceApp.Mac.Authentication
{
    public class MacEndpointService
    {
        private const string _fileName = "endpoints.json";
        private string _filePath = $"{FileSystemService.Instance.FilesStoragePath}/{_fileName}";

        #region Singleton

        private MacEndpointService()
        {
            InitializeEndpoints();
        }

        private readonly static Lazy<MacEndpointService> _instance = new Lazy<MacEndpointService>(() => new MacEndpointService());

        public static MacEndpointService Instance => _instance.Value;

        #endregion

        public MacEndpoint SelectedEndpoint { get; set; }
        public IList<MacEndpoint> CurrentEndpoints { get; set; }

        private void InitializeEndpoints()
        {
            List<MacEndpoint> macEndpoints = new List<MacEndpoint>();
            IList<Endpoint> endpoints = null;

            //get endpoints from disk 
            if (!FileSystemService.Instance.FileExists(_filePath))
            {
                FileSystemService.Instance.CreateDirectory(FileSystemService.Instance.FilesStoragePath);

                endpoints = EndpointService.Instance.CurrentEndpoints;
                var json = JsonConvert.SerializeObject(endpoints);
                FileSystemService.Instance.WriteFile(_filePath, json);
            }
            else
            {
                var json = FileSystemService.Instance.ReadFile(_filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    var eps = JsonConvert.DeserializeObject<IList<Endpoint>>(json);
                    if (eps != null && eps.Count > 0)
                    {
                        endpoints = eps;
                    }
                }
            }
            //if we got endpoints back from disk, convert them for usage in the UI
            if (endpoints != null && endpoints.Count > 0)
            {
                //TODO figure out how to read from disk
                var environments = endpoints.Select(x => x.Environment)
											.Distinct()
											.ToList();

                foreach (var env in environments)
                {
                    MacEndpoint meb = new MacEndpoint() { Endpoints = new List<MacEndpoint>() };
                    meb.Environment = env;
                    meb.Name = string.Empty;

                    foreach (var e in endpoints 
                                        .Where(x => x.Environment == env)
                                        .ToList())
                    {
                        meb.Endpoints.Add(MacEndpoint.GetMacEndpoint(e));
                    }
                    macEndpoints.Add(meb);
                }
                CurrentEndpoints = macEndpoints;
            }
		}

        public void AddEndpoint(MacEndpoint macEndpoint)
        {
            var isUpdate = false;

            //save to disk
            var endpoints = GetEndpoints();
            var endpoint = macEndpoint.ToEndpoint();
            if (!endpoints.Any(x => x.Name == endpoint.Name && x.Environment == endpoint.Environment))
            {
                var editep = endpoints.SingleOrDefault(x => x.Name == endpoint.Name && x.Environment == endpoint.Environment);
                //if existing item, remove the list and we'll replace with the new edited one
                if (editep != null)
                {
					var index = endpoints.IndexOf(editep);
                    endpoints.RemoveAt(index);
                    isUpdate = true;
				}
            }
			endpoints.Add(endpoint);
            var json = JsonConvert.SerializeObject(endpoints);
            FileSystemService.Instance.WriteFile(_filePath, json);

            var rootNode = CurrentEndpoints.SingleOrDefault(x => x.Name == string.Empty && x.Environment == macEndpoint.Environment);
            //update UI
            if(isUpdate)
            {
                //if the item already exists just update it
                if (rootNode != null)
                {
                    var umep = rootNode.Endpoints.SingleOrDefault(x => x.Name == macEndpoint.Name && x.Environment == macEndpoint.Environment);
                    if (umep != null)
                    {
                        var index = rootNode.Endpoints.IndexOf(umep);
                        rootNode.Endpoints.RemoveAt(index);
                        rootNode.Endpoints.Add(macEndpoint);
                        //todo fix ordering
					}
                } 
			}
            else 
			{
                //check of the root node already exists for an environment, if so add as a child, if not add the root node and then the add the child
                if (CurrentEndpoints.Any(x=>x.Environment == macEndpoint.Environment && x.Name == string.Empty))
                {
                    if (rootNode != null)
                    {
                        rootNode.Endpoints.Add(macEndpoint);
                    }
                }
                else 
				{
                    MacEndpoint meb = new MacEndpoint() { Endpoints = new List<MacEndpoint>() };
                    meb.Environment = macEndpoint.Environment;
                    meb.Name = string.Empty;
                    meb.Endpoints.Add(macEndpoint);
                    CurrentEndpoints.Add(meb);
                }
            } 
		} 

        public IList<string> GetEnvironments() 
		{
            return new List<string>()
            {
                MacEndpoint.GetEnvironment(CoreLib.Authentication.Environment.Dev),
                MacEndpoint.GetEnvironment(CoreLib.Authentication.Environment.QA),
                MacEndpoint.GetEnvironment(CoreLib.Authentication.Environment.UAT),
                MacEndpoint.GetEnvironment(CoreLib.Authentication.Environment.Production),
                MacEndpoint.GetEnvironment(CoreLib.Authentication.Environment.Mock),
            };
		}
		
        public IList<Endpoint> GetEndpoints()
        {
            var endpoints = new List<Endpoint>();
			foreach (var mep in CurrentEndpoints)
            {
                endpoints.Add(mep.ToEndpoint());
                foreach (var cmep in mep.Endpoints)
                {
                    endpoints.Add(cmep.ToEndpoint());
				}
			}
            return endpoints;	 
		}
    }

}
