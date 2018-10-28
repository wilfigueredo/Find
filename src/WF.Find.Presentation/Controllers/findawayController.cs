using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WF.Find.Presentation.Controllers
{
    public class findawayController : Controller
    {
        public ActionResult easy()
        {
            var caminho = ObterCaminhoEasy(null, 0);
            return View();
        }

        public ActionResult normal()
        {
            var caminho = ObterCaminho(5, 3);
            Session["caminho"] = caminho;
            return View();
        }

        public ActionResult hard()
        {
            var caminho = ObterCaminho(7, 4);
            Session["caminho"] = caminho;
            return View();
        }

        public List<int> ObterCaminho(int totStep, int option)
        {
            List<int> caminho = new List<int>();
            Random rand = new Random();
            int coluna = 0;
            int step = 0;
            int colunaMaxima = option;
            int colunaMinima = 0;

            while (step < totStep)
            {
                if (step == 0)
                {
                    coluna = rand.Next(colunaMinima, colunaMaxima + 1);
                    caminho.Add(coluna);
                    step++;
                }
                else
                {
                    if (caminho.ElementAt(step - 1) == colunaMinima)
                    {
                        coluna = rand.Next(0, colunaMinima + 1);
                        caminho.Add(coluna);
                        step++;
                    }
                    else if (caminho.ElementAt(step - 1) == colunaMaxima)
                    {
                        coluna = rand.Next(colunaMaxima - 1, colunaMaxima + 1);
                        caminho.Add(coluna);
                        step++;
                    }
                    else if (caminho.ElementAt(step - 1) != colunaMaxima && caminho.ElementAt(step - 1) != colunaMinima)
                    {
                        coluna = rand.Next(caminho.ElementAt(step - 1) - 1, caminho.ElementAt(step - 1) + 2);
                        caminho.Add(coluna);
                        step++;
                    }
                }
            }
            return caminho;

        }

        public List<PathEasy> ObterCaminhoEasy(int? value, int step)
        {
            List<PathEasy> caminho = new List<PathEasy>();
            PathEasy cam = new PathEasy();
            Random rand = new Random();
            int coluna = 0;
            int colunaMaxima = 3;
            int colunaMinima = 0;

            if (string.IsNullOrEmpty(value.ToString()))
            {

                coluna = rand.Next(colunaMinima, colunaMaxima);
                cam.option1 = coluna;

                while (coluna == cam.option1)
                {
                    coluna = rand.Next(colunaMinima, colunaMaxima);
                }

                cam.option2 = coluna;
            }
            else
            {
                caminho = Session["caminho"] as List<PathEasy>;
                if (value == colunaMinima)
                {
                    cam.option1 = colunaMinima;
                    cam.option2 = colunaMinima + 1;
                }
                else if (value == colunaMaxima - 1)
                {
                    cam.option1 = colunaMaxima - 1;
                    cam.option2 = colunaMinima + 1;
                }
                else
                {
                    coluna = rand.Next(colunaMinima, colunaMaxima);
                    cam.option1 = coluna;

                    while (coluna == cam.option1)
                    {
                        coluna = rand.Next(colunaMinima, colunaMaxima);
                    }

                    cam.option2 = coluna;
                }
            }
            caminho.Add(cam);
            Session["caminho"] = caminho;
            return caminho;
        }

        public JsonResult ObterProximoEasy(int Step, int value)
        {
            var caminho = Session["caminho"] as List<PathEasy>;

            if (caminho.ElementAt(Step).option1 == value || caminho.ElementAt(Step).option2 == value)
            {
                if (Step < 3)
                {
                    caminho = ObterCaminhoEasy(value, Step);
                }
                return Json(new { prox = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { prox = false }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult ObterProximo(int Step, int value)
        {
            var caminho = Session["caminho"] as List<int>;
            if (caminho.ElementAt(Step) == value)
                return Json(new { prox = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { prox = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterTodos()
        {
            var caminho = Session["caminho"] as List<int>;
            var result = new { todos = caminho };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterTodosEasy(int step)
        {
            var caminho = Session["caminho"] as List<PathEasy>;
            while (step < 2)
            {
                var cam = ObterCaminhoEasy(null, step);
                caminho.Add(cam.ElementAt(0));
                step++;
            }
            var result = new { todos = caminho };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
    public class PathEasy
    {
        public int option1 { get; set; }
        public int option2 { get; set; }
    }
}