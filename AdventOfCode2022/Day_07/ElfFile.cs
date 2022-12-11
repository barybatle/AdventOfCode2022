using Day_7;

public class ElfFile : Component
{
    public string Name { get; }
    public int Size { get; }
    public ElfDir Parent { get; set; }

    public ElfFile(string name, int size, ElfDir parent) : base(name)
    {
        Name = name;
        Size = size;
        Parent = parent;
    }
    
    public override void Add(Component c)
    {
        throw new NotImplementedException();
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String(' ', depth) + "- " + base.Name);
    }
}