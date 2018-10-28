using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WF.Find.Presentation.Services;
using Microsoft.AspNet.Identity;

namespace WF.Find.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GenerateGuid()
        {
            Guid id = Guid.NewGuid();
            var result = new { id = id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendEmail(string email, string nome)
        {
            string assunto = "Convite Find";
            var callbackUrl = Url.Action("Index", "Home", null, protocol: Request.Url.Scheme);

            string corpo = "Olá " + nome + " você foi convidado para uma partida de Find " +
                "clique  <a href=\"" + callbackUrl + "\">AQUI</a> para jogar";

            IdentityMessage message = new IdentityMessage();
            EmailService sendEmail = new EmailService();
            message.Destination = email;
            message.Subject = assunto;
            message.Body = corpo;

            try
            {
                sendEmail.SendAsync(message);
                var result = new { send = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var result = new { send = false };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}