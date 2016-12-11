using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

public class TodoController : Controller
{
    private readonly TodoSqlRepository _repository;

    public TodoController(TodoSqlRepository repository)
    {
        _repository = repository;
    }
}

