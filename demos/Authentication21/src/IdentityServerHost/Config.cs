// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServerHost
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),

            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1", new[]{ "name", "email" }),
                new ApiResource("api2", "My API #2"),
                new ApiResource("api3", "My API #3"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "console",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1", "api2" }
                },
               new Client
               {
                   ClientId = "mvc",
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   ClientSecrets = { new Secret("secret".Sha256()) },
                   RedirectUris = { "http://localhost:5002/signin-oidc" },
                   PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                   AllowedScopes = { "openid", "profile", "email", "api1" },
                   AllowOfflineAccess = true,
                   RefreshTokenUsage = TokenUsage.OneTimeOnly,
                   AbsoluteRefreshTokenLifetime = (int)TimeSpan.FromDays(365).TotalSeconds,
                   SlidingRefreshTokenLifetime = (int)TimeSpan.FromDays(14).TotalSeconds,

               }
            };
        }
    }
}