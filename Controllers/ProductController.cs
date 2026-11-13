using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_ostapchuk.Models;

namespace wt_lab4_4mvc_ostapchuk.Controllers;

public class ProductController : Controller
{
    // Данные в памяти (Спортивный клуб)
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Абонемент Годовой", Category = "Абонементы", Price = 500, IsAvailable = true },
        new Product { Id = 2, Name = "Абонемент Месячный", Category = "Абонементы", Price = 50, IsAvailable = true },
        new Product { Id = 3, Name = "Персональная тренировка", Category = "Услуги", Price = 80, IsAvailable = false }
    };

    // 1. СПИСОК
    public IActionResult Index()
    {
        return View(_products);
    }

    // 2. ПОДРОБНЕЕ
    public IActionResult Details(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    // 3. ДОБАВЛЕНИЕ (GET - показать форму)
    public IActionResult Create()
    {
        return View();
    }

    // 3. ДОБАВЛЕНИЕ (POST - сохранить)
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

    // 4. РЕДАКТИРОВАНИЕ (GET - показать форму)
    public IActionResult Edit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    // 4. РЕДАКТИРОВАНИЕ (POST - сохранить изменения)
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