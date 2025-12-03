# Advent of code

[Advent of Code](https://adventofcode.com) is a yearly Advent calendar of small programming puzzles.
This repository contains my solutions to the puzzles I've solved so far. The repository does not contain the actual inputs for the puzzles as those should be kept secret and can vary between users.

## Year 2025

| Day | Title | Part 1 | Part 2 |
| --: | :---- | :----- | :----- |
| 1 | [Secret Entrance](https://adventofcode.com/2025/day/1) | [Part 1](Puzzles/Y2025/D01/PartA.cs) | [Part 2](Puzzles/Y2025/D01/PartB.cs) |
| 2 | [Gift Shop](https://adventofcode.com/2025/day/2) | [Part 1](Puzzles/Y2025/D02/PartA.cs) | [Part 2](Puzzles/Y2025/D02/PartB.cs) |
| 3 | [Lobby](https://adventofcode.com/2025/day/3) | [Part 1](Puzzles/Y2025/D03/PartA.cs) | [Part 2](Puzzles/Y2025/D03/PartB.cs) |
| 4 | [???](https://adventofcode.com/2025/day/4) |  |  |
| 5 | [???](https://adventofcode.com/2025/day/5) |  |  |
| 6 | [???](https://adventofcode.com/2025/day/6) |  |  |
| 7 | [???](https://adventofcode.com/2025/day/7) |  |  |
| 8 | [???](https://adventofcode.com/2025/day/8) |  |  |
| 9 | [???](https://adventofcode.com/2025/day/9) |  |  |
| 10 | [???](https://adventofcode.com/2025/day/10) |  |  |
| 11 | [???](https://adventofcode.com/2025/day/11) |  |  |
| 12 | [???](https://adventofcode.com/2025/day/12) |  |  |

## All years

- [2025](Puzzles/Y2025/README.md) (3 / 12)
- [2024](Puzzles/Y2024/README.md) (20 / 25)
- [2017](Puzzles/Y2017/README.md) (25 / 25)
- [2016](Puzzles/Y2016/README.md) (25 / 25)
- [2015](Puzzles/Y2015/README.md) (25 / 25)

## Solving a puzzle

The solution includes a runner, which can be used to solve a puzzle for a certain day.
It will also automatically fetch the `input.txt` from adventofcode.com if it is missing and you have copied your session cookie to `appsettings.development.json`

```
cd CliTool
dotnet run -- solve --year <year> --day <day>
```

Another way to run a puzzle solver is to run the daily solver directly.
This is useful when you're still developing the answer to the puzzle or need to debug it.
This approach does not automatically fetch the `input.txt`, so you may need
to fetch it either manually or by using the clitool's `fetch` command. 

```
cd Puzzles/Y2015/D01
dotnet run
```

## Generating a new solution

When solving new puzzles, you can automatically generate a solution template for it using the
provided runner. This will generate a blank project for you in the correct directory and also
fetch the `input.txt` from adventofcode.com.

You can omit the year and day parameters, if you're working with current date's puzzle.

```
cd CliTool
dotnet run -- init --year <year> --day <day>
```

## Fetching missing input

The runner can also be used to fetch only the `input.txt` for existing solutions.

```
cd CliTool
dotnet run -- fetch --year <year> --day <day>
```
