using System.Diagnostics;
using Artokai.AOC.Core;
using Microsoft.Z3;

namespace Artokai.AOC.Puzzles.Y2025.D10;

[PuzzleInfo(year: 2025, day: 10, part: 2, title: "Factory")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var configs = Input.AsLines()
            .Select(line =>
            {
                var parts = line.Split(" ");
                // ignore the lights for part B
                var buttons = parts[1..^1].Select(btn => btn[1..^1].Split(',').Select(int.Parse).ToList()).ToList();
                var joltages = parts[^1][1..^1].Split(',').Select(int.Parse).ToList();
                return (joltages, buttons);
            });

        // Reuse the Z3 context for all machines to speed things up
        using var z3ctx = new Context();

        var result = configs
            .Select((config, index) => SolveSingleMachine(index, z3ctx, config.joltages, config.buttons))
            .Sum();
        return result.ToString();
    }

    private long SolveSingleMachine(int machineIndex, Context z3ctx, List<int> targetJoltages, List<List<int>> buttons)
    {
        // This is best solved using an ILP solver like Z3
        var optimizer = z3ctx.MkOptimize();

        // Create integer variables for each button press count
        var buttonVars = buttons.Select((_, idx) => z3ctx.MkIntConst($"b{idx}")).ToList();

        // Each button must be pressed a non-negative number of times
        foreach (var buttonVar in buttonVars)
        {
            optimizer.Add(z3ctx.MkGe(buttonVar, z3ctx.MkInt(0)));
        }

        // Add constrains for each target joltage
        // For exmple Sum(b1,b3,b5) eq targetJoltage[0] (if buttons 1,3,5 affect joltage 0)
        for (var joltageIndex = 0; joltageIndex < targetJoltages.Count; joltageIndex++)
        {
            var targetJoltage = targetJoltages[joltageIndex];
            var buttonsVarsAffectingJoltage = buttons
                .Select((btn, buttonIndex) => (btn, buttonIndex))
                .Where(pair => pair.btn.Contains(joltageIndex))
                .Select(pair => buttonVars[pair.buttonIndex])
                .ToList();

            if (buttonsVarsAffectingJoltage.Count == 0)
            {
                // No buttons affect this joltage, so the target must be zero
                if (targetJoltage != 0)
                {
                    throw new Exception($"Joltage requirement #{joltageIndex} is > 0, but there are no buttons incrementing it.");
                }
            }
            else if (buttonsVarsAffectingJoltage.Count == 1)
            {
                // Only one button affects this joltage, so we can set it directly
                var btnVar = buttonsVarsAffectingJoltage[0];
                optimizer.Add(z3ctx.MkEq(btnVar, z3ctx.MkInt(targetJoltage)));
            }
            else
            {
                // Sum of all related button presses must equal target joltage
                var sumExpr = z3ctx.MkAdd(buttonsVarsAffectingJoltage);
                optimizer.Add(z3ctx.MkEq(sumExpr, z3ctx.MkInt(targetJoltage)));
            }
        }

        // Mimimize total number of button presses
        var minimizeTarget = z3ctx.MkAdd(buttonVars);
        optimizer.MkMinimize(minimizeTarget);

        // Solve the system and evaluate all buttonVars
        var result = optimizer.Check();
        if (result != Status.SATISFIABLE)
        {
            throw new Exception($"Could not solve machine {machineIndex}, result: {result}");
        }
        var model = optimizer.Model;
        var totalPresses = buttonVars
            .Select((btnVar) => ((IntNum)model.Eval(btnVar, true)).Int64)
            .Sum();
        return totalPresses;
    }
}
