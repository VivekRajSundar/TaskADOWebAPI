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

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isCreated = await client.PostAsJsonAsync<Department>("create", department);

                if (isCreated.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Department temp = JsonConvert.DeserializeObject<Department>(results);
                    ViewData.Model = temp;
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            //  ViewData.Model = emp;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Department department)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isCreated = await client.PutAsJsonAsync("update", department);

                if (isCreated.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Department temp = JsonConvert.DeserializeObject<Department>(results);
                    ViewData.Model = temp;
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            //  ViewData.Model = emp;
            return View();
        }




        [ActionName("Delete")]
        public async Task<ActionResult> DeleteDep(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Department  temp = JsonConvert.DeserializeObject<Department>(results);
                    ViewData.Model = temp;
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            //  ViewData.Model = emp;
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/departments/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isDeleted = await client.DeleteAsync($"delete/{id}");


                if (isDeleted.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Department");
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            return View();
        }


    }
}