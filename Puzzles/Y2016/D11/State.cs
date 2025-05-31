using System.Numerics;

namespace Artokai.AOC.Puzzles.Y2016.D11;

public record State(int elevatorPos, List<Floor> floors, int steps, State? previousState)
{
    public bool IsTargetState() => floors
        .Select((floor, index) => index == floors.Count - 1 || floor.IsEmpty())
        .All(isEmpty => isEmpty);

    public List<State> GetNextStates(int bitCount)
    {
        var currentFloor = floors[elevatorPos];

        // Convert our bitmasks to list of items on the current floor
        // It will be easier to choose the items to move this way
        var itemsOnFloor = new List<(int bitPos, bool isMicrochip)>();
        for (var i = 0; i < bitCount; i++)
        {
            var bitPos = 1 << i;
            if ((currentFloor.Microchips & (1 << i)) != 0)
                itemsOnFloor.Add((i, true));

            if ((currentFloor.Generators & (1 << i)) != 0)
                itemsOnFloor.Add((i, false));
        }

        // Get list of potential items or item-pairs to move
        var itemsToMove = new List<List<(int bitPos, bool isMicrochip)>>();
        for (var i = 0; i < itemsOnFloor.Count; i++)
        {
            // Move a single item
            itemsToMove.Add(new List<(int bitPos, bool isMicrochip)> { itemsOnFloor[i] });

            // Move two items
            for (var j = i + 1; j < itemsOnFloor.Count; j++)
            {
                itemsToMove.Add(new List<(int bitPos, bool isMicrochip)>
                {
                    itemsOnFloor[i],
                    itemsOnFloor[j]
                });
            }
        }

        // Get next possible elevator positions
        var newElevatorPositions = new List<int>();
        if (elevatorPos > 0)
        {
            // Optimization: only move down if there are items below
            var hasItemsBelow = floors
                .Where((f, i) => i < elevatorPos)
                .Any(floor => !floor.IsEmpty());
            if (hasItemsBelow)
            {
                newElevatorPositions.Add(elevatorPos - 1);
            }
        }

        if (elevatorPos < floors.Count - 1)
            newElevatorPositions.Add(elevatorPos + 1);


        var nextStates = new List<State>();
        foreach (var newElevatorPos in newElevatorPositions)
        {
            foreach (var items in itemsToMove)
            {
                // Convert our chosen items back to bitmasks
                var chipBitsToMove = items
                    .Where(item => item.isMicrochip)
                    .Aggregate(0u, (acc, item) => acc | (uint)1 << item.bitPos);
                var generatorBitsToMove = items
                    .Where(item => !item.isMicrochip)
                    .Aggregate(0u, (acc, item) => acc | (uint)1 << item.bitPos);

                // Check if target floor will be safe after moving the items
                var targetFloor = floors[newElevatorPos];
                var newTargetFloorChips = targetFloor.Microchips | chipBitsToMove;
                var newTargetFloorGenerators = targetFloor.Generators | generatorBitsToMove;
                var newTargetFloor = new Floor(newTargetFloorChips, newTargetFloorGenerators);
                if (!newTargetFloor.IsSafe())
                    continue;

                // Check if current floor will be safe after moving the items
                var newCurrentFloorChips = currentFloor.Microchips & ~chipBitsToMove;
                var newCurrentFloorGenerators = currentFloor.Generators & ~generatorBitsToMove;
                var newCurrentFloor = new Floor(newCurrentFloorChips, newCurrentFloorGenerators);
                if (!newCurrentFloor.IsSafe())
                    continue;

                // Create a new state to be returned
                var updatedFloors = floors.Select((floor, i) =>
                {
                    if (i == elevatorPos)
                        return newCurrentFloor;
                    if (i == newElevatorPos)
                        return newTargetFloor;
                    return floor;
                }).ToList();
                nextStates.Add(new State(newElevatorPos, updatedFloors, steps + 1, this));
            }
        }
        return nextStates;
    }

    public StateKey GetStateKey(int _bitCount)
    {
        // All states in our queue are safe and we don't care about the exact chip/generator distribution,
        // so we can use a simplified state key based on the number of chips and generators per floor.
        // This is because we don't really care which exact chips/generators are on each floor.
        //
        // For example in part B it does not matter at all if we choose to  move
        // elerium chip + controller to second floor or chooose the dilithium pair instead
        // The minimum step count will we be the in the end.

        var chipData = 0u;
        var generatorData = 0u;

        for (var lvl = 0; lvl < floors.Count; lvl++)
        {
            var floor = floors[lvl];
            int chipsCount = BitOperations.PopCount(floor.Microchips);
            int generatorCount = BitOperations.PopCount(floor.Generators);

            chipData |= (uint)chipsCount << (lvl * 8);
            generatorData |= (uint)generatorCount << (lvl * 8);
        }
        return new StateKey(elevatorPos, chipData, generatorData);
    }

    public StateKey GetStateKeyExact(int bitCount)
    {
        // This is an unused StateKey generation that will consider the exact chip/generator distribution.
        // Using it will work, but it will generate a very large search space and wuill be very slow.

        var chipData = 0u;
        var generatorData = 0u;

        for (var i = 0; i < floors.Count; i++)
        {
            chipData |= floors[i].Microchips << (i * bitCount);
            generatorData |= floors[i].Generators << (i * bitCount);
        }
        return new StateKey(elevatorPos, chipData, generatorData);
    }
}

public record struct StateKey(int ElevatorPos, uint ChipData, uint GeneratorData);
