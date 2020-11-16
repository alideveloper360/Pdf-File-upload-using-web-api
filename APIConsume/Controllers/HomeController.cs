using APIConsume.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        //Hosted web API REST Service base url  
        public async Task<ActionResult> Index()
            {
            string Baseurl = "http://localhost:60010/";

            List<apiconsume> EmpInfo = new List<apiconsume>();

                using (var client = new HttpClient())
                { 
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/Tables"); 
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result; 
                        EmpInfo = JsonConvert.DeserializeObject<List<apiconsume>>(EmpResponse);

                    }
                    return View(EmpInfo);
                }
            }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(apiconsume obj, HttpPostedFileBase file)
        {
            string path = Path.Combine(Server.MapPath("~/Ali/"), Path.GetFileName(file.FileName));
            file.SaveAs(path);
            obj.FileName = "~/Ali/" + file.FileName;

            HttpResponseMessage response = GlobalVariable.webapiclient.PostAsJsonAsync("Tables/", obj).Result;
            TempData["SuccessMessage"] = "Saved Successfully";
            return RedirectToAction("Index");

        }
        public void FileView(string Path)
        {
            string FilePath = Server.MapPath(Path);
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }
    }
}
