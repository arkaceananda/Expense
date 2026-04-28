class Expense
{
    private static readonly List<int> _expenses = new List<int>();

    static void Main()
    {
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
                    Console.WriteLine("Enter expense amount: ");
                    if (int.TryParse(Console.ReadLine(), out int amount))
                    {
                        _expenses.Add(amount);
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
                        Console.WriteLine($"Total Expenses is: Rp{_expenses.Sum():N0}");
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
            Console.WriteLine($"- {item:N0}");
        }
    }
}

    