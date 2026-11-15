using TerraIndustry.Core.Interfaces;

namespace TerraIndustry.Core.Constants
{
    /// <summary>
    /// Constants for the energy system.
    /// </summary>
    public static class EnergyConstants
    {
        // Base transfer rates per voltage tier (EU/t)
        public const long LowVoltageTransfer = 32;
        public const long MediumVoltageTransfer = 128;
        public const long HighVoltageTransfer = 512;
        public const long ExtremeVoltageTransfer = 2048;
        public const long QuantumVoltageTransfer = 8192;

        // Default capacities
        public const long DefaultMachineEnergyCapacity = 10000;
        public const long DefaultGeneratorEnergyCapacity = 20000;
        public const long BasicBatteryCapacity = 100000;
        public const long IndustrialBatteryCapacity = 1000000;

        // Coal generator
        public const long CoalGeneratorOutput = 20; // EU/t
        public const int CoalBurnTimeInTicks = 1600; // ~26.7 seconds at 60fps

        /// <summary>
        /// Get the maximum transfer rate for a voltage tier.
        /// </summary>
        public static long GetTransferRate(VoltageTier tier)
        {
            return tier switch
            {
                VoltageTier.LowVoltage => LowVoltageTransfer,
                VoltageTier.MediumVoltage => MediumVoltageTransfer,
                VoltageTier.HighVoltage => HighVoltageTransfer,
                VoltageTier.ExtremeVoltage => ExtremeVoltageTransfer,
                VoltageTier.QuantumVoltage => QuantumVoltageTransfer,
                _ => LowVoltageTransfer
            };
        }
    }
}
