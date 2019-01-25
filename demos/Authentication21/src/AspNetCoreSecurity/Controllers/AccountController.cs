// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreSecurity.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("CookieHandler");
            await HttpContext.SignOutAsync("oidc");
            //return Redirect("~/");
        }

        //[HttpGet]
        //public IActionResult Login(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(
        //    string userName, string password, 
        //    string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;

        //    if (!string.IsNullOrWhiteSpace(userName) &&
        //        userName == password)
        //    {
        //        var claims = new Claim[]
        //        {
        //            new Claim("sub", "12345"),
        //            new Claim("name", "Brock Allen"),
        //            new Claim("email", "brockallen@gmail.com"),
        //            new Claim("department", "sales"),
        //            new Claim("status", "senior"),
        //        };
        //        var ci = new ClaimsIdentity(claims, "pwd");
        //        var cp = new ClaimsPrincipal(ci);

        //        await HttpContext.SignInAsync("CookieHandler", cp);

        //        if (Url.IsLocalUrl(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }

        //        throw new Exception("invalid return url");
        //    }

        //    return View();
        //}
 
        [HttpGet]
        public IActionResult AccessDenied() => View();

        //public IActionResult Google(string returnUrl)
        //{
        //    //await HttpContext.ChallengeAsync("Google");
        //    //return new EmptyResult();

        //    returnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/home/secure";

        //    var props = new AuthenticationProperties {
        //        RedirectUri = "/Account/Callback"
        //    };
        //    props.Items.Add("realRedirectUri", returnUrl);

        //    return Challenge(props, "Google");
        //}

        //public async Task<IActionResult> Callback()
        //{
        //    var authResult = await HttpContext.AuthenticateAsync("Temp");
        //    if (!authResult.Succeeded) return Redirect("~/account/login");

        //    var nameIdClaim = authResult.Principal.FindFirst(ClaimTypes.NameIdentifier);

        //    var nameIdValue = nameIdClaim.Value;
        //    var provider = nameIdClaim.Issuer;

        //    // TODO: lookup your user in the DB based on provider & provider's user id

        //    var claims = new Claim[]
        //       {
        //            new Claim("sub", "12345"),
        //            new Claim("name", "Brock Allen"),
        //            new Claim("email", "brockallen@gmail.com"),
        //            new Claim("department", "sales"),
        //            new Claim("status", "senior"),
        //            new Claim("idp", provider)
        //       };
        //    var ci = new ClaimsIdentity(claims, "external");
        //    var cp = new ClaimsPrincipal(ci);

        //    await HttpContext.SignInAsync("CookieHandler", cp);
        //    await HttpContext.SignOutAsync("Temp");

        //    var returnUrl = authResult.Properties.Items["realRedirectUri"];

        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }

        //    return Redirect("/home/secure");
        //}
   }
}