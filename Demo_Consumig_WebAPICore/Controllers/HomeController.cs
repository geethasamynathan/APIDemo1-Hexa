using Demo_Consumig_WebAPICore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Demo_Consumig_WebAPICore.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Demo_Consumig_WebAPICore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Customer> customerList = new List<Customer>();

            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync("https://localhost:44316/api/Customers"))
                {
                    string apiResaponse = await response.Content.ReadAsStringAsync();

                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResaponse);
                }
            }
            return View(customerList);
        }

        public async Task<IActionResult> GetCustomerById(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Customers/"+id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResaponse = await response.Content.ReadAsStringAsync();

                        customer = JsonConvert.DeserializeObject<Customer>(apiResaponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
                return View(customer);
            }


        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            Customer newcustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44316/api/Customers",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newcustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
               // return View(newcustomer);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Customers/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResaponse = await response.Content.ReadAsStringAsync();

                        customer = JsonConvert.DeserializeObject<Customer>(apiResaponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
                return View(customer);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            Customer newcustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
                //var content = new MultipartFormDataContent();
                //content.Add(new StringContent(customer.CustomerId.ToString()), "CustomerId");
                //content.Add(new StringContent(customer.FirstName), "FirstName");
                //content.Add(new StringContent(customer.LastName), "LastName");
                //content.Add(new StringContent(customer.Contactno), "Contactno");
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44316/api/Customers", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.result = "success";

                    newcustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
               
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Customers/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResaponse = await response.Content.ReadAsStringAsync();

                        customer = JsonConvert.DeserializeObject<Customer>(apiResaponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
                return View(customer);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44316/api/Customers?id="+id))
                {
                     string apiResaponse = await response.Content.ReadAsStringAsync();

                }
                return RedirectToAction("Index");
            }
        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
