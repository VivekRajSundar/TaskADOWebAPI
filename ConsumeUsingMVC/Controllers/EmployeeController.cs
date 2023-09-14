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
using Microsoft.Ajax.Utilities;

namespace ConsumeUsingMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private string _baseUrl = "http://localhost:56459/";
        private HttpClient _httpClient;

        public EmployeeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl + "api/employees/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            //DataTable dt = new DataTable();
            List<Employee> employees = new List<Employee>();

            HttpResponseMessage getAllEmployeesData = await _httpClient.GetAsync("getallemployees");

            if (getAllEmployeesData.IsSuccessStatusCode)
            {
                string results = getAllEmployeesData.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(results);
            }
            else
            {
                Console.WriteLine("Error calling webApi");
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
            HttpResponseMessage isCreated = await _httpClient.PostAsJsonAsync<Employee>("create", employee);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Details(int id)
        {

            HttpResponseMessage getEmployee = await _httpClient.GetAsync($"getById/{id}");

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

            //  ViewData.Model = emp;
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage getEmployee = await _httpClient.GetAsync($"getById/{id}");

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

            return View();
        }

        [HttpPost]
        [Route("Edit"), ActionName("Edit")]
        public async Task<ActionResult> Edit(Employee employee)
        {
            HttpResponseMessage isCreated = await _httpClient.PutAsJsonAsync("update", employee);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        [Route("Delete"), ActionName("Delete")]
        public async Task<ActionResult> DeleteEmp(int id)
        {
            HttpResponseMessage getEmployee = await _httpClient.GetAsync($"getById/{id}");

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

            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage isDeleted = await _httpClient.DeleteAsync($"delete/{id}");

            if (isDeleted.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }
    }
}