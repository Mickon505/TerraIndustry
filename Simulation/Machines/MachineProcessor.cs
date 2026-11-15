using TerraIndustry.Core.Interfaces;
using TerraIndustry.Core.Utilities;
using TerraIndustry.Recipes;

namespace TerraIndustry.Simulation.Machines
{
    /// <summary>
    /// Stateless processor that advances machine logic each tick.
    /// Handles recipe matching, input/output validation, energy consumption,
    /// and processing progress for all non-generator machines.
    /// </summary>
    public class MachineProcessor
    {
        private readonly RecipeRegistry _recipeRegistry;

        public MachineProcessor(RecipeRegistry recipeRegistry)
        {
            _recipeRegistry = recipeRegistry;
        }

        /// <summary>
        /// Process one simulation tick for a consuming machine (not a generator).
        /// </summary>
        public void ProcessTick(MachineInstance machine)
        {
            if (machine.Definition.IsGenerator)
                return; // Generators use GeneratorProcessor

            switch (machine.OperationalState)
            {
                case MachineOperationalState.Idle:
                case MachineOperationalState.WaitingForInput:
                case MachineOperationalState.WaitingForEnergy:
                case MachineOperationalState.WaitingForOutput:
                    TryStartRecipe(machine);
                    break;

                case MachineOperationalState.Working:
                    ContinueProcessing(machine);
                    break;

                case MachineOperationalState.Disabled:
                    break;
            }
        }

        private void TryStartRecipe(MachineInstance machine)
        {
            // Find a matching recipe
            MachineRecipe recipe = _recipeRegistry.FindMatchingRecipe(
                machine.Definition.MachineType, machine.Inventory);

            if (recipe == null)
            {
                machine.OperationalState = MachineOperationalState.WaitingForInput;
                return;
            }

            // Check output capacity for all outputs
            foreach (var output in recipe.Outputs)
            {
                if (!machine.Inventory.CanAcceptOutput(output.ItemType, output.Amount))
                {
                    machine.OperationalState = MachineOperationalState.WaitingForOutput;
                    return;
                }
            }

            // Check energy
            long energyNeeded = recipe.EnergyPerTick;
            if (machine.StoredEnergy < energyNeeded)
            {
                machine.OperationalState = MachineOperationalState.WaitingForEnergy;
                return;
            }

            // Consume inputs
            foreach (var input in recipe.Inputs)
            {
                if (!machine.Inventory.ConsumeInput(input.ItemType, input.Amount))
                {
                    IndustrialLogger.Error(
                        $"Failed to consume input {input.ItemType} x{input.Amount} for recipe {recipe.RecipeId}");
                    machine.OperationalState = MachineOperationalState.Error;
                    return;
                }
            }

            // Start processing
            machine.CurrentRecipeId = recipe.RecipeId;
            machine.ProcessingTimeRequired = recipe.Duration;
            machine.ProcessingProgress = 0;
            machine.OperationalState = MachineOperationalState.Working;

            // Consume first tick of energy
            machine.StoredEnergy -= energyNeeded;
        }

        private void ContinueProcessing(MachineInstance machine)
        {
            MachineRecipe recipe = _recipeRegistry.GetRecipe(machine.CurrentRecipeId);
            if (recipe == null)
            {
                IndustrialLogger.Error($"Recipe {machine.CurrentRecipeId} not found during processing");
                machine.OperationalState = MachineOperationalState.Error;
                return;
            }

            // Check energy
            if (machine.StoredEnergy < recipe.EnergyPerTick)
            {
                machine.OperationalState = MachineOperationalState.WaitingForEnergy;
                return;
            }

            // Consume energy
            machine.StoredEnergy -= recipe.EnergyPerTick;

            // Advance progress
            machine.ProcessingProgress++;

            // Check if complete
            if (machine.ProcessingProgress >= machine.ProcessingTimeRequired)
            {
                CompleteRecipe(machine, recipe);
            }
        }

        private void CompleteRecipe(MachineInstance machine, MachineRecipe recipe)
        {
            // Validate output space one more time
            foreach (var output in recipe.Outputs)
            {
                if (!machine.Inventory.CanAcceptOutput(output.ItemType, output.Amount))
                {
                    // Output full — wait (don't lose progress)
                    machine.OperationalState = MachineOperationalState.WaitingForOutput;
                    machine.ProcessingProgress = machine.ProcessingTimeRequired; // keep at max
                    return;
                }
            }

            // Insert outputs
            foreach (var output in recipe.Outputs)
            {
                machine.Inventory.InsertOutput(output.ItemType, output.Amount);
            }

            // Reset processing state
            machine.CurrentRecipeId = null;
            machine.ProcessingProgress = 0;
            machine.ProcessingTimeRequired = 0;
            machine.OperationalState = MachineOperationalState.Idle;
        }
    }
}
