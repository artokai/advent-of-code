using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Core;

public abstract class SolverBase
{
    public string InputPath { get; set; } = "input.txt";

    private PuzzleInput? _input = null;

    public PuzzleInput Input
    {
        get
        {
            if (_input == null || _input.InputPath != InputPath)
            {
                _input = new PuzzleInput(InputPath);
            }
            return _input;
        }
    }

    public abstract string Solve();
}
