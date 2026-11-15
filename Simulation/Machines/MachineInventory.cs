using System;
using Terraria;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Manages item storage for a machine instance.
    /// Slots are divided into input slots (0..inputCount-1) and output slots (inputCount..total-1).
    /// </summary>
    public class MachineInventory
    {
        private readonly Item[] _slots;
        private readonly int _inputSlotCount;
        private readonly int _outputSlotCount;

        public int TotalSlotCount => _slots.Length;
        public int InputSlotCount => _inputSlotCount;
        public int OutputSlotCount => _outputSlotCount;

        public MachineInventory(int inputSlots, int outputSlots)
        {
            _inputSlotCount = inputSlots;
            _outputSlotCount = outputSlots;
            _slots = new Item[inputSlots + outputSlots];
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = new Item();
                _slots[i].TurnToAir();
            }
        }

        public Item GetItem(int slot)
        {
            ValidateSlot(slot);
            return _slots[slot];
        }

        public void SetItem(int slot, Item item)
        {
            ValidateSlot(slot);
            _slots[slot] = item ?? throw new ArgumentNullException(nameof(item));
        }

        public bool IsInputSlot(int slot) => slot >= 0 && slot < _inputSlotCount;
        public bool IsOutputSlot(int slot) => slot >= _inputSlotCount && slot < _slots.Length;

        /// <summary>
        /// Check if the given item can fit in any output slot (same type stacks or empty slot).
        /// </summary>
        public bool CanAcceptOutput(int itemType, int amount)
        {
            for (int i = _inputSlotCount; i < _slots.Length; i++)
            {
                Item slot = _slots[i];
                if (slot.IsAir)
                    return true;
                if (slot.type == itemType && slot.stack + amount <= slot.maxStack)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Try to insert an item into output slots. Returns the amount that could not be inserted.
        /// </summary>
        public int InsertOutput(int itemType, int amount)
        {
            int remaining = amount;
            for (int i = _inputSlotCount; i < _slots.Length && remaining > 0; i++)
            {
                Item slot = _slots[i];
                if (slot.IsAir)
                {
                    slot.SetDefaults(itemType);
                    int toInsert = Math.Min(remaining, slot.maxStack);
                    slot.stack = toInsert;
                    remaining -= toInsert;
                }
                else if (slot.type == itemType)
                {
                    int space = slot.maxStack - slot.stack;
                    int toInsert = Math.Min(remaining, space);
                    slot.stack += toInsert;
                    remaining -= toInsert;
                }
            }
            return remaining;
        }

        /// <summary>
        /// Consume a specific amount of a specific item from input slots.
        /// Returns true if the full amount was consumed.
        /// </summary>
        public bool ConsumeInput(int itemType, int amount)
        {
            // First verify we have enough
            int available = 0;
            for (int i = 0; i < _inputSlotCount; i++)
            {
                if (_slots[i].type == itemType)
                    available += _slots[i].stack;
            }
            if (available < amount)
                return false;

            // Consume
            int remaining = amount;
            for (int i = 0; i < _inputSlotCount && remaining > 0; i++)
            {
                if (_slots[i].type == itemType)
                {
                    int toConsume = Math.Min(remaining, _slots[i].stack);
                    _slots[i].stack -= toConsume;
                    remaining -= toConsume;
                    if (_slots[i].stack <= 0)
                        _slots[i].TurnToAir();
                }
            }
            return true;
        }

        /// <summary>
        /// Check whether inputs contain at least the given amount of the given item.
        /// </summary>
        public bool HasInput(int itemType, int amount)
        {
            int count = 0;
            for (int i = 0; i < _inputSlotCount; i++)
            {
                if (_slots[i].type == itemType)
                    count += _slots[i].stack;
            }
            return count >= amount;
        }

        private void ValidateSlot(int slot)
        {
            if (slot < 0 || slot >= _slots.Length)
                throw new ArgumentOutOfRangeException(nameof(slot),
                    $"Slot {slot} is out of range [0, {_slots.Length})");
        }
    }
}
