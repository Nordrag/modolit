using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//service for payments
public class PaymentTypeService : BaseService<PaymentTypes>
{
    public PaymentTypeService(List<PaymentTypes> Data) : base(Data)
    {
    }

    //gets the available payments
    public List<PaymentTypes> GetAvailable()
    {
        return dbSet.Where(x => x.IsActive).ToList();
    }

    public bool Toggle(PaymentTypes type)
    {
        type.IsActive = !type.IsActive;
        if (!type.IsActive && dbSet.Count(x => !x.IsActive) >= dbSet.Count -1)
        {
            //cannot disable all the payments
            return false;
        }    
        return ChangeEntry(type);
    }
}

