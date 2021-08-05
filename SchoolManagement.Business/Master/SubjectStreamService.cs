﻿using Microsoft.Extensions.Configuration;
using SchoolManagement.Business.Interfaces.MasterData;
using SchoolManagement.Data.Data;
using SchoolManagement.Master.Data.Data;
using SchoolManagement.Model;
using SchoolManagement.ViewModel.Common;
using SchoolManagement.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Business.Master
{
    public class SubjectStreamService : ISubjectStreamService
    {
        private readonly MasterDbContext masterDb;
        private readonly SchoolManagementContext schoolDb;
        private readonly IConfiguration config;
        private readonly ICurrentUserService currentUserService;

        public SubjectStreamService(MasterDbContext masterDb, SchoolManagementContext schoolDb, IConfiguration config, ICurrentUserService currentUserService)
        {
            this.masterDb = masterDb;
            this.schoolDb = schoolDb;
            this.config = config;
            this.currentUserService = currentUserService;
        }

        public async Task<ResponseViewModel> SaveSubjectStream(SubjectStreamViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var loggedInUser = currentUserService.GetUserByUsername(userName);

                var subjectStream = schoolDb.SubjectStreams.FirstOrDefault(x => x.Id == vm.Id);

                if(subjectStream == null)
                {
                    subjectStream = new SubjectStream()
                    {
                        Id = vm.Id,
                        Name = vm.Name,
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id,
                    };

                    schoolDb.SubjectStreams.Add(subjectStream);

                    response.IsSuccess = true;
                    response.Message = "SubjectStream Added Successfully.";
                }
                else
                {
                    subjectStream.Name = vm.Name;
                    subjectStream.UpdatedOn = DateTime.UtcNow;
                    subjectStream.UpdatedById = loggedInUser.Id;

                    schoolDb.SubjectStreams.Update(subjectStream);

                    response.IsSuccess = true;
                    response.Message = "Subject stream Update Successfully.";
                }

                await schoolDb.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
