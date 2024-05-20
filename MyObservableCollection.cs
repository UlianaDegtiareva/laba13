using ClassLibraryHash_Table;
using ClassLibraryLaba10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace laba13
{
    // Определяем делегат для обработки событий изменения коллекции
    public delegate void CollectionHandler<T>(object source, CollectionHandlerEventArgs<T> args);

    // Класс для передачи информации о событиях изменения коллекции
    public class CollectionHandlerEventArgs<T> : EventArgs
    {
        public string ChangeType { get; set; }
        public T ChangedItem { get; set; }

        public CollectionHandlerEventArgs(string changeType, T changedItem)
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
        }
    }

    public class MyObservableCollection<T>: MyCollection_HashTable<T> where T : IInit, IComparable, ICloneable, new()
    {
        // Событие, уведомляющее об изменениях количества элементов в коллекции
        public event CollectionHandler<T> CollectionCountChanged;
        // Событие, уведомляющее об изменениях ссылок в коллекции
        public event CollectionHandler<T> CollectionReferenceChanged;

        public new int Capacity => base.Capacity;
        public new int Count => base.Count;

        public MyObservableCollection() : base() { }
        public MyObservableCollection(int size, double fillRatio) : base(size, fillRatio) { }
        public MyObservableCollection(MyCollection_HashTable<T> c) : base(c) { }
        public MyObservableCollection(params T[] collection) : base(collection) { }

        // Метод для автоматического заполнения коллекции заданным количеством элементов
        public new void FillCollection(int numberOfItems)
        {
            FillTable(numberOfItems);
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnCollectionCountChanged("Элемент добавлен в таблицу", item);
        }

        public new void Print()
        {
            base.Print();
        }

        public new bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (result)
            {
                OnCollectionCountChanged("Элемент удален из таблицы", item);
            }
            return result;
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionCountChanged("Коллекция очищена");
        }

        public T this[string name]
        {
            get
            {
                foreach (var item in this)
                {
                    if (item is Plants nameableItem && nameableItem.Name == name)
                    {
                        return item;
                    }
                }
                return default(T);
            }
        }

        public void Replace(string name, T newItem)
        {
            var oldItem = this[name];
            if (oldItem != null)
            {
                if (base.Remove(oldItem))
                {
                    base.Add(newItem);
                    OnCollectionReferenceChanged("Элемент изменен", newItem);
                }
            }
            else
            {
                Console.WriteLine($"Элемент '{name}' не найден.");
            }
        }

        // Метод для вызова события изменения количества элементов
        public void OnCollectionCountChanged(string changeType, T changedItem)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs<T>(changeType, changedItem));
        }

        // Метод для вызова события изменения ссылок
        public void OnCollectionReferenceChanged(string changeType, T changedItem)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs<T>(changeType, changedItem));
        }

        // Перегрузка метода для вызова события без передачи элемента
        public void OnCollectionCountChanged(string changeType)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs<T>(changeType, default));
        }
    }

}
