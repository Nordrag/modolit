using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//warehouse stock
public class Stock : BaseModel
{
    public int AmountOnStock { get; set; }
    //fake navigation property
    public Product Product { get; set; }
    //foreign key for the product class
    public int ProductId { get; set; }

    public Stock()
    {
        
    }

    //ctor for simulation a db entity
    public Stock(int onStock, int prodId, Product prod)
    {
        AmountOnStock = onStock;
        Product = prod;
        ProductId = prodId;
    }
}
