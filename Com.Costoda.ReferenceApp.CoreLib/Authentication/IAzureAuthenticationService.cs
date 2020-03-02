//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//using Microsoft.IdentityModel.Clients.ActiveDirectory;

//namespace Com.Costoda.ReferenceApp.CoreLib.Authentication
//{
//    public interface IAzureAuthenticationService
//    {
//        Task<Tuple<CacheToken, string>> AuthenticateEndpoint(
//           Endpoint endpoint,
//           [CallerMemberName] string memberName = "",
//           [CallerLineNumber] int lineNumber = 0);

//        Task<Tuple<CacheToken, string>> AcquireTokenSilentAsync(
//            Endpoint endpoint,
//            [CallerMemberName] string memberName = "",
//            [CallerLineNumber] int lineNumber = 0);

//		Tuple<IEnumerable<TokenCacheItem>, string> GetCachedTokens(
//            Endpoint endpoint,
//            [CallerMemberName] string memberName = "",
//            [CallerLineNumber] int lineNumber = 0);

//        bool ClearCachedTokens(
//            Endpoint endpoint,
//            [CallerMemberName] string memberName = "",
//            [CallerLineNumber] int lineNumber = 0);
//    }
//}
