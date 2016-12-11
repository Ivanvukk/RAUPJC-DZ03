using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
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

        public IActionResult Todo()
        {

            return View();
        }

        /*public async Task<IActionResult> YourAction()
        {
            // Get currently logged -in user using userManager
            ApplicationUser currentUser = await
            _userManager.GetUserAsync(HttpContext.User);

            return default(IActionResult);
        }*/

        
    }
}