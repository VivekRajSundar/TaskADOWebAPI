using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumeUsingMVC.Models;

namespace ConsumeUsingMVC.Controllers
{
    public class DepartmentController : Controller
    {
        string baseUrl = "http://localhost:56459/";
        // GET: Department
        public async Task<ActionResult> Index()
        {
            //DataTable dt = new DataTable();
            List<Department> departments = new List<Department>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getAllEmployeesData = await client.GetAsync("api/departments/getalldepartments");

                if (getAllEmployeesData.IsSuccessStatusCode)
                {
                    string results = getAllEmployeesData.Content.ReadAsStringAsync().Result;
                    departments = JsonConvert.DeserializeObject<List<Department>>(results);
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            ViewData.Model = departments;
            return View();
        }
    }
}