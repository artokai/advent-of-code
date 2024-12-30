# Advent of code

[Advent of Code](https://adventofcode.com) is a yearly Advent calendar of small programming puzzles.
This repository contains my solutions to the puzzles I've solved so far. The repository does not contain the actual inputs for the puzzles as those should be kept secret and can vary between users.

## Year 2024

| Day | Title | Part A | Part B |
| --: | :---- | :----- | :----- |
| 1 | [Historian Hysteria](https://adventofcode.com/2024/day/1) | [Part 1](Puzzles/Y2024/D01/PartA.cs) | [Part 2](Puzzles/Y2024/D01/PartB.cs) |
| 2 | [???](https://adventofcode.com/2024/day/2) |  |  |
| 3 | [???](https://adventofcode.com/2024/day/3) |  |  |
| 4 | [???](https://adventofcode.com/2024/day/4) |  |  |
| 5 | [???](https://adventofcode.com/2024/day/5) |  |  |
| 6 | [???](https://adventofcode.com/2024/day/6) |  |  |
| 7 | [???](https://adventofcode.com/2024/day/7) |  |  |
| 8 | [???](https://adventofcode.com/2024/day/8) |  |  |
| 9 | [???](https://adventofcode.com/2024/day/9) |  |  |
| 10 | [???](https://adventofcode.com/2024/day/10) |  |  |
| 11 | [???](https://adventofcode.com/2024/day/11) |  |  |
| 12 | [???](https://adventofcode.com/2024/day/12) |  |  |
| 13 | [???](https://adventofcode.com/2024/day/13) |  |  |
| 14 | [???](https://adventofcode.com/2024/day/14) |  |  |
| 15 | [???](https://adventofcode.com/2024/day/15) |  |  |
| 16 | [???](https://adventofcode.com/2024/day/16) |  |  |
| 17 | [???](https://adventofcode.com/2024/day/17) |  |  |
| 18 | [???](https://adventofcode.com/2024/day/18) |  |  |
| 19 | [???](https://adventofcode.com/2024/day/19) |  |  |
| 20 | [???](https://adventofcode.com/2024/day/20) |  |  |
| 21 | [???](https://adventofcode.com/2024/day/21) |  |  |
| 22 | [???](https://adventofcode.com/2024/day/22) |  |  |
| 23 | [???](https://adventofcode.com/2024/day/23) |  |  |
| 24 | [???](https://adventofcode.com/2024/day/24) |  |  |
| 25 | [???](https://adventofcode.com/2024/day/25) |  |  |

## All years

- [2024](Puzzles/Y2024/README.md)
- [2015](Puzzles/Y2015/README.md)

## Solving a puzzle

The solution includes a runner, which can be used to solve a puzzle for a certain day.
It will also automatically fetch the `input.txt` from adventofcode.com if it is missing and you have copied your session cookie to `applicationsettings.development.json`

```
cd CliTool
dotnet run -- solve --year <year> --day <day>
```

Another way to run a puzzle solver is to run the daily solver directly.
This is useful when you're still developing the answer to the puzzle or need to debug it.
This approach does not automatically fetch the `input.txt` automatically, so you may need
to fetch it manually and place it in the same directory.

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
