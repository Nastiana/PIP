﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pip_air.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AirportsEntities : DbContext
    {
        public AirportsEntities()
            : base("name=AirportsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Tiskets> Tiskets { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}