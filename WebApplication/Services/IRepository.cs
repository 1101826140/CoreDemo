using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();


        T Get(int id);

        int Create(T t);
    }
}
