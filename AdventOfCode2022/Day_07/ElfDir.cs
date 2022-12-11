namespace Day_7;

public class ElfDir : Component
{
    public List<Component> Children = new List<Component>();
    public ElfDir? Parent { get; set; }
    
    public int Size { get; set; }
    
    public ElfDir(string name) : base(name)
    {
    }

    public ElfDir(string name, ElfDir parent) : base(name)
    {
        Name = name;
        Parent = parent;
    }
    
    public override void Add(Component component)
    {
        Children.Add(component);
    }
    
    public override void Display(int depth)
    {
        Console.WriteLine(new String(' ', depth) + "- " + Name);
        // Recursively display child nodes
        foreach (Component component in Children)
        {
            component.Display(depth + 2);
        }
    }
    
    public static int GetTotalSize(ElfDir elfDirectory)
    {
        var totalSize = 0;
        
        var filesSize = elfDirectory.Children.OfType<ElfFile>().Sum(x => x.Size);

        totalSize += filesSize;
        
        foreach (ElfDir subDir in elfDirectory.Children.OfType<ElfDir>())
        {
            totalSize += GetTotalSize(subDir);
        }

        return totalSize;
    }
}