using TerraIndustry.Core.Constants;
using TerraIndustry.Core.Interfaces;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Stateless processor that handles generator logic: burn fuel, produce energy.
    /// </summary>
    public class GeneratorProcessor
    {
        /// <summary>
        /// Process one simulation tick for a generator.
        /// </summary>
        public void ProcessTick(MachineInstance machine)
        {
            if (!machine.Definition.IsGenerator)
                return;

            // If currently burning fuel, produce energy
            if (machine.FuelBurnTimeRemaining > 0)
            {
                machine.FuelBurnTimeRemaining--;

                long produced = machine.Definition.EnergyProductionPerTick;
                long space = machine.Definition.MaxEnergyCapacity - machine.StoredEnergy;
                long accepted = produced < space ? produced : space;
                machine.StoredEnergy += accepted;

                machine.OperationalState = MachineOperationalState.Working;
                return;
            }

            // Try to consume new fuel
            if (TryConsumeFuel(machine))
            {
                machine.OperationalState = MachineOperationalState.Working;
            }
            else
            {
                machine.OperationalState = machine.StoredEnergy > 0
                    ? MachineOperationalState.Idle
                    : MachineOperationalState.WaitingForInput;
            }
        }

        private bool TryConsumeFuel(MachineInstance machine)
        {
            // Check each input slot for burnable fuel
            for (int i = 0; i < machine.Inventory.InputSlotCount; i++)
            {
                var item = machine.Inventory.GetItem(i);
                if (!item.IsAir)
                {
                    int burnTime = GetBurnTime(item.type);
                    if (burnTime > 0)
                    {
                        item.stack--;
                        if (item.stack <= 0)
                            item.TurnToAir();

                        machine.FuelBurnTimeRemaining = burnTime;
                        machine.FuelBurnTimeTotal = burnTime;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get the burn time in ticks for a fuel item type.
        /// Will be replaced by a fuel registry in later milestones.
        /// </summary>
        public static int GetBurnTime(int itemType)
        {
            // Coal
            if (itemType == Terraria.ItemID.Coal)
                return EnergyConstants.CoalBurnTimeInTicks;

            // Wood
            if (itemType == Terraria.ItemID.Wood)
                return 400; // ~6.7 seconds

            // Gel (slime)
            if (itemType == Terraria.ItemID.Gel)
                return 200; // ~3.3 seconds

            return 0;
        }
    }
}
