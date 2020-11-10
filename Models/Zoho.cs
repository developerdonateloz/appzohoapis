using System.Collections.Generic;

namespace AppConsultaZoho.Models
{
    public class Zoho
    {
        public Dictionary<string, string> dict { get; set; }
        public Zoho()
        {
            dict = new Dictionary<string, string>();
            dict.Add("client_id", "XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            dict.Add("client_secret", "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
            dict.Add("redirect_uri", "https://localhost:44312/Callback");
            dict.Add("response_type", "code");
            dict.Add("access_type", "offline");
            dict.Add("scope", "ZohoCRM.modules.ALL");
            dict.Add("grant_type", "authorization_code");
            dict.Add("authorization_code", string.Empty);
        }
    }
    public class Token
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string api_domain { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
    public class ContactZoho
    {
        public List<Data> Data { get; set; }

    }
    public class ContactView
    {
        public string NombreCompleto { get; set; }
        public string Producto { get; set; }
        public string CorreoElectronico { get; set; }
    }
    public class Data
    {
        public Owner MyProperty { get; set; }
        public string Email { get; set; }
        public string Producto_Acad_mico { get; set; }
        public string Full_Name { get; set; }
        public string Anuncio { get; set; }

    }
    public class Owner
    {
        public string name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }
}