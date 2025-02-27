using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Shopping.Extensions;
using Shopping.Models;
using Microsoft.Extensions.Logging;
public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;

    public CartController(ILogger<CartController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public IActionResult AddToCart(int productId, string productName, decimal price)
    {
        _logger.LogInformation("1AddToCart invoked with parameters: ProductId = {ProductId}, ProductName = {ProductName}, Price = {Price}",
        productId, productName, price);
        var cart = HttpContext.Session.GetCart();
        var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = 1
            });
        }

        _logger.LogInformation("Cart", cart);

        HttpContext.Session.SetCart(cart);

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetCart();
        return View(cart);
    }
}
