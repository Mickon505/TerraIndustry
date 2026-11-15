namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// A device that can store or process fluids.
    /// </summary>
    public interface IFluidHandler
    {
        /// <summary>Number of fluid tanks this handler has.</summary>
        int TankCount { get; }

        /// <summary>Get the fluid stack in the given tank slot.</summary>
        FluidStack GetFluid(int tank);

        /// <summary>Get the capacity of the given tank slot in mB.</summary>
        int GetTankCapacity(int tank);

        /// <summary>Try to insert fluid. Returns amount actually inserted.</summary>
        int InsertFluid(int tank, FluidStack fluid);

        /// <summary>Try to extract fluid. Returns the extracted stack.</summary>
        FluidStack ExtractFluid(int tank, int maxAmount);

        /// <summary>Whether the given tank accepts the specified fluid type.</summary>
        bool IsFluidValid(int tank, string fluidId);
    }

    /// <summary>
    /// Represents a quantity of a specific fluid.
    /// </summary>
    public struct FluidStack
    {
        public static readonly FluidStack Empty = new FluidStack(string.Empty, 0);

        public string FluidId { get; }
        public int Amount { get; }

        public bool IsEmpty => string.IsNullOrEmpty(FluidId) || Amount <= 0;

        public FluidStack(string fluidId, int amount)
        {
            FluidId = fluidId;
            Amount = amount;
        }

        public FluidStack WithAmount(int newAmount) => new FluidStack(FluidId, newAmount);
    }
}
