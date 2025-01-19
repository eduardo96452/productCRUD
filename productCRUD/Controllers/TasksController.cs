using Microsoft.AspNetCore.Mvc;
using Supabase;
using productCRUD.Models;
using static Supabase.Postgrest.Constants;

namespace productCRUD.Controllers
{
    public class TasksController : Controller
    {
        private readonly SupabaseService _supabaseService;

        public TasksController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<IActionResult> Index()
        {
            var client = _supabaseService.GetClient();
            var tasks = await client.From<Models.Task>().Get();
            return View(tasks.Models);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Models.Task task)
        {
            var client = _supabaseService.GetClient();
            task.CreatedAt = DateTime.UtcNow;
            await client.From<Models.Task>().Insert(task);
            return RedirectToAction("Index");
        }

       /* public async Task<IActionResult> Edit(int id)
        {
            var client = _supabaseService.GetClient();
            var task = await client.From<Models.Task>().Filter("id", Operator.Equals , id).Single();
            return View(task);
        }*/

        [HttpPost]
        /*public async Task<IActionResult> Edit(Models.Task task)
        {
            var client = _supabaseService.GetClient();
            await client.From<Models.Task>().Update(task);
            return RedirectToAction("Index");
        }*/

        public async Task<IActionResult> Delete(int id)
        {
            var client = _supabaseService.GetClient();
            await client.From<Models.Task>().Filter("id", Operator.Equals, id).Delete();
            return RedirectToAction("Index");
        }
    }
}
