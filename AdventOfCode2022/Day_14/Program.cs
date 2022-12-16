//again made possible by https://www.youtube.com/@CaptainCoder
using Day_14.Models;

var lines = File.ReadAllLines("input.txt");

var cave = Cave.Parse(lines);

do
{
    //Console.Clear();
    //Console.WriteLine(cave.PrintWindow(new Position(485, -5), new Position(515, 11)));
    //Thread.Sleep(1);
    if (cave.SettledSand.Contains(new Position(500, 0)))
    {
        break;
    }

} while (cave.DropSand());

Console.WriteLine(cave.SettledSand.Count);