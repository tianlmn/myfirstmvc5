namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pokemon1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMPokemon",
                c => new
                    {
                        PokemonId = c.Int(nullable: false, identity: true),
                        PokemonNo = c.Int(nullable: false),
                        PokemonName = c.String(nullable: false, maxLength: 40),
                        PokemonAttr_1 = c.Int(nullable: false),
                        PokemonAttr_2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PokemonId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PMPokemon");
        }
    }
}
