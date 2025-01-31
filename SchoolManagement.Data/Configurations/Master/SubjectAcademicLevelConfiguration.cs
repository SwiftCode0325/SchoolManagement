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
    public class SubjectAcademicLevelConfiguration : IEntityTypeConfiguration<SubjectAcademicLevel>
    {
        public void Configure(EntityTypeBuilder<SubjectAcademicLevel> builder)
        {
            builder.ToTable("SubjectAcademicLevel", Schema.MASTER);

            builder.HasKey(x => new { x.SubjectId, x.AcademicLevelId });

            builder.HasOne<Subject>(s => s.Subject)
               .WithMany(sa => sa.SubjectAcademicLevels)
               .HasForeignKey(f => f.SubjectId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

            builder.HasOne<AcademicLevel>(a => a.AcademicLevel)
               .WithMany(sa => sa.SubjectAcademicLevels)
               .HasForeignKey(f => f.AcademicLevelId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

        }
    }
}
