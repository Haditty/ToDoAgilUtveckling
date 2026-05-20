using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoAgilUtveckling.Commands;
using ToDoAgilUtveckling.Models;

namespace ToDoAgilUtveckling.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public DelegateCommand AddItemCommand { get; }
        public DelegateCommand DeleteItemCommand { get; }

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
                if (_selectedIndex >= 0 && _selectedIndex < ToDoItems.Count)
                {
                    SelectedItem = ToDoItems[_selectedIndex];
                }
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
            AddItemCommand = new DelegateCommand(AddTask, CanAddTask);
            DeleteItemCommand = new DelegateCommand(RemoveTask, CanRemoveTask);
            using (var db = new AppDbContext())
            {
                     
                _toDoItems = new ObservableCollection<ToDoItem>(
                    db.ToDoItems.ToList()        
                );
            }
            SelectedIndex = 0;
        }

        private void AddTask(object? obj)
        {
            ToDoItems.Add(new ToDoItem()
            {
                Id = ToDoItems.Count +1,
                Title = "",
                Description = "",
                CategoryId = 1,
                IsDone = false
            });
        }

        private void RemoveTask(object? obj)
        {
            ToDoItems.Remove(ToDoItems[SelectedIndex]);
        }
        private bool CanAddTask (object? obj)
        {
            return true;
        }

        private bool CanRemoveTask(object? obj)
        {
            return true;
        }
    }
}
