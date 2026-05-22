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
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand AddItemCommand { get; }
        public DelegateCommand DeleteItemCommand { get; }
        public DelegateCommand UpdateCommand { get; }

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
            UpdateCommand = new DelegateCommand(UpdateItem, CanUpdateItem);
            CancelCommand = new DelegateCommand (CancelTask, CanCancelTask);
            using (var db = new AppDbContext())
            {
                     
                ToDoItems = new ObservableCollection<ToDoItem>(
                    db.ToDoItems.ToList()        
                );
            }
            SelectedIndex = 0;
        }

        private void UpdateItem(object? obj)
        {
            if (SelectedItem == null)
                return;
            using (var db = new AppDbContext())
            {
                db.ToDoItems.Update(SelectedItem);
                db.SaveChanges();
            }

        }

        private bool CanUpdateItem(object? obj)
        {
            return true;
        }

        private void AddTask(object? obj)
        {
            var item = new ToDoItem()
            {
                Title = "",
                Description = "",
                CategoryId = 1,
                IsDone = false
            };

            ToDoItems.Add(item);
       

            using (var db = new AppDbContext())
            {
                db.ToDoItems.Add(item);
                db.SaveChanges();
            }
            
        }

        private void CancelTask (object? obj)
        {
            using (var db = new AppDbContext())
            {
                ToDoItems = new ObservableCollection<ToDoItem>(
                    db.ToDoItems.ToList()
                );
            }
            var temp = SelectedIndex;
        }

        private bool CanCancelTask (object? obj)
        {
            return true;
        }

        private void RemoveTask(object? obj)
        {
            if (SelectedItem == null)
                return;

            using (var db = new AppDbContext())
            {

                db.ToDoItems.Remove(SelectedItem);
                db.SaveChanges();
            }
            ToDoItems.Remove(SelectedItem);
            SelectedItem = null;
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
