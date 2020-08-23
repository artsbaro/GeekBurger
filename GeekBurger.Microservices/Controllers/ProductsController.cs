using GeekBurguer.Products.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Products.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            var paulistaStore = "Paulista";
            var morumbiStore = "Morumbi";

            var beef = new Item { ItemId = Guid.NewGuid(), Name = "beef" };
            var pork = new Item { ItemId = Guid.NewGuid(), Name = "pork" };
            var mustard = new Item { ItemId = Guid.NewGuid(), Name = "mustard" };
            var ketchup = new Item { ItemId = Guid.NewGuid(), Name = "ketchup" };
            var bread = new Item { ItemId = Guid.NewGuid(), Name = "bread" };
            var wBread = new Item { ItemId = Guid.NewGuid(), Name = "whole bread" };

            Products = new List<Product>()
            {
                new Product { ProductId = Guid.NewGuid(), Name = "Darth Bacon",
                    Image = "hamb1.png", StoreName = paulistaStore,
                    Items = new List<Item> {beef, ketchup, bread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Cap. Spork",
                    Image = "hamb2.png", StoreName = paulistaStore,
                    Items = new List<Item> { pork, mustard, wBread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Beef Turner",
                    Image = "hamb3.png", StoreName = morumbiStore,
                    Items = new List<Item> {beef, mustard, bread }
                }
            };

        }

        private IList<Product> Products = new List<Product>();

        [HttpGet("{storeName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetProductsByStoreName(string storeName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(storeName))
                    return BadRequest(storeName);

                var productsByStore = await Task.Run(() => Products.Where(product => storeName.ToUpper().Equals(product.StoreName.ToUpper())).ToList());

                if (productsByStore.Count == 0)
                    return NotFound();

                return Ok(productsByStore);
            }
            catch { return StatusCode(500); }
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await Task.Run(() => Products));
        }
    }
}
