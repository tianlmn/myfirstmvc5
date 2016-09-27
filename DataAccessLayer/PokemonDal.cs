using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BusinessEntities;

namespace DataAccessLayer
{
    public class PokemonDal : DbContext
    {
        public PokemonDal()
            : base("name=PokemonDal")
        {
            
        }
        
        public DbSet<PokemonEntity> PokemonEntities { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约

            //数据库与实体映射
            modelBuilder.Entity<PokemonEntity>().ToTable("PMPokemon");
        
        }
    }
}