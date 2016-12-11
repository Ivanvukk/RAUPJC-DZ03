using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.TodoViewModels;

namespace Repository
{
    public interface ITodoRepository
    {
        /// <summary >
        /// Gets TodoItem for a given id. Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        /// </ summary >
        /// <param name =" todoId " > Todo Id </ param >
        /// <param name =" userId " >Id of the user that is trying to fetch the data</ param >
        /// <returns > TodoItem if found , null otherwise </ returns >
        TodoItemModel Get(Guid todoId, Guid userId);

        /// <summary>
        /// Adds new TodoItem object in database.
        /// If object with the same id already exists,
        /// method should throw DuplicateTodoItemException with the message "duplicate id: {id}".
        /// </summary>
        void Add(TodoItemModel todoItem);

        /// <summary >
        /// Tries to remove a TodoItem with given id from the database . Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        /// </ summary >
        /// <param name =" todoId " > Todo Id </ param >
        /// <param name =" userId " >Id of the user that is trying to remove the data</ param >
        /// <returns > True if success , false otherwise </ returns >
        bool Remove(Guid todoId, Guid userId);

        /// <summary >
        /// Updates given TodoItem in database .
        /// If TodoItem does not exist , method will add one . Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        /// </ summary >
        /// <param name =" todoItem " > Todo item </ param >
        /// <param name =" userId " >Id of the user that is trying to update the data</ param>
        void Update(TodoItemModel todoItem, Guid userId);

        /// <summary >
        /// Tries to mark a TodoItem as completed in database . Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        /// </ summary >
        /// <param name =" todoId " > Todo Id </ param >
        /// <param name =" userId " >Id of the user that is trying to mark as completed</ param >
        /// <returns > True if success , false otherwise </ returns >
        bool MarkAsCompleted(Guid todoId, Guid userId);

        /// <summary>
        /// Gets all TodoItem objects in database for user, sorted by date created (descending)
        /// </summary>
        List<TodoItemModel> GetAll(Guid userId);

        /// <summary>
        /// Gets all incomplete TodoItem objects in database for user
        /// </summary>
        List<TodoItemModel> GetActive(Guid userId);

        /// <summary>
        /// Gets all completed TodoItem objects in database for user
        /// </summary>
        List<TodoItemModel> GetCompleted(Guid userId);

        /// <summary>
        /// Gets all TodoItem objects in database for user that apply to the filter
        /// </summary>
        List<TodoItemModel> GetFiltered(Func<TodoItemModel, bool> filterFunction, Guid userId);
    }

    public class TodoSqlRepositoryModel : ITodoRepository
    {

        /*private readonly TodoDbContextModel _context;

        public TodoSqlRepositoryModel(TodoDbContextModel context)
        {
            _context = context;
        }*/

        private readonly List<TodoItemModel> _context;

        public TodoSqlRepositoryModel(List<TodoItemModel> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _context = initialDbState;
            }
            else
            {
                _context = new List<TodoItemModel>();
            }

        }



        public void Add(TodoItemModel todoItem)
        {
            if (_context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id).FirstOrDefault() != null)
            {
                System.ArgumentException DuplicateTodoItemException = new System.ArgumentException("Duplicate id: " + todoItem.Id);
                throw DuplicateTodoItemException;
            }
            else
            {
                _context/*.TodoItems*/.Add(todoItem);
            }

        }

        public TodoItemModel Get(Guid todoId, Guid userId)
        {
            TodoItemModel findTodoItem = _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoId)
                                                      .FirstOrDefault();
            if (findTodoItem != default(TodoItemModel))
            {
                if (findTodoItem.UserId == userId)
                {
                    return findTodoItem;
                }
                else
                {
                    System.ArgumentException TodoAccessDeniedException = new System.ArgumentException("Access denied, user not owner");
                    throw TodoAccessDeniedException;
                }
            }
            else
            {
                return null;
            }


        }

        public List<TodoItemModel> GetActive(Guid userId)
        {

            return _context/*.TodoItems*/.Where(TodoItem => TodoItem.IsCompleted == false && TodoItem.UserId == userId)
                                     .ToList();
        }

        public List<TodoItemModel> GetAll(Guid userId)
        {
            return _context/*.TodoItems*/.Where(TodoItem => TodoItem.UserId == userId)
                                     .ToList();
        }

        public List<TodoItemModel> GetCompleted(Guid userId)
        {

            return _context/*.TodoItems*/.Where(TodoItem => TodoItem.IsCompleted == true && TodoItem.UserId == userId)
                                     .ToList();
        }

        public List<TodoItemModel> GetFiltered(Func<TodoItemModel, bool> filterFunction, Guid userId)
        {

            return _context/*.TodoItems*/.Where(filterFunction)
                                     .ToList()
                                     .Where(TodoItem => TodoItem.Id == userId)
                                     .ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItemModel findTodoItem = _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoId)
                                            .FirstOrDefault();
            if (findTodoItem == default(TodoItemModel))
                return false;

            if (findTodoItem.UserId == userId)
            {
                if (findTodoItem.IsCompleted == false)
                {
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoId)
                                            .FirstOrDefault().IsCompleted = true;
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoId)
                                            .FirstOrDefault().DateCompleted = DateTime.Now;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                System.ArgumentException TodoAccessDeniedException = new System.ArgumentException("Access denied, user not owner");
                throw TodoAccessDeniedException;
            }
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItemModel findTodoItem = _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoId)
                                            .FirstOrDefault();
            if (findTodoItem != default(TodoItemModel))
            {
                if (findTodoItem.UserId == userId)
                {
                    _context/*.TodoItems*/.Remove(findTodoItem);
                    return true;
                }
                else
                {
                    System.ArgumentException TodoAccessDeniedException = new System.ArgumentException("Access denied, user not owner");
                    throw TodoAccessDeniedException;
                }
            }
            else
            {
                return false;
            }
        }

        public void Update(TodoItemModel todoItem, Guid userId)
        {
            TodoItemModel findTodoItem = _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id).FirstOrDefault();

            if (findTodoItem != default(TodoItemModel))
            {
                if (findTodoItem.UserId == userId)
                {
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id)
                                            .FirstOrDefault().IsCompleted = todoItem.IsCompleted;
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id)
                                            .FirstOrDefault().DateCompleted = todoItem.DateCompleted;
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id)
                                            .FirstOrDefault().Text = todoItem.Text;
                    _context/*.TodoItems*/.Where(TodoItem => TodoItem.Id == todoItem.Id)
                                            .FirstOrDefault().DateCreated = todoItem.DateCreated;
                }
                else
                {
                    System.ArgumentException TodoAccessDeniedException = new System.ArgumentException("Access denied, user not owner");
                    throw TodoAccessDeniedException;
                }

            }
            else
            {
                Add(todoItem);
            }
        }
    }
}

