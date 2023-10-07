using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//an abstract service with the CRUD operations
public abstract class BaseService<T>
    where T : BaseModel
{
    //generic sql table representation
    protected List<T> dbSet;


    public BaseService(List<T> Data)
    {
        dbSet = Data;
    }

    public List<T> GetList() { return dbSet; }

    public T GetById(int id)
    {
        return dbSet.FirstOrDefault(x => x.Id == id);
    }

    //could also return a string containing the error message
    //there are also Change range operations is EF core, with a list as parameter
    public bool ChangeEntry(T entry)
    {
        try
        {
            bool result = false;
            var e = GetById(entry.Id);
            if (e != null)
            {
                e = entry;
                //await SaveChanges()
                result = true;
            }
            return result;
        }
        catch (Exception)
        {
            return false;
        }
       
    }

    public void Delete(T entry)
    {
        dbSet.Remove(entry);
    }

    public void Insert(T entry)
    {       
        dbSet.Add(entry);   
    }

    //would be called in the functions above, or manually
    public async Task SaveChanges()
    {
        //await dbSet.SaveChangesAsync();
    }
}

