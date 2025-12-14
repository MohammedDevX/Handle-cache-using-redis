using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_service.Data;
using Products_service.DTOS;
using Products_service.Mappers;
using Products_service.Models;
using Products_service.Services;
using Products_service.Services.Caching;
using Products_service.ViewModels;

namespace Products_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private AppDbContext context;
        private UserService userAC;
        private IRedisCacheService cache;
        public CatalogueController(AppDbContext context, UserService userAC, IRedisCacheService cache)
        {
            this.context = context;
            this.userAC = userAC;
            this.cache = cache;
        }

        [HttpGet("index/{id}")]
        public async Task<ActionResult<Produit>> Index(int id)
        {
            Produit? product = await context.Produit.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            UserDTO? user = await userAC.GetUserAsync(product.AdminId);
            ProduitDTO produitD = ProduitMP.ToProduitDTO(product, user);
            return Ok(produitD);
        }

        [HttpGet("index")]
        public async Task<ActionResult<Produit>> Index()
        {
            // 1) Si pas de donnes personalises, comme notre cas touts les users vont avoire la meme liste de prods
            //string cachingKey = "catalogue";

            // 2) Si Vous voulez cachez des donnes personalises par ex : afficher pour chaque user les listes 
            // des produits favoris
            // donc dans le frontend quand on fecth cette action en doit envoyer dans Headers le UserId
            var userId = Request.Headers["UserId"];
            var cachingKey = $"catalogue:{userId}";
            var cachedProducts = await cache.GetData<List<ProduitDTO>>(cachingKey);
            if (cachedProducts != null)
            {
                return Ok(cachedProducts);
            }

            List<Produit> products = await context.Produit.Include(c => c.Category).ToListAsync();
            if (!products.Any())
            {
                return NotFound();
            }

            var produits = new List<ProduitDTO>();

            foreach (var prod in products)
            {
                var user = await userAC.GetUserAsync(prod.AdminId);
                if (user == null)
                {
                    return StatusCode(502, "Error user service!");
                }

                produits.Add(new ProduitDTO
                {
                    Id = prod.Id,
                    Libelle = prod.Libelle,
                    Prix = prod.Prix,
                    Stock = prod.Stock,
                    AdminId = prod.AdminId,
                    NomAdmin = user.NomUser,
                    CategoryId = prod.CategoryId,
                    LibelleCategory = prod.Category.Libelle
                });
            }

            await cache.SetData(cachingKey, produits);
            return Ok(produits);
        }

        [HttpGet("category")]
        public async Task<ActionResult<Category>> CategoryList()
        {
            List<Category> categories = await context.Category.ToListAsync();
            if (!categories.Any())
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<Category>> OneCategory(int id)
        {
            Category category = await context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult> AddCategory(CategoryVM categoryvm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Category category = ProduitMP.CategoryVMToCategory(categoryvm);

            await context.Category.AddAsync(category);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(OneCategory), new {Id = category.Id}, category);
        }

        [HttpPost("add-produit")]
        public async Task<ActionResult<Produit>> AddProduct(ProduitVM prodvm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Produit produit = ProduitMP.ProduitVMToProduit(prodvm);
            await context.Produit.AddAsync(produit);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Index), new { Id = produit.Id }, produit);
        }

        /*
            Actions update and delete products by admin, need kafka !!! 
        */


    }
}
