using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Producks.Undercutter;
using Producks.Data;
using Producks.Web.ViewModels;
using Producks.Repo;
using Producks.Repo.ViewModels;

namespace Producks.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreDb _context; // StoreDb local database
        private IEnumerable<string> repoBrands;
        private IEnumerable<string> repoCategories;

        public StoreController(StoreDb context)
        {
            _context = context;
            RepoApi rp = new RepoApi(context);
            repoBrands = rp.GetBrandsName();
            repoCategories = rp.GetCategoriesName();
        }

        

        // GET: Categories
        public ActionResult Index(string catName = null, string brandName = null)
        {
            //filtering categories and brand
            ViewBag.Categories = new SelectList(repoCategories);
            ViewBag.Brands = new SelectList(repoBrands);

            RepoApi rp = new RepoApi(_context);
            var products = rp.GetAllProducts();

                        
            
            if (catName != null && catName != "") {
                products = products.Where(p => p.Category == catName);
            }

            if (brandName != null && brandName != "")
            {
                products = products.Where(p => p.Brand == brandName);
            }


            return View(products);
        }
               
    }
}
