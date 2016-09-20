using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using mvc5_first.Filter;
using ViewModel;

namespace mvc5_first.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int tid1 = Thread.CurrentThread.ManagedThreadId;
            List<PokemonEntity> pokemonList = new List<PokemonEntity>();
            Task<List<PokemonEntity>> task = await Task.Factory.StartNew(() => GetPokemons(model));

            int tid2 = Thread.CurrentThread.ManagedThreadId;

            PokemonBusinessLayer bal = new PokemonBusinessLayer();
            bal.UploadPokemon(task.Result);
            
            return RedirectToAction("GetView", "Poke");
        }

        private async Task<List<PokemonEntity>> GetPokemons(FileUploadViewModel model)
        {
            List<PokemonEntity> pokemonList = new List<PokemonEntity>();
            Thread.Sleep(2000);
            int t3 = Thread.CurrentThread.ManagedThreadId;
            await Task.Factory.StartNew(() => { Thread.Sleep(8000); });
            int t4 = Thread.CurrentThread.ManagedThreadId;
            StreamReader sr = new StreamReader(model.fileUpload.InputStream, System.Text.Encoding.Default);
            string srLine = string.Empty;
            while (!sr.EndOfStream)
            {
                srLine = sr.ReadLine();
                string[] items = srLine.Split(new[] {','});
                PokemonEntity entity = new PokemonEntity();
                entity.PokemonNo = int.Parse(items[0]);
                entity.PokemonName = items[1];
                entity.PokemonAttr_1 = int.Parse(items[2]);
                entity.PokemonAttr_2 = int.Parse(items[3]);

                pokemonList.Add(entity);
            }
            return pokemonList;
        }
    }
}