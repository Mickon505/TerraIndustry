using Microsoft.Xna.Framework;
using TerraIndustry.Core.Interfaces;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Mutable state for a single placed machine in the world.
    /// Each placed machine tile entity owns one MachineInstance.
    /// </summary>
    public class MachineInstance
    {
        /// <summary>Unique persistent identity for this placed machine.</summary>
        public int MachineId { get; set; }

        /// <summary>Reference to the immutable definition.</summary>
        public MachineDefinition Definition { get; }

        /// <summary>World position in tile coordinates.</summary>
        public Point Position { get; set; }

        /// <summary>Item inventory.</summary>
        public MachineInventory Inventory { get; }

        /// <summary>Current stored energy (EU).</summary>
        public long StoredEnergy { get; set; }

        /// <summary>Processing progress in ticks (0 = not processing).</summary>
        public int ProcessingProgress { get; set; }

        /// <summary>Total ticks required for current recipe (0 = no recipe).</summary>
        public int ProcessingTimeRequired { get; set; }

        /// <summary>The recipe ID currently being processed, or null.</summary>
        public string CurrentRecipeId { get; set; }

        /// <summary>Current operational state.</summary>
        public MachineOperationalState OperationalState { get; set; }

        /// <summary>Remaining fuel burn time in ticks (generators only).</summary>
        public int FuelBurnTimeRemaining { get; set; }

        /// <summary>Total burn time of the current fuel item (generators only).</summary>
        public int FuelBurnTimeTotal { get; set; }

        /// <summary>Whether this machine is part of an energy network.</summary>
        public int NetworkId { get; set; } = -1;

        /// <summary>Processing progress as a fraction [0, 1].</summary>
        public float ProcessingFraction =>
            ProcessingTimeRequired > 0 ? (float)ProcessingProgress / ProcessingTimeRequired : 0f;

        /// <summary>Energy fill fraction [0, 1].</summary>
        public float EnergyFraction =>
            Definition.MaxEnergyCapacity > 0 ? (float)StoredEnergy / Definition.MaxEnergyCapacity : 0f;

        /// <summary>Fuel burn fraction [0, 1] (generators only).</summary>
        public float FuelFraction =>
            FuelBurnTimeTotal > 0 ? (float)FuelBurnTimeRemaining / FuelBurnTimeTotal : 0f;

        public MachineInstance(MachineDefinition definition, Point position)
        {
            Definition = definition;
            Position = position;
            Inventory = new MachineInventory(definition.InputSlotCount, definition.OutputSlotCount);
            StoredEnergy = 0;
            ProcessingProgress = 0;
            ProcessingTimeRequired = 0;
            CurrentRecipeId = null;
            OperationalState = MachineOperationalState.Idle;
            FuelBurnTimeRemaining = 0;
            FuelBurnTimeTotal = 0;
        }
    }
}
