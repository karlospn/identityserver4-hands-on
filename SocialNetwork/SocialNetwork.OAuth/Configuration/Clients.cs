﻿using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;

namespace SocialNetwork.OAuth.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> All()
        {
            return new[] {
                new Client
                {
                    ClientId = "fekberg",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                },
                new Client
                {
                    ClientId = "socialnetwork_implicit",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "socialnetwork"
                    },
                    RedirectUris = new[] {"http://localhost:28849/signin-oidc"},
                    PostLogoutRedirectUris = { "http://localhost:28849/signout-callback-oidc" },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "socialnetwork_code",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "socialnetwork",
                    },
                    AllowOfflineAccess = true,
                    RedirectUris = new[] {"http://localhost:28849/signin-oidc"},
                    PostLogoutRedirectUris = { "http://localhost:28849/signout-callback-oidc" },
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
    }
    public class Users
    {
        public static List<TestUser> All()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@filipekberg.se",
                    Password = "password",
                    Claims = new []{
                        new Claim("email", "mail@filipekberg.se"),
                        new Claim("test", "test_claim")
                    }
                }
            };
        }
    }

    public class ApiResources
    {
        public static IEnumerable<ApiResource> All()
        {
            return new[] {
                new ApiResource("socialnetwork", "Social Network")
                {
                    UserClaims = new [] { "email", "test" }
                }
            };
        }
    }

    public class IdentityResources
    {
        public static IEnumerable<IdentityResource> All()
        { 
            return new IdentityResource[] {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile(),
                new IdentityServer4.Models.IdentityResources.Email()
            };
        }
    }
}
