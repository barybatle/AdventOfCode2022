using Day_7;

//We do not speak of this one it is shit and I don't have time nor do I want to fix it, it works tho

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

int diskSize = 70000000;
int neededSize = 30000000;

ElfDir rootDir = new ElfDir("/");

var currentDirectory = rootDir;

foreach (var line in lines.Skip(1))
{
    if (line.StartsWith("$ ls"))
    {
        continue;
    }

    if (line.StartsWith("$ cd"))
    {
        var dirName = line.Split(' ')[2];

        if (dirName == "..\r")
        {
            //tutaj jakies liczenie current size i potem parent size += currentsize
            currentDirectory.Size = ElfDir.GetTotalSize(currentDirectory);
            currentDirectory.Parent.Size += currentDirectory.Size;
            currentDirectory = currentDirectory.Parent;
            continue;
        }
        
        currentDirectory = (ElfDir)currentDirectory.Children.Where(x => x.Name == dirName).FirstOrDefault();
        continue;
    }
    
    HandleLine(line, currentDirectory);
}

int unusedSpace = diskSize - CalculateTotalSize(rootDir);

int folderSizeToDelete = neededSize - unusedSpace;

rootDir.Display(1);

Console.WriteLine(ElfDir.GetTotalSize(rootDir));

var largeDirs = GetSubDirectories(rootDir, 100000).Sum(x => x.Size);

var closestSize = FindClosestDirectory(rootDir, folderSizeToDelete).Size;

var test = closestSize + unusedSpace;

Console.ReadKey();

void HandleLine(string line, ElfDir currentDir)
{
    var split = line.Split(' ');
    var name = split[1];
    
    if (split[0] == "dir")
    {
        if (name == "..")
        {
            return;
        }
        currentDir.Add(new ElfDir(name, currentDir));
        return;
    }

    var size = int.Parse(split[0]);
    
    currentDir.Add(new ElfFile(name, size, currentDir));
}

List<ElfDir> GetSubDirectories(ElfDir elfDir, int size)
{
    var subDirectories = new List<ElfDir>();

    foreach (ElfDir subDir in elfDir.Children.OfType<ElfDir>())
    {
        if (ElfDir.GetTotalSize(subDir) <= size)
        {
            subDirectories.Add(subDir);
        }
            
        subDirectories.AddRange(GetSubDirectories(subDir, size));
    }

    return subDirectories;
}

ElfDir FindClosestDirectory(ElfDir elfDirectory, int targetSize)
{
    ElfDir closestDirectory = null;

    int closestSize = int.MaxValue;

    if (ElfDir.GetTotalSize(elfDirectory) >= targetSize)
    {
        if (Math.Abs(targetSize - ElfDir.GetTotalSize(elfDirectory)) < Math.Abs(targetSize - closestSize))
        {
            closestDirectory = elfDirectory;
            closestSize = ElfDir.GetTotalSize(elfDirectory);
        }
    }
    
    foreach (ElfDir subDir in elfDirectory.Children.OfType<ElfDir>())
    {
        ElfDir closestSubDirectory = FindClosestDirectory(subDir, targetSize);
        if (closestSubDirectory != null && Math.Abs(targetSize - ElfDir.GetTotalSize(closestSubDirectory)) < Math.Abs(targetSize - closestSize))
        {
            closestDirectory = closestSubDirectory;
            closestSize = ElfDir.GetTotalSize(closestSubDirectory);
        }
    }

    return closestDirectory;
}

int CalculateTotalSize(ElfDir elfDirectory)
{
    var totalSize = 0;

    // Get the total size of the files in the current directory
    var filesSize = elfDirectory.Children.OfType<ElfFile>().Sum(x => x.Size);
    totalSize += filesSize;

    // Recursively calculate the total size of the children directories
    foreach (ElfDir subDir in elfDirectory.Children.OfType<ElfDir>())
    {
        totalSize += CalculateTotalSize(subDir);
    }

    return totalSize;
}