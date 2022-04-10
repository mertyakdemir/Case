using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseApp.DataLayer.Abstract
{
    public interface IRepositoryBase<T>
    {
        T GetOne(int id);

        List<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
