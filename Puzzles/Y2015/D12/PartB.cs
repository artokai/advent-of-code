using System.Text.Json;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D12;

[PuzzleInfo(year: 2015, day: 12, part: 2, title: "JSAbacusFramework.io")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var json = JsonDocument.Parse(input);
        return GetSum(json.RootElement).ToString();
    }

    private long GetSum(JsonElement element) =>
        element.ValueKind switch
        {
            JsonValueKind.Array => GetSumArray(element),
            JsonValueKind.Object => GetSumObject(element),
            JsonValueKind.Number => element.GetInt64(),
            _ => 0L,
        };

    private long GetSumArray(JsonElement element) =>
        element.EnumerateArray().Sum(GetSum);

    private long GetSumObject(JsonElement element) =>
        element.EnumerateObject().Any(IsRedProperty)
            ? 0L
            : element.EnumerateObject().Select(p => p.Value).Sum(GetSum);

    private bool IsRedProperty(JsonProperty property) =>
        property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red";
}
