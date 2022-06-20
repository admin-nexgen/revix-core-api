using System;
using Revix.Core.Common.Contracts;

namespace Revix.Core.Common.Configurations;

[Serializable]
public class SecurityOptions : ISecurityOptions
{
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string DelegationClientId { get; set; }

    public string DelegationClientSecret { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Scopes { get; set; }

    public string AuthUrl { get; set; }

    public bool AuthEnabled { get; set; }

    public string HttpClientName { get; set; }

    public bool EnableCaching { get; set; }
}