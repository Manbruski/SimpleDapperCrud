using DapperORM.Models;
using System.Collections.Generic;

namespace DapperORM.Interface
{
    public interface IUsuario
    {
        List<Usuario> GetAll();
        Usuario Find(int id);
        Usuario Add(Usuario us);
        Usuario Update(Usuario us);
        void Remove(int id);
    }
}