using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.ViewModels
{
    public class PokemonViewModel
    {
        public int PokemonId { get; set; }

        public string PokemonName { get; set; }

    }



    public class PokemonListViewModel : BaseViewModel
    {
        public PokemonListViewModel()
        {
            PokemonList = new List<PokemonViewModel>();
        }

        public List<PokemonViewModel> PokemonList { get; set; }

    }
}