﻿using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel.Master
{
    public class ClassTeacherViewModel
    {

		public int ClassNameId { get; set; }
        public string TeacherClassName { get; set; }
        public int AcademicLevelId { get; set; }
        public string AcademicLevelName { get; set; }
        public int AcademicYearId { get; set; }
		public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public bool IsPrimary { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedOn { get; set; }
		public int UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
    }
}
