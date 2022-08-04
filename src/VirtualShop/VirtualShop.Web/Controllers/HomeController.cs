using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            this.logger = logger;
            this.productService = productService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProducts();

            return products is null ?
                View("Error") :
                View(products);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (token is null)
                return BadRequest("Invalid token.");

            var product = await productService.FindProductById(id);

            return product is null ?
                View("Error") :
                View(product);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetailsPost(ProductViewModel productViewModel)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (token is null)
                return BadRequest("Invalid token.");

            var cart = new CartViewModel { CartHeader = new () { UserId = User.Claims.First(w => w.Type.Equals("sub")).Value } };

            var cartItem = new CartItemViewModel {
                Product = await productService.FindProductById(productViewModel.Id),
                ProductId = productViewModel.Id,
                Quantity = productViewModel.Quantity
            };

            var cartItemsViewModel = new List<CartItemViewModel>();
            cartItemsViewModel.Add(cartItem);
            cart.CartItems = cartItemsViewModel;

            var cartViewModel = await cartService.AddItemToCartAsync(cart);

            return cartViewModel is null ?
                View(cartViewModel):
                RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout() => SignOut("Cookies", "oidc");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}