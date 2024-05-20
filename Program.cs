using ClassLibraryLaba10;
using ClassLibraryHash_Table;

namespace laba13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyObservableCollection<Plants> table1 = Сreating();
            MyObservableCollection<Plants> table2 = Сreating();
            Console.WriteLine("Коллекция №1");
            table1.Print();
            Console.WriteLine();

            Console.WriteLine("Коллекция №2");
            table2.Print();
            Console.WriteLine();

            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            // Подписываем journal1 на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            table1.CollectionCountChanged += (source, args) => journal1.AddEntry(new JournalEntry("Коллекция №1", args.ChangeType, args.ChangedItem?.ToString()));
            table1.CollectionReferenceChanged += (source, args) => journal1.AddEntry(new JournalEntry("Коллекция №1", args.ChangeType, args.ChangedItem?.ToString()));

            // Подписываем journal2 на событие CollectionReferenceChanged из обеих коллекций
            table1.CollectionReferenceChanged += (source, args) => journal2.AddEntry(new JournalEntry("Коллекция №1", args.ChangeType, args.ChangedItem?.ToString()));
            table2.CollectionReferenceChanged += (source, args) => journal2.AddEntry(new JournalEntry("Коллекция №2", args.ChangeType, args.ChangedItem?.ToString()));

            int answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("НЕОБХОДИМО ВЫБРАТЬ КОЛЛЕКЦИЮ ДЛЯ РАБОТЫ------");
                Console.WriteLine("1. Работа с коллекцией №1");
                Console.WriteLine("2. Работа с коллекцией №2");
                Console.WriteLine("3. Печать журнала №1 ");
                Console.WriteLine("4. Печать журнала №2");
                Console.WriteLine("5. Выход");
                Console.WriteLine();
                answer = InputAnswer();
                Console.WriteLine();
                switch (answer)
                {
                    case 1:
                        WorkWithCollection(table1, "Коллекция №1");
                        break;
                    case 2:
                        WorkWithCollection(table2, "Коллекция №2");
                        break;
                    case 3:
                        PrintJournal(journal1, "Журнал №1");
                        break;
                    case 4:
                        PrintJournal(journal2, "Журнал №2");
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Неправильно задан пункт меню");
                        break;
                }
            } while (answer != 5);

            Console.ReadLine();
        }

        static void WorkWithCollection(MyObservableCollection<Plants> table, string collectionName)
        {
            int ans;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"РАБОТА С {collectionName}-");
                Console.WriteLine("1. Добавление элементов");
                Console.WriteLine("2. Удаление элементов");
                Console.WriteLine("3. Очищение коллекции");
                Console.WriteLine("4. Изменение элемента");
                Console.WriteLine("5. Печать коллекции");
                Console.WriteLine("6. Назад");
                ans = InputAnswer();
                Console.WriteLine();
                switch (ans)
                {
                    case 1:
                        Add(table);
                        break;
                    case 2:
                        Remove(table);
                        break;
                    case 3:
                        Clear(table);
                        break;
                    case 4:
                        Change(table);
                        break;
                    case 5:
                        table.Print();
                        break;
                    case 6:
                        break;
                }
            } while (ans != 6);
        }

        static void Add(MyObservableCollection<Plants> table)
        {
            int type = Menu();
            Plants element = CreateElementByType(type);
            if (element != null)
            {
                element.RandomInit();
                if (table.Contains(element))
                {
                    Console.WriteLine("Элемент с таким значением уже присутствует в таблице");
                }
                else
                {
                    table.Add(element);
                    Console.WriteLine($"{element.GetType().Name} добавлен");
                }
            }
        }

        static void Remove(MyObservableCollection<Plants> table)
        {
            if (table.Count == 0)
            {
                Console.WriteLine("Таблица пуста. Необходимо добавить элементы перед продолжением.");
                return;
            }

            int type = Menu();
            Plants element = CreateElementByType(type);
            if (element != null)
            {
                element.Init();
                if (table.Remove(element))
                {
                    Console.WriteLine("Элемент удален");
                }
                else
                {
                    Console.WriteLine("Элемента нет в таблице");
                }
            }
        }

        static void Change(MyObservableCollection<Plants> table)
        {
            if (table.Count == 0)
            {
                Console.WriteLine("Таблица пуста. Необходимо добавить элементы перед продолжением.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("Выберите, какой элемент вы хотите изменить:");
            int type = Menu();
            Plants element = CreateElementByType(type);
            if (element != null)
            {
                element.Init();
                if (!table.Contains(element))
                {
                    Console.WriteLine("Элемент не найден в коллекции.");
                    return;
                }

                Plants newItem = new Plants();
                newItem.RandomInit();
                if (table.Contains(newItem))
                {
                    Console.WriteLine("Элемент с таким значением уже присутствует в таблице");
                }
                else
                {
                    table.Replace(element.Name, newItem);
                    Console.WriteLine("Элемент изменен.");
                }
            }
        }

        static void Clear(MyObservableCollection<Plants> table)
        {
            if (table.Count == 0)
            {
                Console.WriteLine("Коллекция пуста, очищение невозможно");
            }
            else
            {
                table.Clear();
                Console.WriteLine("Коллекция очищена");
            }
        }

        static void PrintJournal(Journal journal, string journalName)
        {
            if (journal.IsEmpty())
            {
                Console.WriteLine("Журнал пуст.");
            }
            else
            {
                Console.WriteLine(journalName);
                Console.WriteLine();
                journal.PrintJournal();
            }
        }

        static MyObservableCollection<Plants> Сreating()
        {
            MyObservableCollection<Plants> table = new MyObservableCollection<Plants>(CreatePlant(), CreateTree(), CreateFlower(), CreateRose(), CreatePlant());
            return table;
        }

        static Plants CreateElementByType(int type)
        {
            switch (type)
            {
                case 1:
                    return new Plants();
                case 2:
                    return new Trees();
                case 3:
                    return new Flowers();
                case 4:
                    return new Rose();
                default:
                    return null;
            }
        }

        static Plants CreatePlant()
        {
            Plants plant = new Plants();
            plant.RandomInit();
            return plant;
        }

        static Trees CreateTree()
        {
            Trees tree = new Trees();
            tree.RandomInit();
            return tree;
        }

        static Flowers CreateFlower()
        {
            Flowers flower = new Flowers();
            flower.RandomInit();
            return flower;
        }

        static Rose CreateRose()
        {
            Rose rose = new Rose();
            rose.RandomInit();
            return rose;
        }

        static int Menu()
        {
            Console.WriteLine("1. Растение (базовый класс)");
            Console.WriteLine("2. Дерево (производный класс)");
            Console.WriteLine("3. Цветок (производный класс)");
            Console.WriteLine("4. Роза (производный класс)");
            Console.WriteLine("5. Назад");
            Console.WriteLine();
            return InputAnswer();
        }

        static int InputAnswer()
        {
            int answer;
            bool Ok;
            do
            {
                string buf = Console.ReadLine();
                Ok = int.TryParse(buf, out answer);
                if (!Ok)
                {
                    Console.WriteLine("Неправильно выбран пункт меню. Повторите ввод");
                }
            } while (!Ok);
            return answer;
        }
    }
}
