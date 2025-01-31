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
    public class ClassTeacherController : ControllerBase
    {
        private readonly IClassTeacherService classTeacherService;
        private readonly IIdentityService identityService;

        public ClassTeacherController(IClassTeacherService classTeacherService, IIdentityService identityService)
        {
            this.classTeacherService = classTeacherService;
            this.identityService = identityService;
        }

        [HttpGet]
        [Route("getClassTeachers")]
        public ActionResult GetClassTeachers()
        {
            var response = classTeacherService.GetClassTeachers();
            return Ok(response);
        }

        [HttpPost]
        [Route("saveClassTeacher")]
        public async Task<ActionResult> Post([FromBody] ClassTeacherViewModel classTeacherVM)
        {
            var userName = identityService.GetUserName();
            var response = await classTeacherService.SavaClassTeacher(classTeacherVM, userName);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await classTeacherService.DeleteClassTeacher(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllClassNames")]
        public ActionResult GetAllClassNames()
        {
            var response = classTeacherService.GetAllClassNames();

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllAcademicLevels")]
        public ActionResult GetAllAcademicLevels()
        {
            var response = classTeacherService.GetAllAcademicLevels();

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllAcademicYears")]
        public ActionResult GetAllAcademicYears()
        {
            var response = classTeacherService.GetAllAcademicYears();

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllTeachers")]
        public ActionResult GetAllTeachers()
        {
            var response = classTeacherService.GetAllTeachers();

            return Ok(response);
        }
    }
}
