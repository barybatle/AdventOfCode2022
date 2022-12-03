var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

var score = 0;

// A = rock
// B = paper
// C = scissors

// X = lose
// Y = draw
// Z = win

var scoreDict = new Dictionary<string, int>()
{
    {"A", 1},
    {"B", 2},
    {"C", 3}
};

foreach (var line in lines)
{
    var rps = line.Split(" ");
    var opponent = rps[0];
    var me = rps[1];

    switch (me)
    {
        case "X":
            score += 0;
            score += scoreDict[GetLoser(opponent)];
            break;
        case "Y":
            score += 3;
            score += scoreDict[opponent];
            break;
        case "Z":
            score += 6;
            score += scoreDict[GetWinner(opponent)];
            break;
    }
}

Console.WriteLine(score);

Console.ReadKey();

string GetWinner(string move)
{
    switch (move)
    {
        case "A":
            return "B";
        case "B":
            return "C";
        case "C":
            return "A";
    }

    return "";
}

string GetLoser(string move)
{
    switch (move)
    {
        case "A":
            return "C";
        case "B":
            return "A";
        case "C":
            return "B";
    }

    return "";
}

//Part 1 is missing cuz of me forgetting to commit lol