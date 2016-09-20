using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using mvc5_first.Filter;
using ViewModel;

namespace mvc5_first.Controllers
{
    public class PokeController : Controller
    {
        //
        // GET: /Poke/
        public ActionResult Index()
        {
            PokeAttrType a = PokeAttrType.地;
            PokeAttrType b;
            int c = 3;
            PokeEntity pokeEntity = new PokeEntity();
            if (Request["a"] != null)
            {
                a = (PokeAttrType)Enum.Parse(typeof(PokeAttrType), Request["a"]);
            }
            if (Request["b"] != null)
            {
                b = (PokeAttrType)Enum.Parse(typeof(PokeAttrType), Request["b"]);
            }
            else
            {
                b = a;
            }
            if (Request["c"] != null)
            {
                int.TryParse(Request["c"], out c);
            }
            PokemonBusinessLayer poke = new PokemonBusinessLayer(a, b, c);
            pokeEntity = poke.CalCombScore();
            return View("Pokemon", pokeEntity);
        }

        [Authorize]
        [HeaderFooterFilter]
        public ActionResult GetView()
        {
            var pokeBal = new PokemonBusinessLayer();
            var pokemons = pokeBal.GetPokemonEntityList();
            var pokelistViewModel = new PokemonListViewModel();

            foreach (PokemonEntity pokemon in pokemons)
            {
                pokelistViewModel.PokemonList.Add(new PokemonViewModel()
                {
                    PokemonId = pokemon.PokemonNo,
                    PokemonName = pokemon.PokemonName
                });
            }

            return View("MyView", pokelistViewModel);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult CreatePokemon()
        {
            BaseViewModel bvm = new BaseViewModel();
            return View("NewPoke", bvm);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SavePokemon(PokemonEntity entity)
        {
            var pokeBal = new PokemonBusinessLayer();
            pokeBal.AddPoke(entity);
            return GetView();
        }


    }

}