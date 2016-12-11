using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject user manager into constructor
        public TodoController(ITodoRepository repository,
        UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> YourAction()
        {
            // Get currently logged -in user using userManager
            ApplicationUser currentUser = await
            _userManager.GetUserAsync(HttpContext.User);
            // ...
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}