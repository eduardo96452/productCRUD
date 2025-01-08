using Microsoft.AspNetCore.Mvc;
using productCRUD.Models;
using System.Diagnostics;
using static Supabase.Postgrest.Constants;

namespace productCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SupabaseService _supabaseService;

        public HomeController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<IActionResult> Index()
        {
            var client = _supabaseService.GetClient();

            // Obtén las tareas desde Supabase
            var tasks = await client.From<Models.Task>().Get();

            // Asegúrate de pasar el modelo a la vista
            return View(tasks.Models);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View(new Models.Task());
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(Models.Task task)
        {
            var client = _supabaseService.GetClient();
            task.CreatedAt = DateTime.UtcNow;
            await client.From<Models.Task>().Insert(task);
            return RedirectToAction("Index");
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var client = _supabaseService.GetClient();
            var task = await client.From<Models.Task>().Filter("id", Operator.Equals, id).Single();
            return View(task);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            var client = _supabaseService.GetClient();
            await client.From<Models.Task>().Update(task);
            return RedirectToAction("Index");
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var client = _supabaseService.GetClient();
            var task = await client.From<Models.Task>().Filter("id", Operator.Equals, id).Single();
            return View(task);
        }

        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Models.Task task)
        {
            var client = _supabaseService.GetClient();
            await client.From<Models.Task>().Filter("id", Operator.Equals, task.Id).Delete();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
