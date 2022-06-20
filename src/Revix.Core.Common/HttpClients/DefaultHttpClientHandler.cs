using System.Net;
using System.Net.Http;
using Revix.Core.Common.Helpers;

namespace Revix.Core.Common.HttpClients;

public class DefaultHttpClientHandler : HttpClientHandler
{
    public DefaultHttpClientHandler(bool bypassSslValidation = false)
    {
        DefaultProxyCredentials = CredentialCache.DefaultCredentials;
        UseProxy = false;

        if (bypassSslValidation)
        {
            ServerCertificateCustomValidationCallback = CertificateHelper.ServerCertificateCustomValidation;
        }
    }
}