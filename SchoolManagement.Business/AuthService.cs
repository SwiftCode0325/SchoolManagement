﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Business.Interfaces;
using SchoolManagement.Data.Data;
using SchoolManagement.Master.Data.Data;
using SchoolManagement.Util;
using SchoolManagement.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Business
{
    public class AuthService : IAuthService
    {
        private readonly MasterDbContext masterDb;
        private readonly SchoolManagementContext schoolDb;
        private readonly IConfiguration config;


        public AuthService(MasterDbContext masterDb, SchoolManagementContext schoolDb, IConfiguration config)
        {
            this.masterDb = masterDb;
            this.schoolDb = schoolDb;
            this.config = config;
        }
        public UserTokenViewModel Login(LoginViewModel model)
        {
            var response = new UserTokenViewModel();

            //Find the user using username and user should be active;
            var user = schoolDb.Users.FirstOrDefault(u => u.Username.Trim().ToUpper() == model.Username.Trim().ToUpper() && u.IsActive==true);
            //if user is null. Then return response with propert error messsage;
            if(user == null)
            {
                response.IsLoginSuccess = false;
                response.LoginMessage = "User Not Found";

                return response;
            }
            //Else match password with Password hasher.
            if (CustomPasswordHasher.GenerateHash(model.Password) != user.Password)
            {
                response.IsLoginSuccess = false;
                response.LoginMessage = "Login failed Username or Password Invalid";

                return response;
            }

            var school = masterDb.Schools.FirstOrDefault(t => t.SchoolDomain.ToUpper().Trim() == model.SchoolDomain.ToUpper().Trim());

            if(school==null)
            {
                response.IsLoginSuccess = false;
                response.LoginMessage = "Login failed.Invalid school domain name provided.";

                return response;
            }

            //var key = config["Token:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(school.SecretKey.ToString()));
            var signinCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            string userRole = string.Empty;
            string role = user.UserRoles.FirstOrDefault().Role.Name;

            var nowDateTime = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,nowDateTime.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Aud,"mobileapp"),
                new Claim(JwtRegisteredClaimNames.Aud,"webapp"),
                new Claim(ClaimTypes.Role,role),
                new Claim("SecretKey",school.SecretKey.ToString()),
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: config["Tokens:Issuer"],
                claims: claims,
                notBefore: nowDateTime,
                expires: nowDateTime.AddDays(100),
                signingCredentials: signinCredential
            );

            tokenOptions.Header.Add("kid", school.APIKey.ToString());

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            response.Token = tokenString;
            response.IsLoginSuccess = true;
            response.DisplayName = user.FullName;
            response.Roles = user.UserRoles.Select(t => t.Role).Select(t => t.Name).ToList();
            response.SchoolDomain = model.SchoolDomain;

            return response;

           
        }
    }
}