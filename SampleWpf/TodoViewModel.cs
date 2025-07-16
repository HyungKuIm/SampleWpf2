using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SampleWpf
{
    class TodoViewModel : BindableBase
    {
        private string _newTodoText;  // 새로운 Todo 텍스트
        private TodoItem _selectedItem;  // 선택한 Todo 아이템

        public ObservableCollection<TodoItem> TodoItems { get; } = new();  //  Todo 아이템 목록

        private ICollectionView _todoItemsView;
        public ICollectionView TodoItemsView
        {
            get => _todoItemsView;
            set => SetProperty(ref _todoItemsView, value);
        }

        private FilterType _selectedFilter = FilterType.All;
        public FilterType SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                SetProperty(ref _selectedFilter, value);
                //ApplyFilter();
                TodoItemsView.Refresh();
            }
        }

        // 필터 선택지 목록
        public IEnumerable<FilterType> Filters => Enum.GetValues(typeof(FilterType)).Cast<FilterType>();

        // 프로퍼티
        public string NewTodoText
        {
            get => _newTodoText;
            set => SetProperty(ref _newTodoText, value);
        }

        // 요약용 속성들
        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        private int _completedCount;
        public int CompletedCount
        {
            get => _completedCount;
            set => SetProperty(ref _completedCount, value);
        }

        private int _uncompletedCount;
        public int UncompletedCount
        {
            get => _uncompletedCount;
            set => SetProperty(ref _uncompletedCount, value);
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
            // View 생성 및 필터 연결
            TodoItemsView = CollectionViewSource.GetDefaultView(TodoItems);
            TodoItemsView.Filter = FilterTodoItem;

            AddCommand = new DelegateCommand(AddTodo, CanAddTodo)
                            .ObservesProperty(() => NewTodoText);

            DeleteCommand = new DelegateCommand<TodoItem>(DeleteTodo);
            //ToggleCompleteCommand = new DelegateCommand<TodoItem>(ToggleComplete);

            TodoItems.CollectionChanged += OnTodoItemsChanged;

            foreach (var item in TodoItems)
            {
                SubscribeToItem(item);  
            }
        }

        private bool FilterTodoItem(object obj)
        {
            if (obj is not TodoItem item)
                return false;

            return SelectedFilter switch
            {
                FilterType.All => true,
                FilterType.Completed => item.IsComplete,
                FilterType.Uncompleted => !item.IsComplete,
                _ => true
            };
        }

        private void SubscribeToItem(TodoItem item)
        {
            item.PropertyChanged += OnTodoItemPropertyChanged;
        }

        private void UnsubscribeToItem(TodoItem item)
        {
            item.PropertyChanged -= OnTodoItemPropertyChanged;
        }

        private void OnTodoItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsComplete))
            {
                RefreshStatistics();
            }
        }

        private void OnTodoItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TodoItem item in e.NewItems)
                {
                    SubscribeToItem(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (TodoItem item in e.OldItems)
                {
                    UnsubscribeToItem(item);
                }
            }

            RefreshStatistics();
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
            //RefreshStatistics();
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
            //RefreshStatistics();
        }

        //private void ToggleComplete(TodoItem item)
        //{
        //    Debug.WriteLine("ToggleComplete");
        //    item.IsComplete = !item.IsComplete;
        //}

        private void RefreshStatistics()
        {
            TotalCount = TodoItems.Count;
            CompletedCount = TodoItems.Count(x => x.IsComplete);
            UncompletedCount = TodoItems.Count(x => !x.IsComplete);
        }
    }
}
