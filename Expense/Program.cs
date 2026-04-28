using System.Text.Json;
using System.Linq;

namespace Expense
{
    class Program
    {
        private static readonly List<ExpenseItem> _expenses = [];
        private const string FilePath = "expenses.json";

        static void Main()
        {
            ReadJsonFile();
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("""
                [1]. Add Expense
                [2]. View Expenses
                [3]. Delete All Expenses
                [4]. Exit
                """.Trim());
                Console.Write("Choose menu: ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter expense name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter expense amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            var newItem = new ExpenseItem
                            {
                                Name = name,
                                Amount = amount,
                                Date = DateTime.Now
                            };

                            _expenses.Add(newItem);
                            SaveExpensesToFile();
                            Console.WriteLine("Expense added.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case "2":
                        if (_expenses.Count == 0)
                        {
                            Console.WriteLine("No expenses recorded.");
                        }
                        else
                        {
                            DisplayExpenses();
                            Console.WriteLine($"Total Expenses is: Rp{_expenses.Sum(x => x.Amount):N0}");
                        }
                        break;

                    case "3":
                        if (_expenses.Count == 0)
                        {
                            Console.WriteLine("No expenses to delete.");
                        }
                        else
                        {
                            Console.WriteLine("Are you sure you want to delete all expenses? (y/n)");
                            string input = Console.ReadLine();

                            if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
                            {
                                _expenses.Clear();
                                SaveExpensesToFile();
                                Console.WriteLine("All expenses deleted.");
                            }
                            else if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Delete cancelled.");
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void DisplayExpenses()
        {
            foreach (var item in _expenses)
            {
                Console.WriteLine($"- [{item.Date:yyyy-MM-dd}] {item.Name}: Rp{item.Amount:N0}");
            }
        }

        private static void SaveExpensesToFile()
        {
            string jsonString = JsonSerializer.Serialize(_expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, jsonString);
            Console.WriteLine("Expenses saved to file.");
        }

        private static void ReadJsonFile()
        {
            if (File.Exists(FilePath))
            {
                SaveExpensesToFile();
                return;
            }
            string jsonString = File.ReadAllText(FilePath);
            List<ExpenseItem> expensesFromFile = JsonSerializer.Deserialize<List<ExpenseItem>>(jsonString);

            _expenses.Clear();
            _expenses.AddRange(expensesFromFile ?? new List<ExpenseItem>());
        }
    }
}

