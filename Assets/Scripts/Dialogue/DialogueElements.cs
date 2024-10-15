using System.Collections.Generic;

[System.Serializable]
public struct DialogueElements
{
    public List<string> character;
    public string text;
    public List<string> go_to;
    public List<string> options;
    public float? wait_seconds;
    public string function;
    public string if_condition;
    public string add;
    public string dialogue_type;
}
