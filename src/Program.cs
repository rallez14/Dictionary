class Program
{
    static void Main()
    {
        Console.Title = "Dictionary";

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dictionary.json");
        Dictionary dictionary = new Dictionary(filePath);
        UserInterface userInterface = new UserInterface(dictionary);
        userInterface.Run();
    }
}