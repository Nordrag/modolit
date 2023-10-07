using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//representation of a payment type
public class PaymentTypes : BaseModel
{
    public string TypeName { get; set; }
    public bool IsActive { get; set; }
}

