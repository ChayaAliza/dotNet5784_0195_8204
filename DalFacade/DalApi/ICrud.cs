using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create(T de1);
        T? Read(int id);
        List<T> ReadAll();
        void Update(T de1);
        void Delete(int id);
    }
}
