using TerraIndustry.Core.Interfaces;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Immutable definition describing a machine type's static properties.
    /// One instance per machine type, shared by all placed instances of that type.
    /// </summary>
    public class MachineDefinition
    {
        /// <summary>Unique machine type identifier (e.g. "coal_generator").</summary>
        public string MachineType { get; }

        /// <summary>Human-readable display name.</summary>
        public string DisplayName { get; }

        /// <summary>Maximum internal energy capacity (EU).</summary>
        public long MaxEnergyCapacity { get; }

        /// <summary>Energy consumed per tick while processing (EU/t). 0 for generators.</summary>
        public long EnergyConsumptionPerTick { get; }

        /// <summary>Energy produced per tick (EU/t). 0 for non-generators.</summary>
        public long EnergyProductionPerTick { get; }

        /// <summary>Number of input item slots.</summary>
        public int InputSlotCount { get; }

        /// <summary>Number of output item slots.</summary>
        public int OutputSlotCount { get; }

        /// <summary>Base processing speed in ticks.</summary>
        public int BaseProcessingTime { get; }

        /// <summary>Whether this machine is an energy producer.</summary>
        public bool IsGenerator { get; }

        /// <summary>Whether this machine supports fluids.</summary>
        public bool SupportsFluid { get; }

        /// <summary>The voltage tier this machine operates at.</summary>
        public VoltageTier Tier { get; }

        /// <summary>Tile width in tiles.</summary>
        public int TileWidth { get; }

        /// <summary>Tile height in tiles.</summary>
        public int TileHeight { get; }

        public MachineDefinition(
            string machineType,
            string displayName,
            long maxEnergyCapacity,
            long energyConsumptionPerTick,
            long energyProductionPerTick,
            int inputSlotCount,
            int outputSlotCount,
            int baseProcessingTime,
            bool isGenerator,
            bool supportsFluid,
            VoltageTier tier,
            int tileWidth = 3,
            int tileHeight = 3)
        {
            MachineType = machineType;
            DisplayName = displayName;
            MaxEnergyCapacity = maxEnergyCapacity;
            EnergyConsumptionPerTick = energyConsumptionPerTick;
            EnergyProductionPerTick = energyProductionPerTick;
            InputSlotCount = inputSlotCount;
            OutputSlotCount = outputSlotCount;
            BaseProcessingTime = baseProcessingTime;
            IsGenerator = isGenerator;
            SupportsFluid = supportsFluid;
            Tier = tier;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }
    }
}
