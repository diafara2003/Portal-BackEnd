using Code.Repository.DAO.Helper;
using Code.Repository.DAO.Models.ConexionERP;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Code.Repository.DAO.Context
{
    public class ConexionERP
    {
        private int IdContructora;
        private string RutaPreToken = "/API/Auth/Usuario";
        private string RutaEmpresa = "/API/Cliente/Empresas";
        private string RutaSucursal(int idEmpresa) { return $"/API/Cliente/1/Empresa/{idEmpresa}/Sucursales"; }
        private string RutaToken(int sucursal) { return $"/API/Auth/Sesion/Iniciar/1/Empresa/1/Sucursal/{sucursal}"; }

        public ConexionERP(int constructora)
        {
            IdContructora = constructora;
        }
        public ConstructoraERP ObtenerConstructora()
        {
            ConstructoraERP objResult = new ConstructoraERP();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var result = objcnn.constructoras.Where(c => c.ConstId == IdContructora).Single();

            objResult.ConstId = result.ConstId;
            objResult.ConstRuta_API = result.ConstRuta_API;
            objResult.ConstNIT = result.ConstNIT;
            objResult.ConstUsuario_API = "provportal";
            objResult.ConstClave_API = "+m8RuYOMNLGR1yvw0V+dJsTFuQT1BkUkNKXCSBQ6U9fZnRyTpsdg/YjlGFkDQGpIl7IctbC5LMPUexnG/hmkTVmEWC1+9gIR+iD8HqhBKUEgI0oOoJ+cetKxI+38rb57Apr6CfaAhLxFXdR+/fz1A414hEQ5zPCvxDqeLA/8gtHReMdqFxXNxu6j1i3DASDtrVMgMrOz3p0vDP4/Kqa79cOQSOFQrTq5Zjf0UYQRKqjyqz+7Up9Ghk6IIbYLXq8gz4cwrRVB81Iwx/NBNdAZ57ttM4JkkDCIk6b5Dfc0x7Q=6";
            return objResult;
        }
        public async Task<string> ObtenerToken()
        {
            ConstructoraERP Const_ = ObtenerConstructora();
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var fecha = DateTime.Now;
            //se valida si ya existe un token 
            var tokenERP = objcnn.tokenERP.Where(c => c.IdConstructora == Const_.ConstId).FirstOrDefault() ?? new Model.Entity.dbo.TokenERP()
            {
                Token = String.Empty,
                FechaToken = fecha,
            };
           // fecha = fecha.AddHours(9);

            TimeSpan diffResult = tokenERP.FechaToken.Subtract(fecha);

            if (!string.IsNullOrEmpty(tokenERP.Token) && (Math.Abs(diffResult.Hours) < 3))
            {
                return tokenERP.Token;
            }
            else
            {
                //clave para el login "ProvPortal123*"
                var datos_ = new { NomUsuario = Const_.ConstUsuario_API, ClaveUsuario = Const_.ConstClave_API };

                try
                {
                    var pre_ = await Peticion(Const_.ConstRuta_API + RutaPreToken, HttpMethod.Post, datos_, "-1");
                    var preres = JObject.Parse(pre_).ToObject<RespuestaPeticionDTO>();


                    var emp_ = await Peticion(Const_.ConstRuta_API + RutaEmpresa, HttpMethod.Get, "", preres.access_token);
                    var empres = JsonConvert.DeserializeObject<List<EmpresaDTO>>(emp_).FirstOrDefault();

                    var suc_ = await Peticion(Const_.ConstRuta_API + RutaSucursal(empres.IdEmpresa), HttpMethod.Get, "", preres.access_token);
                    var sucres = JsonConvert.DeserializeObject<List<SucursalDTO>>(suc_).FirstOrDefault();

                    var tok_ = await Peticion(Const_.ConstRuta_API + RutaToken(sucres.Id), HttpMethod.Get, "", preres.access_token);
                    var tokres = JObject.Parse(pre_).ToObject<RespuestaPeticionDTO>();


                    if (string.IsNullOrEmpty(Const_.ConstUrlLogo) && !string.IsNullOrEmpty(empres.Imagenes))
                    {
                        Const_.ConstUrlLogo = $"{Const_.ConstRuta_API.ToLower().Replace("/v3", "")}/{empres.Imagenes.Replace("../", "")}";

                        //ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

                        //Const_.ConstClave_API = "";
                        //objcnn.Entry<Constructora>(Const_).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        //objcnn.SaveChanges();
                    }
                    if (!string.IsNullOrEmpty(tokenERP.Token))
                    {
                        objcnn.Entry(tokenERP).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        objcnn.SaveChanges();
                    }


                    objcnn.tokenERP.Add(new Model.Entity.dbo.TokenERP()
                    {
                        Id = 0,
                        FechaToken = fecha,
                        IdConstructora = Const_.ConstId,
                        Token = tokres.access_token

                    });
                    objcnn.SaveChanges();

                    return tokres.access_token;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }


        }
        public async Task<string> Peticion(string url, HttpMethod type, object data, string token)
        {

            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage(type, url);
            request.Content = data.AsJson();
            if (token != "-1")
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await _httpClient.SendAsync(request);

                //  response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {

                return null;
            }



        }

        public async Task<Stream> PeticionArchivo(string url, HttpMethod type, object data, string token)
        {

            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage(type, url);
            request.Content = data.AsJson();
            if (token != "-1")
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.SendAsync(request);
           // response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            return content;
        }

    }

}

public static class Extensions
{
    public static StringContent AsJson(this object o)
        => new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
}



