using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//contains the main functions related to shopping
public class ShoppingService : BaseService<Stock>
{
    public ShoppingService(List<Stock> Data) : base(Data)
    {
    }

    //gets the available stock from the cart
    public List<Stock> GetAvailable(List<CartItem> itemsToBuy)
    {
        var productIds = itemsToBuy.Select(item => item.Product.Id);

        // Use a join to get the matching Stock items
        var matchingStock = from stock in dbSet
                            join cartItem in itemsToBuy on stock.ProductId equals cartItem.Product.Id
                            where stock.AmountOnStock > 0
                            select stock;    
        
        return matchingStock.ToList();     
    }

    //corrects the shopping cart with the amount available
    public List<CartItem> GetPurchase(List<Stock> available, List<CartItem> itemsToBuy)
    {
        List<CartItem> items = new();
        foreach (var onStock in available)
        {
            CartItem item;
            //this should not be null since the previous function removes the missing products
            var toBuy = itemsToBuy.FirstOrDefault(x => x.Product.Id == onStock.ProductId);
            if (toBuy.Amount >= onStock.AmountOnStock)
            {
                //if we want to buy more than whats available, add the stock quantity
                item = new(toBuy.Product, onStock.AmountOnStock);
            }
            else
            {
                //else add how many we want to buy
                item = new(toBuy.Product, toBuy.Amount);
            }
            items.Add(item);
        }
        return items;
    }

    //debatable where to put this, it could be in the shopping cart class
    public float GetTotalPrice(List<CartItem> items)
    {
        var prices = items.Select(x => x.Product.ProductPrice * x.Amount).ToArray();
        return prices.Sum();
    }

    public bool CheckBalance(float amountToPay, float customerBalance)
    {
        return amountToPay <= customerBalance;
    }

    //print the invoice / receipt
    public string PrintPurchase(List<CartItem> items, PaymentTypes payment)
    {
        //for now I just print the sum
        //this would be a good place to reduce stocks by the amount purchased if the payment is successful
        //make sure to get the products in a single query instead of a loop, select * from stock where productId in (list) ->
        //update values in memory, then use dbSet.ChangeRange, that way we only make 2 queries per transaction
        //can also build an IQueryable<Stock> when using EF core
        var prices = items.Select(x => x.Product.ProductPrice * x.Amount).ToArray();
        return $"{prices.Sum()} {payment.TypeName}";
    }
}

