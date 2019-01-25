// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreSecurity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authz;

        public HomeController(IAuthorizationService authz)
        {
            _authz = authz;
        }

        public IActionResult Index()
        {
            //HttpContext.User.Identity.IsAuthenticated;
            //HttpContext.User.Claims;
            return View();
        }

        //[Authorize("ManageCustomers")]
        [Authorize]
        public async Task<IActionResult> Secure()
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var refresh_token = await HttpContext.GetTokenAsync("refresh_token");

            var result = await HttpContext.AuthenticateAsync("CookiesHandler");
            result.Properties.UpdateTokenValue("access_token", "new_token");
            result.Properties.UpdateTokenValue("refresh_token", "new_token");
            await HttpContext.SignInAsync("CookiesHandler", result.Principal, result.Properties);

            //var req = new JobLevelRequirement { Level = 2 };
            //var result = await _authz.AuthorizeAsync(User, null, req);

            return View();
        }
    }
}
