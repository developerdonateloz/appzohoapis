using System.Collections.Generic;

namespace AppConsultaZoho.Models
{
    public class Zoho
    {
        public Dictionary<string, string> dict { get; set; }

        //SELF CLIENT
        public Zoho()
        {
            dict = new Dictionary<string, string>();
            dict.Add("code", "XXXXXXXXXXXXXXXXX");
            
            dict.Add("redirect_uri", "https://localhost:44312/");

            dict.Add("client_id", "YYYYYYYYYYYYYY");
            dict.Add("client_secret", "ZZZZZZZZZZZZZZZ");
            dict.Add("grant_type", "authorization_code");

            dict.Add("refresh_token", "WWWWWWWWWWWWWWWWWWWWWWWWWWWWW");

            dict.Add("scope", "ZohoCRM.modules.ALL");
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