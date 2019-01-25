// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreSecurity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ManageCustomers", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("department", "sales");
            //        policy.RequireClaim("status", "senior");
            //    });
            //});

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "CookieHandler";
                    options.DefaultChallengeScheme = "oidc";

                    //options.DefaultSignInScheme = "CookieHandler";
                    //options.DefaultSignInScheme
                    //options.DefaultChallengeScheme = "CookieHandler";
                    ////options.DefaultAuthenticateScheme = "CookieHandler";
                    ////options.DefaultForbidScheme = "CookieHandler";

                })
                .AddCookie("CookieHandler", options =>
               {
                   options.Cookie.Name = "MyCookie";
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                   options.SlidingExpiration = true;
                   //options.LoginPath = "/account/login";
                   options.AccessDeniedPath = "/account/accessdenied";
               })
               //.AddCookie("Temp")
               //.AddGoogle("Google", options=>
               //{
               //    // options.CallbackPath = "/foo-bar-baz";
               //    options.ClientId = "538301208639-ss13igqmdsuufmgtvueq9fk4g5uckenp.apps.googleusercontent.com";
               //    options.ClientSecret = "oG4Ffvg26D9Iov0yHb9EtZ1z";
               //    options.SignInScheme = "Temp";
               //    //options.Scope.Add("other");
               //})
               //.AddOpenIdConnect("oidc", options =>
               //{
               //    options.CallbackPath = "/signin-oidc";
               //    options.Authority = "https://demo.identityserver.io/";
               //    options.ClientId = "implicit";
               //    options.ResponseType = "id_token";

               //    options.Scope.Clear();
               //    options.Scope.Add("openid");
               //    options.Scope.Add("profile");
               //    options.Scope.Add("email");
               //});
               .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "http://localhost:5000/";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("api1");
                    options.Scope.Add("offline_access");
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();


        }
    }
}