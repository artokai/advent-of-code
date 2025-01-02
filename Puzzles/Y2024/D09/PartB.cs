using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D09;

[PuzzleInfo(year: 2024, day: 9, part: 2, title: "Disk Fragmenter")]
public class PartB : SolverBase
{
    private class FileEntry
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public int Size { get; set; }
    }

    public override string Solve()
    {
        var files = ParseInput(Input.AsSingleLine());

        var toProcess = new Stack<FileEntry>();
        foreach (var file in files.Skip(1))
        {
            toProcess.Push(file);
        }

        while (toProcess.Count > 0)
        {
            var file = toProcess.Pop();
            for (var j = 0; j < files.Count - 1; j++)
            {
                var a = files[j];
                var b = files[j + 1];
                var spaceIndex = a.Index + a.Size;
                if (spaceIndex >= file.Index)
                {
                    break;
                }
                var spaceBetween = b.Index - (a.Index + a.Size);
                if (spaceBetween >= file.Size)
                {
                    file.Index = a.Index + a.Size;
                    files.Remove(file);
                    files.Insert(j + 1, file);
                    break;
                }
            }
        }

        return GetChecksum(files).ToString();
    }

    private long GetChecksum(IEnumerable<FileEntry> files)
    {
        long result = 0;
        foreach (var file in files)
        {
            for (var i = 0; i < file.Size; i++)
            {
                result += (file.Index + i) * file.Id;
            }
        }
        return result;
    }

    private List<FileEntry> ParseInput(string input)
    {
        var isFile = true;
        var diskIndex = 0;
        var fileId = 0;
        var files = new List<FileEntry>();
        for (var i = 0; i < input.Length; i++)
        {
            var len = int.Parse(input[i].ToString());
            if (isFile)
            {
                files.Add(new FileEntry { Index = diskIndex, Id = fileId, Size = len });
                fileId++;
            }
            diskIndex += len;
            isFile = !isFile;
        }

        return files;
    }

    private void PrintDisk(IEnumerable<FileEntry> files)
    {
        var sorted = files.OrderBy(f => f.Index);
        var idx = 0;
        foreach (var file in sorted)
        {
            while (idx < file.Index)
            {
                Console.Write(".");
                idx++;
            }
            for (var i = 0; i < file.Size; i++)
            {
                Console.Write(file.Id);
                idx++;
            }
        }
        Console.WriteLine();
    }
}
