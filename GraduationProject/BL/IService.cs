using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IService<T>
    {
        IEnumerable GetAll();
        T GetById(string id);
        int Update(T model, string id);
        int Delete(string id);
    }
}
