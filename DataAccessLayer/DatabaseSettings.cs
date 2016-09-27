using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseSettings
    {
        public static void SetDatabase()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PokemonDal>());
            using (var context = new PokemonDal())
            {
                context.Database.Initialize(true);
            }
        }
    }
}