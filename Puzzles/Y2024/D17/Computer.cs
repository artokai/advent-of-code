namespace Artokai.AOC.Puzzles.Y2024.D17;

public class Computer
{
    private int MAXJUMPS = 1000;
    private int jumpCounter = 0;
    private List<long> outs = [];
    private long a = 0;
    private long b = 0;
    private long c = 0;

    public string Run(long initialA)
    {
        outs = [];
        jumpCounter = 0;
        a = initialA;
        b = 0;
        c = 0;

        // Manually converted input to C# code
        // Program: 2,4,1,5,7,5,0,3,4,0,1,6,5,5,3,0

        L0: BST(4);               // b = 3 lowest bits of a
        L1: BXL(5);               // b = b xor 0b101 (b remains at most 0b111)
        L2: CDV(5);               // c = a / pow(2,b) (b is at most 7)
        L3: ADV(3);               // a = a / pow(2,3) => a = a / 8 (strip away 3 lowest bits)
        L4: BXC(0);               // b = b xor c
        L5: BXL(6);               // b = b xor 0b110
        L6: OUT(5);               // output 3 lowest bits of b 
        L7: var line = JNZ(0);    // Always jump to L0
        switch (line)
        {
            case 0:
                goto L0;
            case 1:
                goto L1;
            case 2:
                goto L2;
            case 3:
                goto L3;
            case 4:
                goto L4;
            case 5:
                goto L5;
            case 6:
                goto L6;
            case 7:
                goto L7;
        }

        return string.Join(",", outs);
    }

    long pow(long x) {
        return (long)Math.Pow(2, x);
    }

    long GetCombo(long combo) {
        return combo switch {
            >= 0 and <= 3 => combo,
            4 => a,
            5 => b,
            6 => c,
            _ => -1
        };
    }

    // 0
    void ADV(long combo)
    {
        var value = GetCombo(combo);
        a = a / pow(value);
    }

    // 1
    void BXL(long literal)
    {
        b = b ^ literal;
    }

    // 2
    void BST(long combo)
    {
        var value = GetCombo(combo);
        b = value % 8;
    }

    // 3
    long JNZ(long literal)
    {
        jumpCounter++;
        if (jumpCounter > MAXJUMPS) throw new Exception("Jump limit reached");
        return (a == 0) ? -1 : literal;
    }

    // 4
    void BXC(long literal)
    {
        b = b ^ c;
    }

    // 5
    void OUT(long combo)
    {
        var value = GetCombo(combo) % 8;
        outs.Add(value);
    }

    // 6
    void BDV(long combo)
    {
        var value = GetCombo(combo);
        b = a / pow(value);
    }

    // 7
    void CDV(long combo)
    {
        var value = GetCombo(combo);
        c = a / pow(value);
    }
}
