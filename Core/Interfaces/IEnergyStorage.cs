namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// A device that can both receive and provide energy (battery).
    /// </summary>
    public interface IEnergyStorage
    {
        long StoredEnergy { get; }
        long MaxEnergyStorage { get; }
        VoltageTier Tier { get; }

        /// <summary>Accept energy for storage. Returns amount actually accepted.</summary>
        long ReceiveEnergy(long amount);

        /// <summary>Provide energy from storage. Returns amount actually provided.</summary>
        long ExtractEnergy(long amount);
    }
}
