using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ConsumeUsingMVC.Models;
using System.Threading.Tasks;

namespace ConsumeUsingMVC.Controllers
{
    public class ProjectController : Controller
    {
        private string _baseUrl = "http://localhost:56459/";
        private HttpClient _httpClient;

        public ProjectController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl + "api/projects/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Project
        public async Task<ActionResult> Index()
        {
            List<Project> projects = new List<Project>();
            HttpResponseMessage getAllProjectsData = await _httpClient.GetAsync("getAllProjects");

            if (getAllProjectsData.IsSuccessStatusCode)
            {
                string results = getAllProjectsData.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Project>>(results);
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            ViewData.Model = projects;
            return View();
        }

        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Project project)
        {
            HttpResponseMessage isCreated = await _httpClient.PostAsJsonAsync<Project>("create", project);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage getProject = await _httpClient.GetAsync($"getById/{id}");

            if (getProject.IsSuccessStatusCode)
            {
                string results = getProject.Content.ReadAsStringAsync().Result;
                Project temp = JsonConvert.DeserializeObject<Project>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Project project)
        {
            HttpResponseMessage isCreated = await _httpClient.PutAsJsonAsync("update", project);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage getProject = await _httpClient.GetAsync($"getById/{id}");

            if (getProject.IsSuccessStatusCode)
            {
                string results = getProject.Content.ReadAsStringAsync().Result;
                Project temp = JsonConvert.DeserializeObject<Project>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }




        [ActionName("Delete")]
        public async Task<ActionResult> DeleteProj(int id)
        {
            HttpResponseMessage getProject = await _httpClient.GetAsync($"getById/{id}");

            if (getProject.IsSuccessStatusCode)
            {
                string results = getProject.Content.ReadAsStringAsync().Result;
                Project temp = JsonConvert.DeserializeObject<Project>(results);
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
                return RedirectToAction("Index", "Project");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }
    }
}