using Microsoft.Xna.Framework;

namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// Base interface for all industrial machines in the simulation layer.
    /// </summary>
    public interface IIndustrialMachine
    {
        /// <summary>Unique persistent identity for this machine instance.</summary>
        int MachineId { get; }

        /// <summary>The definition that describes this machine's static properties.</summary>
        string MachineType { get; }

        /// <summary>World position (tile coordinates).</summary>
        Point Position { get; }

        /// <summary>Current operational state.</summary>
        MachineOperationalState OperationalState { get; }

        /// <summary>Called every simulation tick to advance machine logic.</summary>
        void Update();
    }

    public enum MachineOperationalState
    {
        Idle,
        Working,
        WaitingForInput,
        WaitingForOutput,
        WaitingForEnergy,
        Disabled,
        Error
    }
}
