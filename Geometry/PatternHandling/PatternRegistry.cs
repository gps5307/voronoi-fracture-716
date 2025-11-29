public static class PatternRegistry
{
    public static Dictionary<string, PatternDefinition> Patterns { get; private set; }

    static PatternRegistry()
    {
        string json = File.ReadAllText("patterns.json");
        Patterns = JsonConvert.DeserializeObject<Dictionary<string, PatternDefinition>>(json);
    }

    public static PatternDefinition Get(string name)
    {
        return Patterns.ContainsKey(name)
            ? Patterns[name]
            : null;
    }
}