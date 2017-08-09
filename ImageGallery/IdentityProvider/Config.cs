using System.Collections.Generic;
using System.Security.Claims;
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
                        new Claim("family_name", "Johnson")
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
                        new Claim("family_name", "Johnson")
                    }
                }

            };
        }

        public static List<IdentityResource> GetTestIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<Client> GetTestClients()
        {
            return new List<Client>
            {
                
            };
        }
    }
}