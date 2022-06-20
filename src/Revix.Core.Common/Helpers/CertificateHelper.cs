using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Serilog;

namespace Revix.Core.Common.Helpers;

public static class CertificateHelper
{
    public static bool ServerCertificateCustomValidation(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
    {
        Log.Information("Requested URI: {RequestUri}", requestMessage.RequestUri);
        Log.Information("Effective date: {GetEffectiveDateString}", certificate.GetExpirationDateString());
        Log.Information("Exp date: {GetExpirationDateString}", certificate.GetExpirationDateString());
        Log.Information("Issuer: {Issuer}", certificate.Issuer);
        Log.Information("Subject: {Subject}", certificate.Subject);
            
        Log.Warning("Errors: {SslErrors}", sslErrors);
            
        return true;
    }
}