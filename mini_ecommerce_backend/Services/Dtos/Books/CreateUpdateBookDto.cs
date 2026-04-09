using System;
using System.ComponentModel.DataAnnotations;
using mini_ecommerce_backend.Entities.Books;

namespace mini_ecommerce_backend.Services.Dtos.Books;

public class CreateUpdateBookDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public BookType Type { get; set; } = BookType.Undefined;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    public float Price { get; set; }
}