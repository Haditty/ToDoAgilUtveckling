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
        private ToDoItem _selectedItem;

        public ToDoItem SelectedItem
        {
            get => _selectedItem;

            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
                if (_selectedIndex >= 0 && _selectedIndex < ToDoItems.Count) { SelectedItem = ToDoItems[_selectedIndex]; }
                SelectedItem = ToDoItems[SelectedIndex];
            }
        }
        

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
                     
                _toDoItems = new ObservableCollection<ToDoItem>(
                    db.ToDoItems.ToList()        
                );
            }
          
        }
    }
}
