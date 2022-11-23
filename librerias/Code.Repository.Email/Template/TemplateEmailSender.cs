using Code.Repository.Email.Hash;
using Code.Repository.Email.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;


namespace Code.Repository.Email.Template
{
    public class TemplateEmailSender
    {

        string _rootPath = string.Empty;
        public TemplateEmailSender(string path ="")
        {
            _rootPath = path;
        }



        public AlternateView InvitacionTercero(UserEMailDTO data, bool isnew = false)
        {

            string body = string.Empty;

            string webRootPath = string.Empty;
            if (isnew)
                webRootPath = $"{_rootPath}/Template/InvitacionATercero.html";
            else webRootPath = $"{_rootPath}/Template/InvitacionATerceroExistente.html";

            using (StreamReader reader = new StreamReader($"{webRootPath}"))
            {
                body = reader.ReadToEnd();
            }
            //  body = body.Replace("{{username}}", NIT);

            body = body.Replace("{{empresa}}", data.nombreEmpresa);
            body = body.Replace("{{id}}", data.correo);
            body = body.Replace("{{password}}", data.clave);


            AlternateView AV = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            try
            {
                if (string.IsNullOrEmpty(data.logoEMpresa)) data.logoEMpresa = $"{_rootPath}\\images\\sin-logopng.png";
                else if (data.logoEMpresa.Contains("http")) data.logoEMpresa = $"{_rootPath}\\images\\sin-logopng.png";


                LinkedResource Img = new LinkedResource(data.logoEMpresa, MediaTypeNames.Image.Jpeg);
                Img.ContentId = "LogoProveedor";
                Img.TransferEncoding = TransferEncoding.Base64;
                AV.LinkedResources.Add(Img);

            }
            catch (Exception)
            {
            }


            AV.LinkedResources.Add(logoSinco());


            return AV;
        }

        public AlternateView RememberPassword(UserEMailDTO data)
        {

            string body = string.Empty;

            string webRootPath = $"{_rootPath}/Template/ResetearContrasena.html";

            using (StreamReader reader = new StreamReader($"{webRootPath}"))
            {

                body = reader.ReadToEnd();
            }

            body = body.Replace("{{username}}", data.correo);
            body = body.Replace("{{empresa}}", data.nombreEmpresa);
            body = body.Replace("{{password}}", data.clave);



            AlternateView AV = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);


            if (string.IsNullOrEmpty(data.logoEMpresa)) data.logoEMpresa = $"{_rootPath}\\images\\sin-logopng.png";

            LinkedResource Img = new LinkedResource(data.logoEMpresa, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "LogoProveedor";
            Img.TransferEncoding = TransferEncoding.Base64;
            AV.LinkedResources.Add(Img);


            LinkedResource ImgSInco = new LinkedResource($"{_rootPath}\\images\\logoSinco.png", MediaTypeNames.Image.Jpeg);
            ImgSInco.ContentId = "LogoEmpresa";
            ImgSInco.TransferEncoding = TransferEncoding.Base64;
            AV.LinkedResources.Add(ImgSInco);


            return AV;
        }

        public AlternateView RechazoAprobacion(UserRechazoDTO data)
        {

            string body = string.Empty;
            string webRootPath = $"{_rootPath}/Template/RechazoAprobacion.html";


            if (string.IsNullOrEmpty(data.comentarios)) data.comentarios = "";
            else
                data.comentarios = $"<div style='margin-top:20px'><p style='font-weight: bold;'> Comentarios:</p><div>{data.comentarios}</div></div>";


            using (StreamReader reader = new StreamReader($"{webRootPath}"))
            {

                body = reader.ReadToEnd();
            }
            body = body.Replace("{{constructora}}", data.nombreEmpresa);
            body = body.Replace("{{comentarios}}", data.comentarios);

            string motivos = string.Empty;

            if (data.motivosRechazo != null)
            {

                motivos = "<div style='margin-top:20px'><p style='font-weight: bold;'> Causas del rechazo:</p>";
                foreach (var item in data.motivosRechazo) motivos += $"<div style='color: #058C97;padding: 5px;border-bottom: 1px solid #ebebeb;' class='motivo-texto'>{item}</div>";
                motivos += "</div>";
            }
            body = body.Replace("{{motivos}}", motivos);


            AlternateView AV = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            if (data.logoEMpresa == null) data.logoEMpresa = $"{_rootPath}\\images\\sin-logopng.png";

            try
            {
                LinkedResource Img = new LinkedResource(data.logoEMpresa, MediaTypeNames.Image.Jpeg);
                Img.ContentId = "LogoProveedor";
                Img.TransferEncoding = TransferEncoding.Base64;
                AV.LinkedResources.Add(Img);
            }
            catch (Exception)
            {


            }

            try
            {
                LinkedResource ImgSInco = new LinkedResource($"{_rootPath}\\images\\logoSinco.png", MediaTypeNames.Image.Jpeg);
                ImgSInco.ContentId = "LogoEmpresa";
                ImgSInco.TransferEncoding = TransferEncoding.Base64;
                AV.LinkedResources.Add(ImgSInco);

            }
            catch (Exception)
            {


            }

            return AV;
        }

        public AlternateView CreacionERP(UserRechazoDTO data)
        {

            string body = string.Empty;

            string webRootPath = $"{_rootPath}/Template/AprobadoERP.html";
            using (StreamReader reader = new StreamReader($"{webRootPath}"))
            {

                body = reader.ReadToEnd();
            }
            body = body.Replace("{{empresa}}", data.nombreEmpresa);


            AlternateView AV = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);


            if (string.IsNullOrEmpty(data.logoEMpresa)) data.logoEMpresa = $"{_rootPath}\\images\\sin-logopng.png";


            try
            {
                LinkedResource Img = new LinkedResource(data.logoEMpresa, MediaTypeNames.Image.Jpeg);
                Img.ContentId = "LogoProveedor";
                Img.TransferEncoding = TransferEncoding.Base64;
                AV.LinkedResources.Add(Img);

            }
            catch (Exception)
            {


            }

            try
            {
                LinkedResource ImgSInco = new LinkedResource($"{_rootPath}\\images\\logoSinco.png", MediaTypeNames.Image.Jpeg);
                ImgSInco.ContentId = "LogoEmpresa";
                ImgSInco.TransferEncoding = TransferEncoding.Base64;
                AV.LinkedResources.Add(ImgSInco);
            }
            catch (Exception)
            {


            }






            return AV;
        }



        LinkedResource logoSinco()
        {
            LinkedResource ImgSInco = new LinkedResource($"{_rootPath}\\images\\logo-sinco-dark.png", MediaTypeNames.Image.Jpeg);
            ImgSInco.ContentId = "LogoEmpresa";
            ImgSInco.TransferEncoding = TransferEncoding.Base64;

            return ImgSInco;
        }
    }
}
