using System.Collections.Generic;
using TerraIndustry.Core.Constants;
using TerraIndustry.Core.Interfaces;
using TerraIndustry.Core.Utilities;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Central registry for all machine definitions.
    /// Populated at mod load time.
    /// </summary>
    public class MachineRegistry
    {
        private readonly Dictionary<string, MachineDefinition> _definitions = new();

        public void Register(MachineDefinition definition)
        {
            if (_definitions.ContainsKey(definition.MachineType))
            {
                IndustrialLogger.Warn(
                    $"Machine definition '{definition.MachineType}' is being overwritten");
            }
            _definitions[definition.MachineType] = definition;
            IndustrialLogger.Info($"Registered machine: {definition.MachineType}");
        }

        public MachineDefinition Get(string machineType)
        {
            _definitions.TryGetValue(machineType, out var def);
            return def;
        }

        public bool Exists(string machineType) => _definitions.ContainsKey(machineType);

        public IEnumerable<MachineDefinition> GetAll() => _definitions.Values;

        /// <summary>
        /// Register all built-in machine definitions.
        /// </summary>
        public void RegisterDefaults()
        {
            Register(new MachineDefinition(
                machineType: MachineConstants.CoalGenerator,
                displayName: "Coal Generator",
                maxEnergyCapacity: EnergyConstants.DefaultGeneratorEnergyCapacity,
                energyConsumptionPerTick: 0,
                energyProductionPerTick: EnergyConstants.CoalGeneratorOutput,
                inputSlotCount: 1,
                outputSlotCount: 0,
                baseProcessingTime: 0,
                isGenerator: true,
                supportsFluid: false,
                tier: VoltageTier.LowVoltage
            ));

            Register(new MachineDefinition(
                machineType: MachineConstants.ElectricFurnace,
                displayName: "Electric Furnace",
                maxEnergyCapacity: EnergyConstants.DefaultMachineEnergyCapacity,
                energyConsumptionPerTick: 20,
                energyProductionPerTick: 0,
                inputSlotCount: 1,
                outputSlotCount: 1,
                baseProcessingTime: MachineConstants.DefaultProcessingTime,
                isGenerator: false,
                supportsFluid: false,
                tier: VoltageTier.LowVoltage
            ));
        }
    }
}
