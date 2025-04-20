using Artokai.AOC.Core;
using Artokai.AOC.Core.Combinatorics;

namespace Artokai.AOC.Puzzles.Y2015.D21;

[PuzzleInfo(year: 2015, day: 21, part: 2, title: "RPG Simulator 20XX")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var parsedInpput = Input.AsLines().Select(line =>
        {
            var parts = line.Split(':', 2);
            return new KeyValuePair<string, int>(parts[0].Trim(), int.Parse(parts[1].Trim()));
        }).ToDictionary();

        var hitPoints = 100;
        var bossHitPoints = parsedInpput["Hit Points"];
        var bossDamage = parsedInpput["Damage"];
        var bossArmor = parsedInpput["Armor"];

        var maxCost = int.MinValue;
        foreach (var weapon in Store.Weapons.GetCombinations(1))
        {
            var armors = Store.Armor.GetCombinations(0).Concat(Store.Armor.GetCombinations(1));
            foreach (var armor in armors)
            {
                var rings = Store.Rings.GetCombinations(0).Concat(Store.Rings.GetCombinations(1)).Concat(Store.Rings.GetCombinations(2));
                foreach (var ring in rings)
                {
                    var items = weapon.Concat(armor).Concat(ring);
                    var cost = items.Sum(i => i.Cost);
                    var damageValue = items.Sum(i => i.Damage);
                    var armorValue = items.Sum(i => i.Armor);

                    var turnsToWin = Math.Ceiling(bossHitPoints / Math.Max(1.0, damageValue - bossArmor));
                    var turnsToLose = Math.Ceiling(hitPoints / Math.Max(1.0, bossDamage - armorValue));
                    var isWinning = turnsToWin <= turnsToLose;

                    if (!isWinning)
                    {
                        maxCost = Math.Max(maxCost, cost);
                    }
                }
            }
        }
        return maxCost.ToString();
    }
}
