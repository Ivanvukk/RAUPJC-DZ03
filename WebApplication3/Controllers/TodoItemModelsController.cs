using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.Models.TodoViewModels;

namespace WebApplication3.Controllers
{
    public class TodoItemModelsController : Controller
    {
        private readonly Models.TodoItemContext _context;

        public TodoItemModelsController(Models.TodoItemContext context)
        {
            _context = context;    
        }

        // GET: TodoItemModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItemModel.ToListAsync());
        }

        // GET: TodoItemModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItemModel = await _context.TodoItemModel.SingleOrDefaultAsync(m => m.Id == id);
            if (todoItemModel == null)
            {
                return NotFound();
            }

            return View(todoItemModel);
        }

        // GET: TodoItemModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItemModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateCompleted,DateCreated,IsCompleted,Text,UserId")] TodoItemModel todoItemModel)
        {
            if (ModelState.IsValid)
            {
                todoItemModel.Id = Guid.NewGuid();
                _context.Add(todoItemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(todoItemModel);
        }

        // GET: TodoItemModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItemModel = await _context.TodoItemModel.SingleOrDefaultAsync(m => m.Id == id);
            if (todoItemModel == null)
            {
                return NotFound();
            }
            return View(todoItemModel);
        }

        // POST: TodoItemModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCompleted,DateCreated,IsCompleted,Text,UserId")] TodoItemModel todoItemModel)
        {
            if (id != todoItemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemModelExists(todoItemModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(todoItemModel);
        }

        // GET: TodoItemModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItemModel = await _context.TodoItemModel.SingleOrDefaultAsync(m => m.Id == id);
            if (todoItemModel == null)
            {
                return NotFound();
            }

            return View(todoItemModel);
        }

        // POST: TodoItemModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var todoItemModel = await _context.TodoItemModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.TodoItemModel.Remove(todoItemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TodoItemModelExists(Guid id)
        {
            return _context.TodoItemModel.Any(e => e.Id == id);
        }
    }
}
