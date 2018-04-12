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
        public ActionResult Home()
        {
            return View();
        }

        // GET: Partido
        public ActionResult IndexPartido()
        {
            Data.Instance.partidosAVL.dateOrNumber = true;
            return View(Data.Instance.listaPartidos);
        }

        // GET: Partido
        public ActionResult IndexPartidoFecha()
        {
            Data.Instance.partidosAVL.dateOrNumber = false;
            return View(Data.Instance.listaPartidos);
        }

        // GET: Partido/Details/5
        public ActionResult DetailsPartido(int id)
        {
            var partido = Data.Instance.listaPartidos.Where(x => x.noPartido == id).FirstOrDefault();
            return View(partido);
        }

        // GET: Partido/Details/5
        public ActionResult DetailsPartidoFecha(int id)
        {
            var partido = Data.Instance.listaPartidos.Where(x => x.fechaPartido ==  Convert.ToString(id)).FirstOrDefault();
            return View(partido);
        }

        // GET: Partido/Create
        public ActionResult CreateFechaPartido()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult CreateFechaPartido(FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Data.Instance.partidosAVL.Insert(new Partido
                {
                    noPartido = Convert.ToInt16(collection["Número de partido"]),
                    fechaPartido =  (collection["Fecha de partido"]),
                    grupo = collection["Grupo"],
                    pais1 = collection["pais1"],
                    pais2 = collection["pais2"],
                    estadio = collection["Estadio"]
                });
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Creating a new item 'Partido' to Data.Instance.partidosAVL from Create view", ts);

                return RedirectToAction("IndexPartidoFecha");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Create
        public ActionResult CreateNoPartido()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult CreateNoPartido(FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Data.Instance.partidosAVL.Insert(new Partido
                {
                    noPartido = Convert.ToInt16(collection["Número de partido"]),
                    fechaPartido =  (collection["Fecha de partido"]),
                    grupo = collection["Grupo"],
                    pais1 = collection["pais1"],
                    pais2 = collection["pais2"],
                    estadio = collection["Estadio"]
                });
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Creating a new item 'Partido' to Data.Instance.partidosAVL from Create view", ts);

                return RedirectToAction("IndexPartido");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult DeleteFechaPartido(int id)
        {
            var partido = Data.Instance.listaPartidos.Find(x => x.fechaPartido ==  Convert.ToString(id));
            return View(partido);
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult DeleteFechaPartido(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // TODO: Add delete logic here
                Partido partido = Data.Instance.listaPartidos.Find(x => x.fechaPartido ==  Convert.ToString(id));
                Data.Instance.partidosAVL.Eliminar(partido);
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Deleting a new item 'Partido' to Data.Instance.partidosAVL from Delete view", ts);

                return RedirectToAction("IndexPartidoFecha");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult DeletePartido(int id)
        {
            var partido = Data.Instance.listaPartidos.Find(x => x.noPartido == id);
            return View(partido);
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult DeletePartido(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // TODO: Add delete logic here
                Partido partido = Data.Instance.listaPartidos.Find(x => x.noPartido == id);
                Data.Instance.partidosAVL.Eliminar(partido);
                Data.Instance.listaPartidos = Data.Instance.partidosAVL.Orders("InOrder");

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Deleting a new item 'Partido' to Data.Instance.partidosAVL from Delete view", ts);

                return RedirectToAction("IndexPartido");
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
