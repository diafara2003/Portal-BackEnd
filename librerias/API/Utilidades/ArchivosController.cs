using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace API.Utilidades
{
    [Route("[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        [Route("ConvertirExcelToJson")]
        [HttpPost]
        public async Task<IActionResult> ConvertirExcelToJson(IFormFile file)
        {
            try
            {
                DataTable TableExcel = new DataTable();
                Tools tool = new Tools();
                string fExten = "";

                if (!string.IsNullOrEmpty(file.FileName))
                    fExten = System.IO.Path.GetExtension(file.FileName);
                if (file != null && file.Length > 0 && fExten.ToLower() != ".csv")
                {
                    using (Stream ms = file.OpenReadStream())
                    {
                        TableExcel = tool.ConvertExcelToDataTable(ms, fExten);
                    }
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(TableExcel);
                return Ok(JSONString);

              

            }
            catch (Exception ex)
            {
                return BadRequest((ex.InnerException != null && ex.InnerException.Message != null ? ex.InnerException.Message : ex.Message));
            }
        }
    }
}
