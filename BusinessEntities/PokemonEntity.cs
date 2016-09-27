using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    [Table("PMPokemon")]
    public class PokemonEntity
    {
        /// <summary>
        /// 口袋妖怪id
        /// </summary>
        [Key]
        public int PokemonId { get; set; }

        [Required]
        public int PokemonNo { get; set; }

        [StringLength(40)]
        [Required]
        public string PokemonName { get; set; }

        [Required]
        public int PokemonAttr_1 { get; set; }

        [Required]
        public int PokemonAttr_2 { get; set; }

    }
}