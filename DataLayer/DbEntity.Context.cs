﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GidaGKPEntities : DbContext
    {
        public GidaGKPEntities()
            : base("name=GidaGKPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApplicantApplicationDetail> ApplicantApplicationDetails { get; set; }
        public virtual DbSet<ApplicantBankDetail> ApplicantBankDetails { get; set; }
        public virtual DbSet<ApplicantDetail> ApplicantDetails { get; set; }
        public virtual DbSet<ApplicantFormStep> ApplicantFormSteps { get; set; }
        public virtual DbSet<ApplicantPlotDetail> ApplicantPlotDetails { get; set; }
        public virtual DbSet<ApplicantProjectDetail> ApplicantProjectDetails { get; set; }
        public virtual DbSet<ApplicantTransactionDetail> ApplicantTransactionDetails { get; set; }
        public virtual DbSet<ApplicantUploadDoc> ApplicantUploadDocs { get; set; }
        public virtual DbSet<ApplicantUser> ApplicantUsers { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
    }
}
