using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D23;

[PuzzleInfo(year: 2016, day: 23, part: 2, title: "Safe Cracking")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var program = string.Join('\n', Input.AsLines());
        var optimized = Optimize(program);

        var emulator = new Emulator(optimized.Split('\n'));
        emulator.SetRegisterValue("a", 12);
        // emulator.PrintInstructions();
        emulator.Run();
        return emulator.GetRegisterValue("a").ToString();

    }

    private string Optimize(string program)
    {
        program = UseAddInstruction(program);
        program = UseMultiplyInstruction(program);
        return program;
    }

    private string UseAddInstruction(string program)
    {
        // Replace inc-loop with custom add command:
        //   inc a
        //   dec b
        //   jnz b -2
        //
        // Result
        //   nop
        //   add a b
        //   cpy 0 b

        var re = new Regex(@"inc (?<inc>\w+)\ndec (?<dec>\w+)\njnz (?<jnz>\w+) -2\n", RegexOptions.Multiline);
        var matches = re.Matches(program);
        foreach (Match match in matches)
        {
            var inc = match.Groups["inc"].Value;
            var dec = match.Groups["dec"].Value;
            var jnz = match.Groups["jnz"].Value;
            if (dec == jnz)
            {
                var replacement = $"\nnop\nadd {inc} {dec}\ncpy 0 {dec}\n";
                program = program.Replace(match.Value, replacement);
            }
        }
        return program;
    }

    private string UseMultiplyInstruction(string program)
    {
        // Replace multiplication (a = a + b * c) with custom mul command:
        //   add a b
        //   cpy 0 b
        //   dec c
        //   jnz c -5
        //
        // Result
        //   mul c b
        //   add m a
        //   cpy 0 b
        //   cpy 0 c

        var re = new Regex(@"add (?<addA>\w+) (?<addB>\w+)\ncpy 0 (?<cpy>\w+)\ndec (?<dec>\w+)\njnz (?<jnz>\w+) -5\n", RegexOptions.Multiline);
        var matches = re.Matches(program);
        foreach (Match match in matches)
        {
            var addA = match.Groups["addA"].Value;
            var addB = match.Groups["addB"].Value;
            var cpy = match.Groups["cpy"].Value;
            var dec = match.Groups["dec"].Value;
            var jnz = match.Groups["jnz"].Value;
            if (addB == cpy && dec == jnz)
            {
                var replacement = $"mul {dec} {addB}\nadd m {addA}\ncpy 0 {addB}\ncpy 0 {dec}\n";
                program = program.Replace(match.Value, replacement);
            }
        }
        return program;
    }

}
