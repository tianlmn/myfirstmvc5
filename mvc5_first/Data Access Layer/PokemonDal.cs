using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using mvc5_first.Models;

namespace mvc5_first.Data_Access_Layer
{
    public class PokemonDal : DbContext
    {
        public DbSet<PokemonEntity> PokemonEntities { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约

            //数据库与实体映射
            modelBuilder.Entity<PokemonEntity>().ToTable("PKPokemon");
        }
    }
}