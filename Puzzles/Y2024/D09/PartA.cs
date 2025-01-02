using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D09;

[PuzzleInfo(year: 2024, day: 9, part: 1, title: "Disk Fragmenter")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var disk = ParseInput(Input.AsSingleLine());

        var head = 0;
        var tail = disk.Length - 1;
        while (head < tail)
        {
            while (disk[head] >= 0 && head < disk.Length)
            {
                head++;
            }
            while (disk[tail] < 0 && tail >= 0)
            {
                tail--;
            }

            if (head < tail)
            {
                var tmp = disk[head];
                disk[head] = disk[tail];
                disk[tail] = tmp;
            }
        }

        var checksum = 0L;
        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] >= 0)
            {
                checksum += i * disk[i];
            }
        }

        return checksum.ToString();
    }

    private int[] ParseInput(string input)
    {
        long total = 0;
        var sizes = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            var len = int.Parse(input[i].ToString());
            sizes[i] = len;
            total += len;
        }

        var disk = new int[total];
        var id = 0;
        var blockIndex = 0;
        var isFile = true;
        for (var i = 0; i < sizes.Length; i++)
        {
            if (isFile)
            {
                for (var j = 0; j < sizes[i]; j++)
                {
                    disk[blockIndex++] = id;
                }
                id++;
            }
            else
            {
                for (var j = 0; j < sizes[i]; j++)
                {
                    disk[blockIndex++] = -1;
                }
            }
            isFile = !isFile;
        }

        return disk;
    }

    private void PrintDisk(int[] disk)
    {
        for (var i = 0; i < disk.Length; i++)
        {
            var block = disk[i];
            if (block < 0)
            {
                Console.Write(".");
            }
            else
            {
                Console.Write(block);
            }
        }
        Console.WriteLine();
    }
}
