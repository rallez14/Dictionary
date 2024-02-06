using System.Collections.Generic;

public interface IDictionary
{
    List<DictionaryEntry> FindWords(string word);
    void AddWord(string word, string partOfSpeech, string definition);
    void SaveChanges(string filePath);
}