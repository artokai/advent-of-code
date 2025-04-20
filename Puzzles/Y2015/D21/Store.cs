namespace Artokai.AOC.Puzzles.Y2015.D21;

public record struct Item(string Name, int Cost, int Damage, int Armor);

public static class Store
{
    public static List<Item> Weapons = new List<Item> {
        new Item("Dagger", 8, 4, 0),
        new Item("Shortsword", 10, 5, 0),
        new Item("Warhammer", 25, 6, 0),
        new Item("Longsword", 40, 7, 0),
        new Item("Greataxe", 74, 8, 0),
    };

    public static List<Item> Armor = new List<Item> {
        new Item("Leather", 13, 0, 1),
        new Item("Chainmail", 31, 0, 2),
        new Item("Splintmail", 53, 0, 3),
        new Item("Bandedmail", 75, 0, 4),
        new Item("Platemail", 102, 0, 5),
    };

    public static List<Item> Rings = new List<Item> {
        new Item("Damage +1 ", 25, 1, 0),
        new Item("Damage +2 ", 50, 2, 0),
        new Item("Damage +3 ", 100, 3, 0),
        new Item("Defense +1 ", 20, 0, 1),
        new Item("Defense +2 ", 40, 0, 2),
        new Item("Defense +3 ", 80, 0, 3),
    };
}
