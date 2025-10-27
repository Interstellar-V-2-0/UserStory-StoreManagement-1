namespace StoreManagement.Api.DTOs;

using StoreManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;

public class CustomerReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public CustomerReadDto(Customer customer)
    {
        Id = customer.Id;
        FirstName = customer.FirstName;
        LastName = customer.LastName;
        Email = customer.Email;
    }
}

public class CustomerCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Customer ToCustomer()
    {
        return new Customer
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email
        };
    }
}

public class CustomerUpdateDto
{
    public int Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Customer ToCustomer()
    {
        return new Customer
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email
        };
    }
}