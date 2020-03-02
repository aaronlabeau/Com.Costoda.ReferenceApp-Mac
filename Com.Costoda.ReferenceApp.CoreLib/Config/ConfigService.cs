using System;
namespace Com.Costoda.ReferenceApp.CoreLib.Config
{
    public class ConfigService
    {
        #region Singleton

        private ConfigService()
        {
        }

        private readonly static Lazy<ConfigService> _instance = new Lazy<ConfigService>(() => new ConfigService());

        public static ConfigService Instance => _instance.Value;

        #endregion

		public bool IsDevelopmentMode { get; set; }
        public bool IsMockServices { get; set; }
    }
}
