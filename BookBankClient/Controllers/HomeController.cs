using BookBankClient.Models;
using BookBankLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookBankClient.Controllers
{
    public class HomeController : Controller
    {
        string baseuri = "https://35.201.117.105.nip.io/proxy4api/";
        private readonly ILogger<HomeController> _logger;
        string json;
        HttpResponseMessage response;
        DBOperation dbOps = new DBOperation();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseuri);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apiKey", "Tz6kN0PQYttjaCFlLyU1DaTRPqmjVbgSkb2HENrDXzKGblAA");
            return client;
        }
        public IActionResult Index() => View();

        public IActionResult Authors()
        {
            string json;
            HttpResponseMessage response;
            IEnumerable<Author> authors = null;
            using (var client = GetClient())
            { 
                response = client.GetAsync("api/authors/").Result;
                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().Result;
                    authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(json);
                }
            }
            return View("Authors", authors);
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

        [HttpGet]
        public ViewResult AddAuthor() => View();

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            string json;
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                using (var client = GetClient())
                {
                    json = JsonConvert.SerializeObject(author);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = client.PostAsync("api/authors", content).Result;
                }
                return RedirectToAction("Authors", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult UpdateAuthor(Author author)
        {
            return View(author);
        }

        [HttpPost]
        public IActionResult UpdateAuthor (string id, Author author)
        {
            using (var client = GetClient())
            {
                json = JsonConvert.SerializeObject(author);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PutAsync($"api/authors/{id}", content).Result;
            }
            return RedirectToAction("Authors", "Home");
        }

        [HttpGet]
        public IActionResult DeleteAuthor(String id)
        {
            using (var client = GetClient())
            {
                response = client.DeleteAsync($"api/authors/{id}").Result;
            }
            return RedirectToAction("Authors", "Home");
        }

        [HttpGet]
        public ViewResult ViewAuthor(string id)
        {
            string json;
            HttpResponseMessage response;
            AuthorWithBooks author = null;
            using (var client = GetClient())
            {
                response = client.GetAsync($"api/authors/{id}?includeBooks=true").Result;
                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<AuthorWithBooks>(json);
                }
            }
            return View("ViewAuthor", author);
        }
        [HttpGet]
        public ViewResult AddBook(string id)
        {
            ViewData["AuthorId"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(string id, Book book)
        {
            string json;
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                using (var client = GetClient())
                {
                    json = JsonConvert.SerializeObject(book);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = client.PostAsync($"api/author/{id}/books", content).Result;
                }
                return Redirect($"/Home/ViewAuthor?id={id}");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult DeleteBook(String authorId, string bookId)
        {
            using (var client = GetClient())
            {
                response = client.DeleteAsync($"api/author/{authorId}/books/{bookId}").Result;
            }
            return Redirect($"/Home/ViewAuthor?id={authorId}");
        }

        [HttpGet]
        public ViewResult UpdateBook(string authorId, string bookId)
        {
            string json;
            HttpResponseMessage response;
            Book book = null;
            using (var client = GetClient())
            {
                response = client.GetAsync($"api/author/{authorId}/books/{bookId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(json);
                }
            }
            ViewData["authorId"] = authorId;
            return View(book);
        }

        [HttpPost]
        public ActionResult UpdateBook(string authorId, string bookId, Book book)
        {
            using (var client = GetClient())
            {
                json = JsonConvert.SerializeObject(book);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PutAsync($"api/author/{authorId}/books/{bookId}", content).Result;
            }
            return Redirect($"/Home/ViewAuthor?id={authorId}");
        }

        [HttpPost]
        public IActionResult Search(string authorId)
        {
            return Redirect($"/Home/ViewAuthor?id={authorId}");
        }
        

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            try
            {
                UserModel _user = dbOps.GetUser(user.UserId).Result.First();
                if (_user != null && user.Password == _user.Password)
                {
                    return Redirect("/Home/Authors");
                }
                ViewData["Error"] = "User doesn't Exists";
                return View("Index");
            }
            catch
            {
                ViewData["Error"] = "User doesn't Exists";
                return View("Index");
            }
           
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return Redirect("/Home/");
        }
    }
}
