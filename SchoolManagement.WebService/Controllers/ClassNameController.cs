﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Business.Interfaces.MasterData;
using SchoolManagement.ViewModel.Master;
using SchoolManagement.WebService.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassNameController : ControllerBase
    {
        private readonly IClassNameService classNameService;
        private readonly IIdentityService identityService;

        public ClassNameController(IClassNameService classNameService, IIdentityService identityService)
        {
            this.classNameService = classNameService;
            this.identityService = identityService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var response = classNameService.GetClassNames();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassNameViewModel classNameVM)
        {
            var userName = identityService.GetUserName();
            var response = await classNameService.SavaClassName(classNameVM, userName);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await classNameService.DeleteClassName(id);
            return Ok(response);
        }
    }
}
