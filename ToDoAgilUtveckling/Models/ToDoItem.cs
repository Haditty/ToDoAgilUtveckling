using System;
using System.Collections.Generic;

namespace ToDoAgilUtveckling.Models;

public partial class ToDoItem
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public bool? IsDone { get; set; }

    public DateTime? Date { get; set; }

    public virtual Category Category { get; set; } = null!;
}
