using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BusinessEntities;

namespace DataAccessLayer
{
    public class PokemonDal : DbContext
    {
        public DbSet<PokemonEntity> PokemonEntities { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            //数据库与实体映射
            modelBuilder.Entity<PokemonEntity>().ToTable("PKPokemon");

            base.OnModelCreating(modelBuilder);            
        }
    }
}