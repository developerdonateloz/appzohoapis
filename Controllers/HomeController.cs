using AppConsultaZoho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppConsultaZoho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaZoho()
        {
            Zoho ozoho = new Zoho();

            string url = string.Format("https://accounts.zoho.com/oauth/v2/auth?response_type={0}&scope={1}&client_id={2}&redirect_uri={3}&access_type={4}",
                ozoho.dict["response_type"],
                ozoho.dict["scope"],
                ozoho.dict["client_id"],
                ozoho.dict["redirect_uri"],
                ozoho.dict["access_type"]);

            return Redirect(url);
        }
    }
}