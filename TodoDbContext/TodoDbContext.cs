using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


public class TodoDbContext : DbContext

{
    public DbSet<Model.TodoItem> todoItems { get; set;}
}

