using AppConsultaZoho.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace AppConsultaZoho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRefreshToken()
        {
            Zoho ozoho = new Zoho();

            string url = string.Format("https://accounts.zoho.com/oauth/v2/token?code={0}&redirect_uri={1}&client_id={2}&client_secret={3}&grant_type={4}",
                ozoho.dict["code"],
                ozoho.dict["redirect_uri"],
                ozoho.dict["client_id"],
                ozoho.dict["client_secret"],
                ozoho.dict["grant_type"]
                );

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";

            WebResponse response = request.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            string orefresh_token = string.Empty;

            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string resultado = reader.ReadToEnd();
                var otoken = JsonConvert.DeserializeObject<Token>(resultado);
                orefresh_token = otoken.refresh_token;
            }

            return View("Index");
        }

        public ActionResult GetContactByPhone()
        {
            ContactView ocontactview = new ContactView();

            #region Obtener Access Token
            Zoho ozoho = new Zoho();
            ozoho.dict["grant_type"] = "refresh_token";

            string url = string.Format("https://accounts.zoho.com/oauth/v2/token?refresh_token={0}&client_id={1}&client_secret={2}&grant_type={3}",
               ozoho.dict["refresh_token"],
               ozoho.dict["client_id"],
               ozoho.dict["client_secret"],
               ozoho.dict["grant_type"]
               );

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";

            WebResponse response = request.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            string oaccess_token = string.Empty;

            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string resultado = reader.ReadToEnd();
                var otoken = JsonConvert.DeserializeObject<Token>(resultado);
                oaccess_token = otoken.access_token;
            }
            #endregion

            #region Búsqueda por Número de Teléfono
            string urlbusquedacontacto = "https://www.zohoapis.com/crm/v2/contacts/search?phone=+51987757745";

            WebRequest requestcontact = WebRequest.Create(urlbusquedacontacto);
            requestcontact.Method = "GET";
            requestcontact.Headers["Authorization"] = "Zoho-oauthtoken " + oaccess_token;

            WebResponse responsecontact = requestcontact.GetResponse();

            ContactZoho ocontact = new ContactZoho();

            using (var reader = new System.IO.StreamReader(responsecontact.GetResponseStream(), encoding))
            {
                string resultado = reader.ReadToEnd();
                ocontact = JsonConvert.DeserializeObject<ContactZoho>(resultado);
            }

            if (ocontact != null)
            {
                var item = ocontact.Data.FirstOrDefault();
                ocontactview = new ContactView
                {
                    NombreCompleto = item.Full_Name,
                    Producto = item.Producto_Acad_mico,
                    CorreoElectronico = item.Email
                };
            }

            #endregion

            return View("Index", ocontactview);
        }
    }
}