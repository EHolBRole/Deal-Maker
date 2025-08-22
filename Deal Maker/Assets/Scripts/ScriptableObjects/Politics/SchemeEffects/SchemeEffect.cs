using UnityEngine;

public abstract class SchemeEffect : ScriptableObject
{
    [TextArea] public string description; // For designer reference

    public abstract void Apply(DemonState executor, DemonState target);
}