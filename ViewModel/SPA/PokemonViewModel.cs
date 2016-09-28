using System.Collections.Generic;

namespace ViewModel.SPA
{
    public class PokemonViewModel
    {
        public int PokemonId { get; set; }

        public string PokemonName { get; set; }

    }



    public class PokemonListViewModel : MainViewModel
    {
        public PokemonListViewModel()
        {
            PokemonList = new List<PokemonViewModel>();
        }

        public List<PokemonViewModel> PokemonList { get; set; }

    }
}