using DapperORM.Models;
using DapperORM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperORM.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository _repo;

        #region "Acciones para el mantenimiento de Usuario"
        /**
        Preguntamos si _repo esta null para igualarlo a un nuevo UsuarioRepository()
        para poder utilizar las acciones que tiene UsuarioRepository()
        **/
        private UsuarioRepository Repo
        {
            get
            {
                if (_repo == null)
                {
                    _repo = new UsuarioRepository();
                }
                return _repo;
            }
        }
        //Retorna al index con todos los usuarios registrados en la bd
        public ActionResult Index()
        {
            return View("Index", Repo.GetAll());
        }
        //Retorna la vista de Create
        public ActionResult Create()
        {
            return View("Create");
        }
        /**
        Se crea un usuario llamando a Repo.Add y pasandole por parametro 
        us, luego redirecciona al Index de Empleado
            **/
        [HttpPost]
        public ActionResult Create(Usuario us)
        {
            try
            {
                Repo.Add(us);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /**
        Editar pasa el id por parametro a Repo.Find 
        a la vista de Edit**/
        public ActionResult Edit(int id)
        {
            return View("Edit", Repo.Find(id));
        }
        /**
        Cuando editamos, ocupamos pasar el id de Edit, el cual contiene el
        id del Usuario a modificar**/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario us)
        {
            if (ModelState.IsValid)
            {
                Repo.Update(us);
            }
            return View();
        }
        /**
        Utilizamos de UsuarioRepository el remove, pasandole por parametro el id
        y retornando a la vista principal**/    
        public ActionResult Delete(int id)
        {
            Repo.Remove(id);
            return View("Index", Repo.GetAll());
        }
    }
    #endregion

}