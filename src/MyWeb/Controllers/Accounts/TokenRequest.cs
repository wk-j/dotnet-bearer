using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MyWeb.Controllers.Accounts
{

    public class TokenRequest
    {
        public string User { set; get; }
        public string Password { set; get; }
    }
}