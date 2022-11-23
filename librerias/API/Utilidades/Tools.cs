using ClosedXML.Excel;
using Excel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utilidades
{
    public class Tools
    {
        public DataTable ConvertExcelToDataTable(Stream stream, string Extension)
        {
            IExcelDataReader excelReader;
            if (Extension.ToLower() == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            DataTable dt = new DataTable();
            dt = result.Tables[0];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName != null)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ToString().ToLower();
                }

            }
            return dt;
        }

        public DataTable ConvertCSVtoDataTable(Stream stream)
        {
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            DataTable dt = new DataTable();
            string separator = "";
            if (text.Contains(";"))
                separator = ";";
            if (text.Contains(","))
                separator = ",";
            string[] tableData = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var col = from cl in tableData[0].Split(separator.ToCharArray()) select new DataColumn(cl);
            dt.Columns.AddRange(col.ToArray());
            (from st in tableData.Skip(1) select dt.Rows.Add(st.Split(separator.ToCharArray()))).ToList();
            return dt;
        }

        public async Task<XLWorkbook> BuildExcelFile(DataTable dt, string NombreHoja)
        {
            var t = Task.Run(() =>
            {
                return ConvertDataTableToExcel(dt, NombreHoja);
            });
            return await t;
        }

        public async Task<XLWorkbook> BuildBasicExcelFile(DataTable dt, string NombreHoja)
        {
            var t = Task.Run(() =>
            {
                return ConvertDataTableToBasicExcel(dt, NombreHoja);
            });
            return await t;
        }
        public XLWorkbook ConvertDataTableToExcel(DataTable dt, string NombreHoja)
        {
            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(dt, NombreHoja);
            for (int i = 1; i <= ws.ColumnCount(); i++)
            {
                ws.Column(i).AdjustToContents();
            }
            return wb;
        }


        public XLWorkbook ConvertDataTableToBasicExcel(DataTable dt, string NombreHoja)
        {
            XLWorkbook wb = new XLWorkbook();
            IXLWorksheet ws = wb.Worksheets.Add(NombreHoja);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //Se agregan las encabezados
                ws.Cell(1, i + 1).Value = dt.Columns[i].ToString();
                ws.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1);
                ws.Cell(1, i + 1).Style.Font.FontColor = XLColor.White;

                //Se agrega los datos de cada columna 
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ws.Cell(j + 2, i + 1).Value = dt.Rows[j][dt.Columns[i].ToString()].ToString();
                }
            }

            return wb;
        }
    }
}
