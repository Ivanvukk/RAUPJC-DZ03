using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models.TodoViewModels;

namespace WebApplication3.Models
{
    public class TodoItemContext : DbContext
    {
        public TodoItemContext (DbContextOptions<TodoItemContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemModel> TodoItemModel { get; set; }
    }
}
