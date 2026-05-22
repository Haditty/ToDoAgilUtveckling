using System;
using System.Collections.Generic;

namespace ToDoAgilUtveckling.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Category1 { get; set; } = null!;

    public virtual ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
}
