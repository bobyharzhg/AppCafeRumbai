﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CafeRumbai
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CafeRumbaiEntities4 : DbContext
    {
        public CafeRumbaiEntities4()
            : base("name=CafeRumbaiEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<keranjang> keranjangs { get; set; }
        public virtual DbSet<minuman> minumans { get; set; }
        public virtual DbSet<tblLogin> tblLogins { get; set; }
        public virtual DbSet<ukuran> ukurans { get; set; }
    }
}
