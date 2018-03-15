using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio_3.Clases;
using Laboratorio_3.Models;
using EstructurasDeDatos;
using System.Diagnostics;

namespace Laboratorio_3.Controllers
{
    public class PartidoController : Controller
    {
        // GET: Partido
        public ActionResult IndexPartido()
        {
            return View();
        }

        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Data.Instance.partidosAVL.Insert(new Partido
                {
                    noPartido = Convert.ToInt16(collection["Número de partido"]),
                    fechaPartido = Convert.ToDateTime(collection["Fecha de partido"]),
                    Grupo = collection["Grupo"],
                    pais1 = collection["Pais No. 1"],
                    pais2 = collection["Pais No. 2"],
                    estadio = collection["Estadio"]
                });
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult Delete(int id)
        {
            var partido = Data.Instance.listaPartidos.Find(x => x.noPartido == id);
            return View(partido);
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Partido partido = Data.Instance.listaPartidos.Find(x => x.noPartido == id);
                Data.Instance.partidosAVL.Eliminar(partido);
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadPartido()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPartido(HttpPostedFileBase file)
        {
            TreeAVL<Partido> temp = new TreeAVL<Partido>();
            List<Partido> tempL = new List<Partido>();
            try
            {
                if (!file.FileName.EndsWith(".json"))
                    return View();
                if (file.ContentLength > 0)
                {
                    var json = new JsonConverter<Partido>();
                    AVLTreeNode<Partido> raiz = json.datosJson(file.InputStream);
                    temp.Root = raiz;
                    tempL = temp.Orders("PreOrder");
                    foreach(Partido p in tempL)
                    {
                        Data.Instance.partidosAVL.Insert(p);
                    }
                    Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");
                    return RedirectToAction("IndexPartido");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
    }
}
