using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_ostapchuk.Models;

namespace wt_lab4_4mvc_ostapchuk.Controllers;

public class ProductController : Controller
{
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Абонемент Годовой", Category = "Абонементы", Price = 500, IsAvailable = true },
        new Product { Id = 2, Name = "Абонемент Месячный", Category = "Абонементы", Price = 50, IsAvailable = true },
        new Product { Id = 3, Name = "Персональная тренировка", Category = "Услуги", Price = 80, IsAvailable = false }
    };

    public IActionResult Index()
    {
        return View(_products);
    }

    public IActionResult Details(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    // GET: /Product/Create (открывает форму)
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Product/Create (сохраняет форму)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            product.Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Category = product.Category;
                existing.Price = product.Price;
                existing.IsAvailable = product.IsAvailable;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
}