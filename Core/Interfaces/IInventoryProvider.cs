using Terraria;

namespace TerraIndustry.Core.Interfaces
{
    /// <summary>
    /// Provides access to item inventory slots for a machine.
    /// </summary>
    public interface IInventoryProvider
    {
        /// <summary>Total number of item slots.</summary>
        int SlotCount { get; }

        /// <summary>Get the item in the given slot.</summary>
        Item GetItem(int slot);

        /// <summary>Set the item in the given slot.</summary>
        void SetItem(int slot, Item item);

        /// <summary>Whether a slot is an input slot.</summary>
        bool IsInputSlot(int slot);

        /// <summary>Whether a slot is an output slot.</summary>
        bool IsOutputSlot(int slot);
    }
}
