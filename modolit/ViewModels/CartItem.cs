using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//representation for what and how much we want to buy
public class CartItem
{
    public Product Product { get; set; }
    public int Amount { get; set; }

    public CartItem(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }
}

