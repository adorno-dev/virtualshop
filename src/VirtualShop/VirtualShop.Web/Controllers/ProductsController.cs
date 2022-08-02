using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualShop.Web.Models;
using VirtualShop.Web.Roles;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        private async Task<string> GetAccessToken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            return token is not null ? token : "";
        }

        public ProductsController(IProductService service, ICategoryService categoryService)
        {
            this.productService = service;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>?>> Index()
        {
            var products = await productService.GetAllProducts(await GetAccessToken());

            return products is null ?
                View("Error") :
                View(products);
        }

        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");

            return View();
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProduct(product, await GetAccessToken());

                if (response is not null)
                    return RedirectToAction(nameof(Index));
            }
            else ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");
            
            return View(product);
        }

        [HttpGet("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");

            var product = await productService.FindProductById(id, await GetAccessToken());

            return product is null ?
                View("Error") :
                View(product);
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.UpdateProduct(product, await GetAccessToken());

                if (response is not null)
                    return RedirectToAction(nameof(Index));
            }
            else ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");
            
            return View(product);
        }

        [HttpGet("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.FindProductById(id, await GetAccessToken());

            return product is null ?
                View("Error") :
                View(product);
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await productService.DeleteProduct(id, await GetAccessToken());

            return !product ?
                View("Error") :
                RedirectToAction("Index");
        }
    }
}