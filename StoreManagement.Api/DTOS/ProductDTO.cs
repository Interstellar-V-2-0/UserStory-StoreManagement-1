namespace StoreManagement.Api.DTOs;

using StoreManagement.Domain.Models;

public class ProductReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public ProductReadDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
    }
}

public class ProductCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            Name = Name,
            Description = Description,
            Price = Price
        };
    }
}

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price
        };
    }
}