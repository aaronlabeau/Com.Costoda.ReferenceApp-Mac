using System;
using System.Collections.Generic;
using Security;

namespace Com.Costoda.ReferenceApp.Mac.Keychain
{
    public class KeychainService
    {
        #region Singleton

        private KeychainService() { }

        private readonly static Lazy<KeychainService> _instance = new Lazy<KeychainService>(() => new KeychainService());

        public static KeychainService Instance => _instance.Value;

        #endregion

        public IList<SecRecord> GetRecordsFromKeychain(string key)
        {
            List<SecRecord> returnResults = new List<SecRecord>();
            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                AccessGroup = key
            };

            var records = SecKeyChain.QueryAsRecord(queryRecord, Int32.MaxValue, out SecStatusCode resultCode);
            if (resultCode == SecStatusCode.Success)
            {
                returnResults.AddRange(records);
            }
            return returnResults;
        }

        public bool ClearRecordsFromKeychain(string key)
        {
            bool results = false;

            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                AccessGroup = key
            };
            var queryResults = SecKeyChain.Remove(queryRecord);

            if (queryResults == SecStatusCode.Success)
                results = true;

            return results;
        }
    }
}
