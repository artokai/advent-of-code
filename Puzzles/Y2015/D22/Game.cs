
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D22;

public record struct GameState(
    bool IsPlayerTurn,
    int PlayerHitPoints,
    int PlayerArmor,
    int Mana,
    int ManaSpent,
    int BossHitPoints,
    int BossDamage,
    int ShieldTurnsLeft,
    int PoisonTurnsLeft,
    int RechargeTurnsLeft
);

public class Game
{
    private const int MagicMissileCost = 53;
    private const int DrainCost = 73;
    private const int ShieldCost = 113;
    private const int PoisonCost = 173;
    private const int RechargeCost = 229;
    private readonly bool _isPartB;
    private readonly GameState _initialGameState;

    public Game(PuzzleInput input, bool isPartB = false)
    {
        var parsedInpput = input.AsLines().Select(line =>
        {
            var parts = line.Split(':', 2);
            return new KeyValuePair<string, int>(parts[0].Trim(), int.Parse(parts[1].Trim()));
        }).ToDictionary();

        _isPartB = isPartB;
        _initialGameState = new GameState(
            IsPlayerTurn: true,
            PlayerHitPoints: 50,
            PlayerArmor: 0,
            Mana: 500,
            ManaSpent: 0,
            BossHitPoints: parsedInpput["Hit Points"],
            BossDamage: parsedInpput["Damage"],
            ShieldTurnsLeft: 0,
            PoisonTurnsLeft: 0,
            RechargeTurnsLeft: 0
        );
    }

    public int Play()
    {
        return Play(_initialGameState);
    }

    private int Play(GameState state)
    {
        var afterEffectsState = ApplyEffects(state);
        if (afterEffectsState.BossHitPoints <= 0)
        {
            return afterEffectsState.ManaSpent;
        }

        if (afterEffectsState.PlayerHitPoints <= 0)
        {
            return int.MaxValue;
        }

        if (afterEffectsState.IsPlayerTurn)
        {
            return PlayPlayerTurn(afterEffectsState);
        }
        else
        {
            return PlayBossTurn(afterEffectsState);
        }
    }

    private GameState ApplyEffects(GameState state)
    {
        var playerArmor = 0;
        var playerHitPoints = state.PlayerHitPoints;
        var mana = state.Mana;
        var bossHitPoints = state.BossHitPoints;
        var shieldTurnsLeft = state.ShieldTurnsLeft;
        var poisonTurnsLeft = state.PoisonTurnsLeft;
        var rechargeTurnsLeft = state.RechargeTurnsLeft;

        // In PartB, the player loses one hit point at the start of their own turn
        if (_isPartB && state.IsPlayerTurn)
        {
            playerHitPoints -= 1;
        }

        if (shieldTurnsLeft > 0)
        {
            playerArmor = 7;
            shieldTurnsLeft--;
        }

        if (poisonTurnsLeft > 0)
        {
            bossHitPoints -= 3;
            poisonTurnsLeft--;
        }

        if (rechargeTurnsLeft > 0)
        {
            mana += 101;
            rechargeTurnsLeft--;
        }

        return state with
        {
            PlayerHitPoints = playerHitPoints,
            PlayerArmor = playerArmor,
            Mana = mana,
            BossHitPoints = bossHitPoints,
            ShieldTurnsLeft = shieldTurnsLeft,
            PoisonTurnsLeft = poisonTurnsLeft,
            RechargeTurnsLeft = rechargeTurnsLeft
        };
    }

    private int PlayPlayerTurn(GameState state)
    {
        var outcomes = new List<int>();

        // Magic Missile
        if (state.Mana >= MagicMissileCost)
        {
            var bossHitPoints = state.BossHitPoints - 4;
            if (bossHitPoints <= 0)
            {
                outcomes.Add(state.ManaSpent + MagicMissileCost);
            }
            else
            {
                outcomes.Add(Play(state with
                {
                    IsPlayerTurn = false,
                    BossHitPoints = bossHitPoints,
                    Mana = state.Mana - MagicMissileCost,
                    ManaSpent = state.ManaSpent + MagicMissileCost
                }));
            }
        }

        // Drain
        if (state.Mana >= DrainCost)
        {
            var bossHitPoints = state.BossHitPoints - 2;
            if (bossHitPoints <= 0)
            {
                outcomes.Add(state.ManaSpent + DrainCost);
            }
            else
            {
                outcomes.Add(Play(state with
                {
                    IsPlayerTurn = false,
                    BossHitPoints = bossHitPoints,
                    Mana = state.Mana - DrainCost,
                    ManaSpent = state.ManaSpent + DrainCost,
                    PlayerHitPoints = state.PlayerHitPoints + 2
                }));
            }
        }

        // Shield
        if (state.Mana >= ShieldCost && state.ShieldTurnsLeft <= 0)
        {
            outcomes.Add(Play(state with
            {
                IsPlayerTurn = false,
                Mana = state.Mana - ShieldCost,
                ManaSpent = state.ManaSpent + ShieldCost,
                ShieldTurnsLeft = 6,
            }));
        }

        // Poison
        if (state.Mana >= PoisonCost && state.PoisonTurnsLeft <= 0)
        {
            outcomes.Add(Play(state with
            {
                IsPlayerTurn = false,
                Mana = state.Mana - PoisonCost,
                ManaSpent = state.ManaSpent + PoisonCost,
                PoisonTurnsLeft = 6,
            }));
        }

        // Recharge
        if (state.Mana >= RechargeCost && state.RechargeTurnsLeft <= 0)
        {
            outcomes.Add(Play(state with
            {
                IsPlayerTurn = false,
                Mana = state.Mana - RechargeCost,
                ManaSpent = state.ManaSpent + RechargeCost,
                RechargeTurnsLeft = 5,
            }));
        }

        // If we can't do anything, we lose
        return (outcomes.Count <= 0) ? int.MaxValue : outcomes.Min();
    }

    private int PlayBossTurn(GameState state)
    {
        var totalDamage = Math.Max(1, state.BossDamage - state.PlayerArmor);
        var playerHitPoints = state.PlayerHitPoints - totalDamage;

        if (playerHitPoints <= 0)
        {
            return int.MaxValue;
        }
        else
        {
            return Play(state with
            {
                IsPlayerTurn = true,
                PlayerHitPoints = playerHitPoints
            });
        }
    }
}
