using System;
using Volo.Abp.Application.Dtos;
using mini_ecommerce_backend.Entities.Books;

namespace mini_ecommerce_backend.Services.Dtos.Books;

public class BookDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }
}