using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//representation of a product sold by the shop
public class Product : BaseModel
{
    public string ProductName { get; set; }
    //some currencies use floating points, and also percentage have greater accuracy with a float
    public float ProductPrice { get; set; }
}

