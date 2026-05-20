using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoAgilUtveckling.Models;

namespace ToDoAgilUtveckling.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AppDbContext _db;

        private ObservableCollection<ToDoItem> _toDoItems;

        public ObservableCollection<ToDoItem> ToDoItems {
            get => _toDoItems;
            set
            {

                _toDoItems = value;

                RaisePropertyChanged();

            }
        }


        public MainViewModel()
        {

            using (var db = new AppDbContext())
            {
                _db = new AppDbContext();           // saves connection to _db field
                _toDoItems = new ObservableCollection<ToDoItem>(
                    _db.ToDoItems.ToList()          // reads all rows from ToDoItems table
                );
            }


        }

    }

}
