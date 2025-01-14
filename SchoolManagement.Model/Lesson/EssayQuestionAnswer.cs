﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
   public  class EssayQuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Question Question { get; set; }
        
        public virtual ICollection<EssayStudentAnswer> EssayStudentAnswers { get; set; }







    }
}
