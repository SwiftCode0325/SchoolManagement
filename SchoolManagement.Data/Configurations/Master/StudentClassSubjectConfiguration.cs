﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Data.Common;
using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Configurations.Master
{
    public class StudentClassSubjectConfiguration : IEntityTypeConfiguration<StudentClassSubject>
    {
        public void Configure(EntityTypeBuilder<StudentClassSubject> builder)
        {
            builder.ToTable("StudentClassSubject", Schema.MASTER);

            builder.HasKey(x => new { x.StudentId, x.ClassNameId, x.AcademicLevelId, x.AcademicYearId, x.SubjectId });

            builder.HasOne<StudentClass>(x => x.StudentClass)
                .WithMany(sc => sc.StudentClassSubjects)
                .HasForeignKey(f => new { f.StudentId, f.ClassNameId, f.AcademicLevelId, f.AcademicYearId})
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.HasOne<SubjectAcademicLevel>(x => x.SubjectAcademicLevel)
                .WithMany(sc => sc.StudentClassSubjects)
                .HasForeignKey(f => new { f.SubjectId, f.AcademicLevelId })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

        }
    }
}
