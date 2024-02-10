public class UserInterface
{
    private IDictionary dictionary;

    public UserInterface(IDictionary dictionary)
    {
        this.dictionary = dictionary;
    }

    public void Run()
    {
        int choice;
        do
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Find Word");
            Console.WriteLine("2. Add Word");
            Console.WriteLine("3. Remove Word");
            Console.WriteLine("4. Exit");

            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        FindWord();
                        break;
                    case 2:
                        AddWord();
                        break;
                    case 3:
                        RemoveWord();
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        Thread.Sleep(2000);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Thread.Sleep(2000);
            }
        } while (choice != 4);
    }

    private void FindWord()
    {
        Console.Clear();
        Console.Write("Enter the word to find: ");
        string searchWord = Console.ReadLine();
        List<DictionaryEntry> foundEntries = dictionary.FindWords(searchWord);

        if (foundEntries.Any())
        {
            Console.Clear();
            int currentDefinitionIndex = 0;

            do
            {
                DisplayDefinitions(foundEntries[currentDefinitionIndex].Word,
                    foundEntries[currentDefinitionIndex].PartOfSpeech,
                    foundEntries[currentDefinitionIndex].Definitions);

                int lastIndex = foundEntries.Count - 1;

                if (lastIndex > 0)
                {
                    Console.WriteLine("1. Continue");
                    Console.WriteLine("2. Go back");
                    Console.WriteLine("3. Exit");

                    if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                currentDefinitionIndex = (currentDefinitionIndex + 1) % (lastIndex + 1);
                                break;
                            case 2:
                                Console.Clear();
                                currentDefinitionIndex =
                                    (currentDefinitionIndex - 1 + (lastIndex + 1)) % (lastIndex + 1);
                                break;
                            case 3:
                                return;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
                else
                {
                    Console.WriteLine("1. Continue");
                    Console.WriteLine("2. Exit");

                    if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                break;
                            case 2:
                                return;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            } while (true);
        }
        else
        {
            Console.WriteLine($"Word '{searchWord}' not found in the dictionary.");
            Thread.Sleep(2000);
        }
    }

    private void AddWord()
    {
        Console.Clear();
        Console.Write("Enter the word to add: ");
        string word = Console.ReadLine();
        Console.Write("Enter the part of speech: ");
        string partOfSpeech = Console.ReadLine();
        Console.Write("Enter the definition: ");
        string definition = Console.ReadLine();

        dictionary.AddWord(word, partOfSpeech, definition);
    }

    public static void DisplayDefinitions(string word, string partOfSpeech, List<string> definitions)
    {
        Console.Clear();
        Console.WriteLine($"Word: {word}");
        Console.WriteLine($"Part of Speech: {partOfSpeech}");
        Console.WriteLine("Definitions:");

        foreach (var definition in definitions)
        {
            Console.WriteLine($"- {definition}");
        }
    }

    public static void DisplayDefinition(DictionaryEntry entry)
    {
        Console.Clear();
        Console.WriteLine($"Word: {entry.Word}");
        Console.WriteLine($"Part of Speech: {entry.PartOfSpeech}");
        Console.WriteLine("Definitions:");

        foreach (var definition in entry.Definitions)
        {
            Console.WriteLine($"- {definition}");
        }
    }

    private void RemoveWord()
    {
        Console.Clear();
        Console.Write("Enter the word to remove: ");
        string wordToRemove = Console.ReadLine();
        dictionary.RemoveWord(wordToRemove);
    }
}