namespace Artokai.AOC.Puzzles.Y2016.D11;

public record Floor(uint Microchips, uint Generators)
{
    public bool IsSafe()
    {
        var exposedChips = Microchips & ~Generators;
        return Generators == 0 || exposedChips == 0;
    }

    public bool IsEmpty()
    {
        return Microchips == 0 && Generators == 0;
    }

    public override string ToString()
    {
        var MAX_ELEMENT_COUNT = 8;
        var chips = Convert.ToString(Microchips, 2).PadLeft(MAX_ELEMENT_COUNT, '0');
        var gens = Convert.ToString(Generators, 2).PadLeft(MAX_ELEMENT_COUNT, '0');
        return $"chips: {chips}, generators:  {gens}";

    }
}
