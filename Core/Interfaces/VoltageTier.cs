namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// Voltage tiers for the energy system. Higher tiers support greater throughput.
    /// </summary>
    public enum VoltageTier
    {
        /// <summary>32 EU/t max cable transfer</summary>
        LowVoltage = 1,

        /// <summary>128 EU/t max cable transfer</summary>
        MediumVoltage = 2,

        /// <summary>512 EU/t max cable transfer</summary>
        HighVoltage = 3,

        /// <summary>2048 EU/t max cable transfer</summary>
        ExtremeVoltage = 4,

        /// <summary>8192 EU/t max cable transfer</summary>
        QuantumVoltage = 5
    }
}
