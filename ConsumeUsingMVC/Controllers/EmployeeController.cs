using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using ConsumeUsingMVC.Models;
using Microsoft.AspNetCore.Http;

namespace ConsumeUsingMVC.Controllers
{
    public class EmployeeController : Controller
    {
        string baseUrl = "http://localhost:56459/";
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            //DataTable dt = new DataTable();
            List<Employee> employees = new List<Employee>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getAllEmployeesData = await client.GetAsync("api/employees/getallemployees");

                if (getAllEmployeesData.IsSuccessStatusCode)
                {
                    string results = getAllEmployeesData.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<Employee>>(results);
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            ViewData.Model = employees;
            return View();
        }

        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isCreated = await client.PostAsJsonAsync<Employee>("create", employee);

                if (isCreated.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Employee temp = JsonConvert.DeserializeObject<Employee>(results);
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

        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Employee temp = JsonConvert.DeserializeObject<Employee>(results);
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
        [Route("Edit"), ActionName("Edit")]
        public async Task<ActionResult> Edit(Employee employee)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isCreated = await client.PutAsJsonAsync("update", employee);

                if (isCreated.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("Error calling webApi");
                }
            }
            return View();
        }

        [Route("Delete"), ActionName("Delete")]
        public async Task<ActionResult> DeleteEmp(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getEmployee = await client.GetAsync($"getById/{id}");

                if (getEmployee.IsSuccessStatusCode)
                {
                    string results = getEmployee.Content.ReadAsStringAsync().Result;
                    Employee temp = JsonConvert.DeserializeObject<Employee>(results);
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
                client.BaseAddress = new Uri(baseUrl + "api/employees/");
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage isDeleted = await client.DeleteAsync($"delete/{id}");
       

                if (isDeleted.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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