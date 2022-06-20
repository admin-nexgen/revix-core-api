namespace Revix.Core.Common.Contracts;

public interface ISecurityOptions
{
    string ClientId { get; set; }

    string ClientSecret { get; set; }

    string DelegationClientId { get; set; }

    string DelegationClientSecret { get; set; }

    string Username { get; set; }

    string Password { get; set; }

    string Scopes { get; set; }

    string AuthUrl { get; set; }

    bool AuthEnabled { get; set; }

    string HttpClientName { get; set; }
}