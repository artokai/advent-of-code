# Advent of code

[Advent of Code](https://adventofcode.com) is a yearly Advent calendar of small programming puzzles.
This repository contains my solutions to the puzzles I've solved so far. The repository does not contain the actual inputs for the puzzles as those should be kept secret and can vary between users.

## Year 2024

| Day | Title | Part 1 | Part 2 |
| --: | :---- | :----- | :----- |
| 1 | [Historian Hysteria](https://adventofcode.com/2024/day/1) | [Part 1](Puzzles/Y2024/D01/PartA.cs) | [Part 2](Puzzles/Y2024/D01/PartB.cs) |
| 2 | [Red-Nosed Reports](https://adventofcode.com/2024/day/2) | [Part 1](Puzzles/Y2024/D02/PartA.cs) | [Part 2](Puzzles/Y2024/D02/PartB.cs) |
| 3 | [Mull It Over](https://adventofcode.com/2024/day/3) | [Part 1](Puzzles/Y2024/D03/PartA.cs) | [Part 2](Puzzles/Y2024/D03/PartB.cs) |
| 4 | [Ceres Search](https://adventofcode.com/2024/day/4) | [Part 1](Puzzles/Y2024/D04/PartA.cs) | [Part 2](Puzzles/Y2024/D04/PartB.cs) |
| 5 | [Print Queue](https://adventofcode.com/2024/day/5) | [Part 1](Puzzles/Y2024/D05/PartA.cs) | [Part 2](Puzzles/Y2024/D05/PartB.cs) |
| 6 | [Guard Gallivant](https://adventofcode.com/2024/day/6) | [Part 1](Puzzles/Y2024/D06/PartA.cs) | [Part 2](Puzzles/Y2024/D06/PartB.cs) |
| 7 | [Bridge Repair](https://adventofcode.com/2024/day/7) | [Part 1](Puzzles/Y2024/D07/PartA.cs) | [Part 2](Puzzles/Y2024/D07/PartB.cs) |
| 8 | [Resonant Collinearity](https://adventofcode.com/2024/day/8) | [Part 1](Puzzles/Y2024/D08/PartA.cs) | [Part 2](Puzzles/Y2024/D08/PartB.cs) |
| 9 | [Disk Fragmenter](https://adventofcode.com/2024/day/9) | [Part 1](Puzzles/Y2024/D09/PartA.cs) | [Part 2](Puzzles/Y2024/D09/PartB.cs) |
| 10 | [Hoof It](https://adventofcode.com/2024/day/10) | [Part 1](Puzzles/Y2024/D10/PartA.cs) | [Part 2](Puzzles/Y2024/D10/PartB.cs) |
| 11 | [Plutonian Pebbles](https://adventofcode.com/2024/day/11) | [Part 1](Puzzles/Y2024/D11/PartA.cs) | [Part 2](Puzzles/Y2024/D11/PartB.cs) |
| 12 | [Garden Groups](https://adventofcode.com/2024/day/12) | [Part 1](Puzzles/Y2024/D12/PartA.cs) | [Part 2](Puzzles/Y2024/D12/PartB.cs) |
| 13 | [Claw Contraption](https://adventofcode.com/2024/day/13) | [Part 1](Puzzles/Y2024/D13/PartA.cs) | [Part 2](Puzzles/Y2024/D13/PartB.cs) |
| 14 | [Restroom Redoubt](https://adventofcode.com/2024/day/14) | [Part 1](Puzzles/Y2024/D14/PartA.cs) | [Part 2](Puzzles/Y2024/D14/PartB.cs) |
| 15 | [???](https://adventofcode.com/2024/day/15) |  |  |
| 16 | [???](https://adventofcode.com/2024/day/16) |  |  |
| 17 | [Chronospatial Computer](https://adventofcode.com/2024/day/17) | [Part 1](Puzzles/Y2024/D17/PartA.cs) | [Part 2](Puzzles/Y2024/D17/PartB.cs) |
| 18 | [???](https://adventofcode.com/2024/day/18) |  |  |
| 19 | [???](https://adventofcode.com/2024/day/19) |  |  |
| 20 | [???](https://adventofcode.com/2024/day/20) |  |  |
| 21 | [???](https://adventofcode.com/2024/day/21) |  |  |
| 22 | [???](https://adventofcode.com/2024/day/22) |  |  |
| 23 | [???](https://adventofcode.com/2024/day/23) |  |  |
| 24 | [???](https://adventofcode.com/2024/day/24) |  |  |
| 25 | [Code Chronicle](https://adventofcode.com/2024/day/25) | [Part 1](Puzzles/Y2024/D25/PartA.cs) |  |

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
