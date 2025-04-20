using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D04;

[PuzzleInfo(year: 2016, day: 4, part: 2, title: "Security Through Obscurity")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines()
            .Select(Room.Parse)
            .Where(room => room.Checksum == room.CalculateChecksum())
            .Select(room => (room.SectorId, Decrypted: DecryptName(room)))
            .Where(tuple => tuple.Decrypted.Contains("northpole"))
            .Where(tuple => tuple.Decrypted.Contains("object"))
            .Select(tuple => tuple.SectorId.ToString())
            .First();
    }

    public string DecryptName(Room room)
    {
        var decryptedChars = room.EncryptedName.Select(c =>
            c switch
            {
                '-' => ' ',
                _ => (char)('a' + (c - 'a' + room.SectorId) % 26)
            }
        ).ToArray();
        return new string(decryptedChars);
    }
}
