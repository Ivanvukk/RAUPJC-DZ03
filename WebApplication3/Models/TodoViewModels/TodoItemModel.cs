using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.TodoViewModels
{
    public class TodoItemModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        /// <summary >
        /// Nullable date time .
        /// DateTime is value type and won ’t allow nulls .
        /// DateTime ? is nullable DateTime and will accept nulls .
        /// Use null when todo completed date does not exist (e.g. todo is still not completed )
        /// </ summary >
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary >
        /// User id that owns this TodoItem
        /// </ summary >
        public Guid UserId { get; set; }


        public TodoItemModel(string text, Guid userId)
        {
            Id = Guid.NewGuid();                    // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;             // Set creation date as current time
            UserId = userId;
        }

        public TodoItemModel()
        {
            // entity framework needs this one
            // not for use :)
        }
    }
}
