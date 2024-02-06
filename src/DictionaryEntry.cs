using System.Collections.Generic;

public class DictionaryEntry
{
    public string Word { get; set; }
    public string PartOfSpeech { get; set; }
    public List<string> Definitions { get; set; }
}