using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperORM.Interface;
using Npgsql;
using DapperORM.Models;
using Dapper;
using System.Data;

namespace DapperORM.Repository
{
    public class UsuarioRepository : IUsuario
    {
        //Conexion a la base de datos
        private NpgsqlConnection _db = new NpgsqlConnection("Server = localhost; Database=postgres; User Id = postgres; Password=123;");

        /**
        Lista con todos los usuarios para cargarlos luego en una vista,
        retornando una consulta(en este caso al utilizar dapper se utiliza Query)
        que en este caso es de una lista de los usuarios
         **/
        public List<Usuario> GetAll()
        {
            return this._db.Query<Usuario>("SELECT id, nombre, apellido, direccion, fecha_nacimiento FROM \"Usuario\"").ToList();
        }

        /**
        Metodo en la que por medio de una consulta, insertamos un nuevo
        usuario, en la cual ejecutamos la consulta y retornando un nuevo Usuario
            **/
        public Usuario Add(Usuario us)
        {
            var sqlQuery = "INSERT INTO \"Usuario\" (nombre, apellido, direccion, fecha_nacimiento) VALUES(@Nombre, @Apellido, @Direccion, @Fecha_Nacimiento);";
            this._db.Execute(sqlQuery, us);
            return us;
        }

        /**
        Metodo que busca los datos del usuario pasandoles por parametro un id
        para que luego pueda ser modificado
             **/
        public Usuario Find(int id)
        {
            return this._db.Query<Usuario>("SELECT nombre, apellido, direccion, fecha_nacimiento FROM \"Usuario\" WHERE id = @id", new { id }).SingleOrDefault();
        }


        /**
        Al pasarle el find con el id, me va a mandar a una vista con los datos
        seteados a los inputs para poder modificarlos y luego actualizarlos
            **/
        public Usuario Update(Usuario us)
        {
            var sqlQuery =
            "UPDATE \"Usuario\" SET nombre = @Nombre, apellido = @Apellido, direccion = @Direccion, fecha_nacimiento = @Fecha_Nacimiento WHERE id = @id";
            this._db.Execute(sqlQuery, us);
            return us;
        }
        /**
        Metodo que elimina a un usuario pasandole por parametro el id
        **/
        public void Remove(int id)
        {
            var sqlQuery = " DELETE FROM \"Usuario\" where id =@id";
            this._db.Execute(sqlQuery, new { id });
        }

    }
}