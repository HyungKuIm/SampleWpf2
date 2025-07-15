using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf
{
    class TodoViewModel : BindableBase
    {
        private string _newTodoText;  // 새로운 Todo 텍스트
        private TodoItem _selectedItem;  // 선택한 Todo 아이템

        public ObservableCollection<TodoItem> TodoItems { get; } = new();  //  Todo 아이템 목록

        // 프로퍼티
        public string NewTodoText
        {
            get => _newTodoText;
            set => SetProperty(ref _newTodoText, value);
        }

        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public DelegateCommand AddCommand { get; }  // Todo추가 커맨드
        public DelegateCommand<TodoItem> DeleteCommand { get; }  // Todo 삭제 커맨드
        //public DelegateCommand<TodoItem> ToggleCompleteCommand { get; } //  완료여부 토글 커맨드

        public TodoViewModel()
        {
            AddCommand = new DelegateCommand(AddTodo, CanAddTodo)
                            .ObservesProperty(() => NewTodoText);

            DeleteCommand = new DelegateCommand<TodoItem>(DeleteTodo);
            //ToggleCompleteCommand = new DelegateCommand<TodoItem>(ToggleComplete);
        }

        private void AddTodo()
        {
            var newItem = new TodoItem
            {
                Id = TodoItems.Count + 1,
                Text = NewTodoText,
                IsComplete = false
            };
            TodoItems.Add(newItem);
            NewTodoText = string.Empty;
        }

        private bool CanAddTodo()
        {
            return !string.IsNullOrWhiteSpace(NewTodoText);
        }

        private void DeleteTodo(TodoItem item)
        {
            Debug.WriteLine("DeleteTodo");
            if (item == null)
            {
                Debug.WriteLine("DeleteTodo: item is null!");
                return;
            }
            TodoItems.Remove(item);
        }

        //private void ToggleComplete(TodoItem item)
        //{
        //    Debug.WriteLine("ToggleComplete");
        //    item.IsComplete = !item.IsComplete;
        //}
    }
}
