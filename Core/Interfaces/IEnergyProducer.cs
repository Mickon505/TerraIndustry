namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// A device that generates energy for the network.
    /// </summary>
    public interface IEnergyProducer
    {
        /// <summary>Energy produced per tick (EU/t) at current operating conditions.</summary>
        long EnergyOutput { get; }

        /// <summary>Maximum energy output per tick (EU/t).</summary>
        long MaxEnergyOutput { get; }

        /// <summary>Extract energy from this producer. Returns amount actually extracted.</summary>
        long ExtractEnergy(long amount);

        /// <summary>The voltage tier this producer operates at.</summary>
        VoltageTier Tier { get; }
    }
}
