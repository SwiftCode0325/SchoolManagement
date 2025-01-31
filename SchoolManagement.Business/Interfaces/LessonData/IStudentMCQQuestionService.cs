﻿using SchoolManagement.ViewModel;
using SchoolManagement.ViewModel.Common;
using SchoolManagement.ViewModel.Lesson;
using SchoolManagement.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolManagement.Business.Interfaces.LessonData
{
    public interface IStudentMCQQuestionService
    {
        Task<ResponseViewModel> SaveStudentMCQQuestion(StudentMCQQuestionViewModel vm, string userName);
        List<StudentMCQQuestionViewModel> GetAllStudentMCQQuestions();
        List<DropDownViewModel> GetAllQuestions();
        List<DropDownViewModel> GetAllStudentAnswerTexts();
        List<DropDownViewModel> GetAllStudentNames();

        PaginatedItemsViewModel<BasicStudentMCQQuestionViewModel> GetStudentNameList(string searchText, int currentPage, int pageSize, int studentNameId);
    }
}
