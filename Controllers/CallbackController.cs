using AppConsultaZoho.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace WebZohoAPIS.Controllers
{
    public class CallbackController : Controller
    {
        // GET: Callback
        public ActionResult Index()
        {
            if (Request.QueryString["code"] != null)
            {
                if (Request.QueryString["code"] != null)
                {
                    ContactView ocontactview = new ContactView();
                    Zoho ozoho = new Zoho();
                    ozoho.dict["authorization_code"] = Request.QueryString["code"];

                    #region Solicitar Tokens
                    string url = string.Format("https://accounts.zoho.com/oauth/v2/token?code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                        ozoho.dict["authorization_code"],
                        ozoho.dict["client_id"],
                        ozoho.dict["client_secret"],
                        ozoho.dict["redirect_uri"],
                        ozoho.dict["grant_type"]
                        );

                    WebRequest request = WebRequest.Create(url);
                    request.Method = "POST";

                    WebResponse response = request.GetResponse();
                    var encoding = ASCIIEncoding.ASCII;
                    string access_token = string.Empty;
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        string resultado = reader.ReadToEnd();
                        var otoken = JsonConvert.DeserializeObject<Token>(resultado);
                        access_token = otoken.access_token;
                    }

                    #endregion

                    #region Búsqueda por Número de Teléfono
                    string urlbusquedacontacto = "https://www.zohoapis.com/crm/v2/contacts/search?phone=+51948316529";

                    WebRequest requestcontact = WebRequest.Create(urlbusquedacontacto);
                    requestcontact.Method = "GET";
                    requestcontact.Headers["Authorization"] = "Zoho-oauthtoken " + access_token;

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

            return RedirectToAction("Index", "Home");
        }
    }
    
}