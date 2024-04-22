
namespace WebApp3.Models;

public class Order
{
    public int idOrder { get; set; }
    public int idProduct { get; set; }
    public int amount { get; set; }
    public String createdAt { get; set; }
    public String fullFilled { get; set; }
    
}