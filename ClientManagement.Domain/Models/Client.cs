namespace Clientmanagement.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();

}