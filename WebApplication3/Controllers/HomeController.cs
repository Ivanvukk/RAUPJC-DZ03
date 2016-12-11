using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.TodoViewModels;
using Repository;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;
using Model;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        /*static TodoDbContextModel db = new TodoDbContextModel();
        private readonly TodoSqlRepositoryModel _repository = new TodoSqlRepositoryModel(db);
        private readonly UserManager<ApplicationUser> _userManager;
        

        // Inject user manager into constructor
        public HomeController(TodoSqlRepositoryModel repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }*/

        private readonly UserManager<ApplicationUser> _userManager;
        static List<TodoItemModel> db = new List<TodoItemModel>();
        public TodoSqlRepositoryModel _repository = new TodoSqlRepositoryModel(db);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Todo()
        {

            Guid Id = new Guid();
            TodoItemModel item = new TodoItemModel("Trgovina", Id);
            TodoItemModel item2 = new TodoItemModel("Zadaća", Id);

            _repository.Add(item);
            _repository.Add(item2);



            return View(_repository.GetAll(Id));
        }

        public IActionResult Delete()
        {
      
            return Content("Deleted");
        }

        public IActionResult Edit()
        {

            return Content("Edited");
        }

        public IActionResult Create()
        {

            return Content("Create new");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
