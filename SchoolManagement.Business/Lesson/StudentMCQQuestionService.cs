﻿using Microsoft.Extensions.Configuration;
using SchoolManagement.Business.Interfaces.LessonData;
using SchoolManagement.Data.Data;
using SchoolManagement.Master.Data.Data;
using SchoolManagement.Model;
using SchoolManagement.ViewModel;
using SchoolManagement.ViewModel.Common;
using SchoolManagement.ViewModel.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Business
{
    public class StudentMCQQuestionService : IStudentMCQQuestionService
    {
        private readonly MasterDbContext masterDb;
        private readonly SchoolManagementContext schoolDb;
        private readonly IConfiguration config;
        private readonly ICurrentUserService currentUserService;

        public StudentMCQQuestionService(MasterDbContext masterDb, SchoolManagementContext schoolDb, IConfiguration config, ICurrentUserService currentUserService)
        {
            this.masterDb = masterDb;
            this.schoolDb = schoolDb;
            this.config = config;
            this.currentUserService = currentUserService;
        }


        public List<StudentMCQQuestionViewModel> GetAllStudentMCQQuestions()
        {
            var response = new List<StudentMCQQuestionViewModel>();
            var query = schoolDb.StudentMCQQuestions.Where(u => u.Question.Id != null);
            var StudentMCQQuestionList = query.ToList();

            foreach (var item in StudentMCQQuestionList) 
            {
                var vm = new StudentMCQQuestionViewModel
                {
                    QuestionId = item.QuestionId,
                    QuestionName = item.Question.QuestionText,
                    //StudentAnswerText = 
                    StudentId = item.StudentId,
                    StudentName = item.Student.User.FullName,
                    TeacherComments = item.TeacherComments,
                    Marks = item.Marks,
                    IsCorrectAnswer = item.IsCorrectAnswer
                };
                response.Add(vm);
            }
            return response;
        }

        public async Task<ResponseViewModel> SaveStudentMCQQuestion(StudentMCQQuestionViewModel vm, string userName)
        {
            var respone = new ResponseViewModel();
            try
            {
                var currentuser = schoolDb.Users.FirstOrDefault(x => x.Username.ToUpper() == userName.ToUpper());
                var StudentMCQQuestions = schoolDb.StudentMCQQuestions.FirstOrDefault(x => x.QuestionId == vm.QuestionId);
                var loggedInUser = currentUserService.GetUserByUsername(userName);

                if (StudentMCQQuestions == null)
                {
                    StudentMCQQuestions = new StudentMCQQuestion()
                    {
                        QuestionId = vm.QuestionId,
                        StudentId = vm.StudentId,
                        TeacherComments = vm.TeacherComments,
                        Marks = vm.Marks,
                        IsCorrectAnswer = vm.IsCorrectAnswer
                    };

                    schoolDb.StudentMCQQuestions.Add(StudentMCQQuestions);
                    respone.IsSuccess = true;
                    respone.Message = " Student MCQ Question is added susccesfully.";
                }

                else
                {
                    StudentMCQQuestions.TeacherComments = vm.TeacherComments;
                    StudentMCQQuestions.Marks = vm.Marks;
                    StudentMCQQuestions.IsCorrectAnswer = vm.IsCorrectAnswer;

                    schoolDb.StudentMCQQuestions.Update(StudentMCQQuestions);
                    respone.IsSuccess = true;
                    respone.Message = " Student MCQ Question is updated susccesfully.";
                }

                await schoolDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                respone.IsSuccess = false;
                respone.Message = ex.ToString();
            }

            return respone;
        }

        public List<DropDownViewModel> GetAllQuestions()
        {
            var questions = schoolDb.Questions
              .Where(x => x.IsActive == true)
              .Select(q => new DropDownViewModel() { Id = q.Id, Name = string.Format("{0}", q.QuestionText) })
              .Distinct().ToList();

            return questions;
        }

        public List<DropDownViewModel> GetAllStudentNames()
        {
            var students = schoolDb.Students
              .Where(x => x.IsActive == true)
              .Select(s => new DropDownViewModel() { Id = s.Id, Name = string.Format("{0}", s.User.FullName) })
              .Distinct().ToList();

            return students;
        }

        public List<DropDownViewModel> GetAllStudentAnswerTexts()
        {
            var studentanswertext = schoolDb.MCQQuestionStudentAnswers
               .Where(x => x.QuestionId != null)
               .Select(sqt => new DropDownViewModel() { Id = sqt.QuestionId, Name = string.Format("{0}", sqt.AnswerText) })
               .Distinct().ToList();

            return studentanswertext;
        }

        public PaginatedItemsViewModel<BasicStudentMCQQuestionViewModel> GetStudentNameList(string searchText, int currentPage, int pageSize, int studentNameId)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;

            var vmu = new List<BasicStudentMCQQuestionViewModel>();

            var student = schoolDb.StudentMCQQuestions.OrderBy(x => x.StudentId);

            if (!string.IsNullOrEmpty(searchText))
            {
                student = student.Where(x => x.Student.User.FullName.Contains(searchText)).OrderBy(x => x.StudentId);
            }

            if (studentNameId > 0)
            {
                student = student.Where(x => x.StudentId == studentNameId).OrderBy(x => x.StudentId);
            }


            totalRecordCount = student.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var studentNameList = student.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            studentNameList.ForEach(student =>
            {
                var vm = new BasicStudentMCQQuestionViewModel()
                {
                    QuestionId = student.QuestionId,
                    QuestionName = student.Question.QuestionText,
                    StudentId = student.StudentId,
                    StudentName = student.Student.User.FullName,
                    TeacherComments = student.TeacherComments,
                    Marks = student.Marks,
                    IsCorrectAnswer = student.IsCorrectAnswer

                };
                vmu.Add(vm);
            });

            var container = new PaginatedItemsViewModel<BasicStudentMCQQuestionViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vmu);

            return container;
        }
    }
}
