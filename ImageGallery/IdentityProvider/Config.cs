﻿using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityProvider
{
    public static class Config
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "b07ba099-2a33-4059-9c2a-6ee574d5dbf0",
                    Username = "Frank",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Johnson"),
                        new Claim("address", "1 Street"),
                        new Claim("role", "FreeUser")

                    }
                },
                new TestUser
                {
                    SubjectId = "7aa6b243-9baf-4fa1-b9ed-41f457559585",
                    Username = "Bob",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Bob"),
                        new Claim("family_name", "Johnson"),
                        new Claim("address", "2 Street"),
                        new Claim("role", "PayingUser")


                    }
                }

            };
        }

        public static List<IdentityResource> GetTestIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("role", "Roles(s)", new List<string>{"role"})
            };
        }

        public static List<Client> GetTestClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>
                    {
                       "https://localhost:44336/signin-oidc"
                    },
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        "role"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:44336/signout-callback-oidc"
                    }
                }
            };
        }
    }
}