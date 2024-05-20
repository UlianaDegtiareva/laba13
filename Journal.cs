using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba13
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ItemData { get; set; }

        public JournalEntry(string collectionName, string changeType, string itemData)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ItemData = itemData;
        }

        // Перегруженная версия метода ToString
        public override string ToString()
        {
            if (ItemData == null)
            {
                return $"{CollectionName}, Изменения: {ChangeType}";
            }
            else
            {
                return $"{CollectionName}, Изменения: {ChangeType}, Элемент: {ItemData}";
            }
        }
    }

    public class Journal
    {
        // Список записей журнала
        private List<JournalEntry> journal = new List<JournalEntry>();

        // Метод для добавления записи в журнал
        public void AddEntry(JournalEntry entry)
        {
            journal.Add(entry);
        }

        // Метод для вывода всех записей журнала
        public void PrintJournal()
        {
            foreach (var entry in journal)
            {
                Console.WriteLine(entry);
            }
        }
        public bool IsEmpty()
        {
            return journal.Count == 0;
        }
    }
}
