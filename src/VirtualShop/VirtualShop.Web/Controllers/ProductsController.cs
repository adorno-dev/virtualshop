using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualShop.Web.Models;
using VirtualShop.Web.Roles;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService service, ICategoryService categoryService)
        {
            this.productService = service;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>?>> Index()
        {
            var products = await productService.GetAllProducts();

            return products is null ?
                View("Error") :
                View(products);
        }

        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(), "Id", "Name");

            return View();
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProduct(product);

                if (response is not null)
                    return RedirectToAction(nameof(Index));
            }
            else ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(), "Id", "Name");
            
            return View(product);
        }

        [HttpGet("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(), "Id", "Name");

            var product = await productService.FindProductById(id);

            return product is null ?
                View("Error") :
                View(product);
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.UpdateProduct(product);

                if (response is not null)
                    return RedirectToAction(nameof(Index));
            }
            else ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(), "Id", "Name");
            
            return View(product);
        }

        [HttpGet("DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.FindProductById(id);

            return product is null ?
                View("Error") :
                View(product);
        }

        [HttpPost("DeleteProduct")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await productService.DeleteProduct(id);

            return !product ?
                View("Error") :
                RedirectToAction("Index");
        }
    }
}