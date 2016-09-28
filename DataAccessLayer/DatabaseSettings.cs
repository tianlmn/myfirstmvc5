using System.Data.Entity;
using BusinessEntities;

namespace DataAccessLayer
{
    public class DatabaseSettings
    {
        public static void SetDatabase()
        {
            Database.SetInitializer(new PokemonDatabaseInit());
            using (var context = new PokemonDal())
            {
                context.Database.Initialize(true);
            }
        }
    }

    public class PokemonDatabaseInit : CreateDatabaseIfNotExists<PokemonDal>
    {
        protected override void Seed(PokemonDal context)
        {
            context.PokemonEntities.Add(new PokemonEntity()
            {
                PokemonId=1,
                PokemonName = "皮卡丘",
                PokemonAttr_1 = 3,
                PokemonAttr_2 = 3,
                PokemonNo = 25
            });
            base.Seed(context);
        }
    }
}