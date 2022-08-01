using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualShop.Web.Models;
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
    }
}