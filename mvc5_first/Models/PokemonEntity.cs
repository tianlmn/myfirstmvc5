using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace mvc5_first.Models
{
    [Table("PKPokemon")]
    public class PokemonEntity
    {
        /// <summary>
        /// 口袋妖怪id
        /// </summary>
        [Key]
        public int PokemonId { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "口袋编号超出范围")]
        public int PokemonNo { get; set; }

        [StringLength(40)]
        [Required]
        [Column(TypeName = "nvarchar")]

        public string PokemonName { get; set; }

        [Required]
        [Range(0,17,ErrorMessage = "属性超出范围")]
        public int PokemonAttr_1 { get; set; }

        [Required]
        [Range(0, 17, ErrorMessage = "属性超出范围")]
        public int PokemonAttr_2 { get; set; }

    }
}