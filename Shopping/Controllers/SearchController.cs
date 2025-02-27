using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Shopping.Repositories;

namespace Shopping.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductRepository _productRepository;

        public SearchController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> SearchPage(string query, string category, int? minYear, int? maxYear, double? maxPrice, bool isEco = false)
        {
            IEnumerable<Product> results = await _productRepository.GetAllProducts();

            Debug.WriteLine($"Category: {category}");
            if (!string.IsNullOrEmpty(query))
            {
                if (category == "Name")
                {
                    results = results.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
                }
                else if (category == "Category")
                {
                    results = results.Where(p => p.Category.Contains(query, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (minYear.HasValue)
            {
                results = results.Where(p => p.Year >= minYear);
            }
            if (maxYear.HasValue)
            {
                results = results.Where(p => p.Year <= maxYear);
            }
            if (isEco)
            {
                results = results.Where(p => p.IsEco);
            }
            if (maxPrice.HasValue)
            {
                results = results.Where(p => p.Price <= maxPrice);
            }

            ViewData["Category"] = category;
            ViewData["Query"] = query;
            ViewData["MaxYear"] = maxYear;
            ViewData["MinYear"] = minYear;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["isEco"] = isEco;
            return View(results);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
