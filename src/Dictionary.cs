using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

public class Dictionary : IDictionary
{
    private List<DictionaryEntry> dictionaryEntries;
    private string filePath;

    public Dictionary(string filePath)
    {
        this.filePath = filePath;
        LoadDictionary();
    }

    private void LoadDictionary()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Dictionary file not found. Downloading from the internet...");
                DownloadDictionary();
                Console.Clear();
            }

            string jsonData = File.ReadAllText(filePath);
            dictionaryEntries = JsonConvert.DeserializeObject<List<DictionaryEntry>>(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the dictionary: {ex.Message}");
            dictionaryEntries = new List<DictionaryEntry>();
        }
    }

    private void DownloadDictionary()
    {
        try
        {
            WebClient webClient = new WebClient();
            const string downloadUrl = "https://raw.githubusercontent.com/rallez14/DictionaryData/master/dictionary.json";
            webClient.DownloadFile(downloadUrl, filePath);
            Console.WriteLine("Dictionary file downloaded successfully.");
            Thread.Sleep(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while downloading the dictionary file: {ex.Message}");
            Thread.Sleep(2000);
        }
    }

    public List<DictionaryEntry> FindWords(string word)
    {
        return dictionaryEntries
            .Where(entry => entry.Word.Equals(word, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void AddWord(string word, string partOfSpeech, string definition)
    {
        var existingEntries = FindWords(word);

        if (existingEntries.Any())
        {
            Console.WriteLine($"Word '{word}' already exists in the dictionary.");
            Thread.Sleep(2000);
        }
        else
        {
            var newEntry = new DictionaryEntry
            {
                Word = word,
                PartOfSpeech = partOfSpeech,
                Definitions = new List<string> { definition }
            };

            dictionaryEntries.Add(newEntry);
            Console.WriteLine($"Word '{word}' added to the dictionary.");
            SaveChanges(filePath);
            Thread.Sleep(2000);
        }
    }

    public void SaveChanges(string filePath)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(dictionaryEntries, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine("Changes saved to the dictionary file.");
            Thread.Sleep(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving changes to the dictionary file: {ex.Message}");
            Thread.Sleep(2000);
        }
    }
}