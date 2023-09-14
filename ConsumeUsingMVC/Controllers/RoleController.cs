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
    public class RoleController : Controller
    {
        private string _baseUrl = "http://localhost:56459/";
        private HttpClient _httpClient;

        public RoleController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl + "api/roles/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Roles
        public async Task<ActionResult> Index()
        {
            List<Role> projects = new List<Role>();
            HttpResponseMessage getAllRolesData = await _httpClient.GetAsync("getAllRoles");

            if (getAllRolesData.IsSuccessStatusCode)
            {
                string results = getAllRolesData.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Role>>(results);
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            ViewData.Model = projects;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Role role)
        {
            HttpResponseMessage isCreated = await _httpClient.PostAsJsonAsync<Role>("create", role);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Role");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage getRole = await _httpClient.GetAsync($"getById/{id}");

            if (getRole.IsSuccessStatusCode)
            {
                string results = getRole.Content.ReadAsStringAsync().Result;
                Role temp = JsonConvert.DeserializeObject<Role>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Role role)
        {
            HttpResponseMessage isCreated = await _httpClient.PutAsJsonAsync("update", role);

            if (isCreated.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Role");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage getRole = await _httpClient.GetAsync($"getById/{id}");

            if (getRole.IsSuccessStatusCode)
            {
                string results = getRole.Content.ReadAsStringAsync().Result;
                Role temp = JsonConvert.DeserializeObject<Role>(results);
                ViewData.Model = temp;
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }




        [ActionName("Delete")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            HttpResponseMessage getRole = await _httpClient.GetAsync($"getById/{id}");

            if (getRole.IsSuccessStatusCode)
            {
                string results = getRole.Content.ReadAsStringAsync().Result;
                Role temp = JsonConvert.DeserializeObject<Role>(results);
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
                return RedirectToAction("Index", "Role");
            }
            else
            {
                Console.WriteLine("Error calling webApi");
            }

            return View();
        }
    }
}