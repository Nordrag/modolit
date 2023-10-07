
//make some products to test
Product[] testProducts = new Product[] {
    new Product{ Id =1, ProductName = "Alma", ProductPrice = 200 },
    new Product{ Id =2, ProductName = "Kenyér", ProductPrice = 500 },
    new Product{ Id =3, ProductName = "Sajt", ProductPrice = 600 }

    //add others
};

//add products to cart
ShoppingCart.ItemsToBuy = new List<CartItem>
{
    //params: what product to buy, and how many
    new CartItem(testProducts[0], 2), //trying to buy 2 apples, and 1 bread
    new CartItem(testProducts[1], 1),
};

//service for shopping logic, the parameter expects the shop's stock, which is just hard coded here
ShoppingService shoppingService = new(new List<Stock> 
{ 
    //parameters: how many on stock, the primary key(see on top) and the product (mock navigation property)
    new Stock(2, 1, testProducts[0]), //we have 2 apples on stock 2 bread and 3 cheeses
    new Stock(2, 2, testProducts[1]),
    new Stock(3, 3, testProducts[2])
});

//payment service, initialized with only cash payment available
PaymentTypeService paymentTypeService = new PaymentTypeService(new List<PaymentTypes> {
    new PaymentTypes{  IsActive = true, TypeName = "Cash" },
    new PaymentTypes{  IsActive = false, TypeName = "Credit Card" },
});

//the customers cash
float customerBalance = 20000;

//shortcut
var shoppingList = ShoppingCart.ItemsToBuy;
//get available payments
var payments = paymentTypeService.GetAvailable();
//get available stock from the cart
var onStock = shoppingService.GetAvailable(shoppingList);
//filter the shopping list, and only add the amount that is on stock
var purchaseList = shoppingService.GetPurchase(onStock, shoppingList);
//does the customer have enough money
var canPay = shoppingService.CheckBalance(shoppingService.GetTotalPrice(purchaseList),customerBalance);

//it should print 900, since there are at least 2 apples and 1 bread on stock

if (canPay)
{
    //would normally offer a payment type selection through the gui, for now I just pass in the first, which is cash
    var result = shoppingService.PrintPurchase(purchaseList, payments.First());
    Console.WriteLine(result);
}
else
{
    Console.WriteLine("The customer does not have enough money");
}

