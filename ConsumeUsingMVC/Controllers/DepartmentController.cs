﻿using Newtonsoft.Json;
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
        private string _baseUrl = "http://localhost:56459/";
        private HttpClient _httpClient;

        public DepartmentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl + "api/departments/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Department
        public async Task<ActionResult> Index()
        {
            List<Department> departments = new List<Department>();
            HttpResponseMessage getAllDepartmentsData = await _httpClient.GetAsync("getAllDepartments");

            if (getAllDepartmentsData.IsSuccessStatusCode)
            {
                string results = getAllDepartmentsData.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(results);
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            ViewData.Model = departments;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            HttpResponseMessage isCreated = await _httpClient.PostAsJsonAsync<Department>("create", department);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Department");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage getDepartment = await _httpClient.GetAsync($"getById/{id}");

            if (getDepartment.IsSuccessStatusCode)
            {
                string results = getDepartment.Content.ReadAsStringAsync().Result;
                Department temp = JsonConvert.DeserializeObject<Department>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Department department)
        {
            HttpResponseMessage isCreated = await _httpClient.PutAsJsonAsync("update", department);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Department");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage getDepartment = await _httpClient.GetAsync($"getById/{id}");

            if (getDepartment.IsSuccessStatusCode)
            {
                string results = getDepartment.Content.ReadAsStringAsync().Result;
                Department temp = JsonConvert.DeserializeObject<Department>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }




        [ActionName("Delete")]
        public async Task<ActionResult> DeleteDep(int id)
        {
            HttpResponseMessage getDepartment = await _httpClient.GetAsync($"getById/{id}");

            if (getDepartment.IsSuccessStatusCode)
            {
                string results = getDepartment.Content.ReadAsStringAsync().Result;
                Department temp = JsonConvert.DeserializeObject<Department>(results);
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
                return RedirectToAction("Index", "Department");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }
    }
}