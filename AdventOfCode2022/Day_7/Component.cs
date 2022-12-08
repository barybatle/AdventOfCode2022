using System.Reflection.Metadata;

namespace Day_7;

public abstract class Component
{
    public string Name;
    public int Size;

    public Component(string name)
    {
        Name = name;
    }

    public abstract void Add(Component c);
    public abstract void Display(int depth);
}