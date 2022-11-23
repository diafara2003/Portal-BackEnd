using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Helper
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;

            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                RegistrarError(error, context);
                await response.WriteAsync(result);
            }
        }


        void RegistrarError(Exception error, HttpContext context)
        {
            string ruta = $"{_webHostEnvironment.ContentRootPath}\\Error\\";


            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);

            }


            ruta += @"\ErroresAPI.txt";

            if (!File.Exists(ruta))
            {
                var archivo = File.Create(ruta);
                archivo.Close();
            }

            using (StreamWriter file = new StreamWriter(ruta, true))
            {
                string api = $"{context.Request.PathBase}{context.Request.Path}";
                file.WriteLine(CrearTextoError(error?.Message, api, context.Request.Method)); //se agrega información al documento

                file.Close();
            }

        }

        string CrearTextoError(string exception, string _request, string method)
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendLine("================ |INICIO DEL ERROR |====================================");
            texto.AppendLine(string.Format("------->Fecha y hora {0}", DateTime.Now.ToString("G")));
            texto.AppendLine("====================================================");
            texto.AppendLine("Eception: " + exception);
            texto.AppendLine("====================================================");
            texto.AppendLine("METHOD: " + method.ToUpper());
            texto.AppendLine("====================================================");
            texto.AppendLine("REQUEST: " + _request);
            texto.AppendLine("====================================================");
            texto.AppendLine("================ |FIN DEL ERROR |====================================");

            return texto.ToString();
        }


    }
}
