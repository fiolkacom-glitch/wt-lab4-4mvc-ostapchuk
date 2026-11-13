using System.ComponentModel.DataAnnotations;

namespace wt_lab4_4mvc_ostapchuk.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название")]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Категория")]
    public string Category { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Цена должна быть положительной")]
    [Display(Name = "Цена (руб.)")]
    public decimal Price { get; set; }

    [Display(Name = "Доступен")]
    public bool IsAvailable { get; set; } = true;
}