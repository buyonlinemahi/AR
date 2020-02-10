using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;


namespace LMGEDIApp.Infrastructure.Services
{
    public class ARCommonServices : IARCommonServices
    {
        public void CreateErrorLog(string errorMessage, string Stacktrace)
        {
            try
            {
                //create errorlog folder if not exist....hp
                DirectoryInfo dr = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/"));
                if (!dr.CreateSubdirectory("ErrorLog").Exists)
                    dr.CreateSubdirectory("ErrorLog");

                string path = "~/ErrorLog/" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }

                //HttpContext.Current.Response.Write(System.Web.HttpContext.Current.Server.MapPath(path));
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + Environment.NewLine;
                    err += "Error Message:" + errorMessage + Environment.NewLine;
                    err += "Error Stacktrace:" + Stacktrace + Environment.NewLine;
                    err += "User IP:" + System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    w.WriteLine(err);
                    w.WriteLine("_________________________________________________________");
                    w.Flush();
                    w.Close();

                }

            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write(ex.StackTrace);
            }
        }
    }
}
