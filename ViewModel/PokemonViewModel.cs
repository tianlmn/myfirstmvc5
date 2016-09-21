using System.Collections.Generic;

namespace ViewModel
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