namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// A device that consumes energy from the network.
    /// </summary>
    public interface IEnergyConsumer
    {
        /// <summary>Energy demanded per tick (EU/t).</summary>
        long EnergyDemand { get; }

        /// <summary>Maximum energy this device can store internally.</summary>
        long MaxEnergyStorage { get; }

        /// <summary>Current stored energy.</summary>
        long StoredEnergy { get; }

        /// <summary>Receive energy from the network. Returns amount actually accepted.</summary>
        long ReceiveEnergy(long amount);

        /// <summary>The voltage tier this consumer operates at.</summary>
        VoltageTier Tier { get; }
    }
}
