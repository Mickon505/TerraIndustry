INDUSTRIAL TERRARIA
Project Blueprint, Technical Architecture & AI Development Specification

Project type: Terraria tModLoader content overhaul
Language: C#
Framework: tModLoader
Architecture: Modular monolith with independent industrial simulation engine
Project scope: Large-scale industrial and technological overhaul
Primary inspirations: Factorio, Mekanism, Immersive Engineering, Industrial Foregoing, Create, Extreme Reactors

PART 1 — PROJECT VISION
1.1 Concept

Industrial Terraria is a large-scale Terraria mod focused on industrial automation, technological progression, resource extraction, energy production, chemical processing and space exploration.

The objective is to introduce a complete industrial gameplay layer while preserving Terraria's fundamental gameplay experience.

Players should progress from basic material processing and primitive generators to complex industrial production facilities, advanced power generation, automated resource extraction and extraterrestrial mining expeditions.

The mod should introduce new gameplay systems that integrate with existing Terraria progression rather than simply adding isolated machines and items.

1.2 Design pillars
Industrial automation: Machines, processing chains, inventories and automation.
Energy engineering: Generators, electricity networks, cables, batteries and advanced reactors.
Resource extraction: Geological scanners, virtual deposits and industrial drilling.
Chemical processing: Fluids, pipes, pumps, tanks and chemical reactions.
Technological progression: Research, unlocks, advanced components and industrial infrastructure.
World integration: New ores, structures, enemies, bosses, weapons, tools and modified vanilla recipes.
Space exploration: Rocket construction, planetary missions and extraterrestrial resources.
1.3 Design philosophy

The industrial systems should be interconnected.

Example production chain:

Coal → Coal Generator → Electricity → Crusher → Ore Dust → Electric Furnace → Metal Bars → Assembler → Machine Components.

Advanced production chains should require multiple machines, different materials, electricity and potentially fluids.

Machines should create meaningful infrastructure requirements rather than functioning as independent crafting stations.

PART 2 — TECHNOLOGY STACK
Technology	Purpose
C#	Main programming language
tModLoader	Terraria modding framework
.NET SDK	Build environment
Visual Studio / Rider	Development environment
Git	Version control
JSON	External content definitions
HJSON	Localization and configuration
Aseprite	Pixel art and animations

Use the current stable tModLoader release and its officially supported .NET environment.

Avoid unnecessary third-party dependencies.

Use existing tModLoader functionality whenever practical.

PART 3 — PROJECT ARCHITECTURE
3.1 Architectural philosophy

Use a modular monolith with a clean separation between:

Industrial Simulation Layer

Contains gameplay-independent industrial logic:

Machine processing.
Electricity.
Fluids.
Inventories.
Recipe processing.
Resource extraction.
Research.
Space missions.
Network management.

Terraria Integration Layer

Contains Terraria-specific implementation:

ModTile.
ModTileEntity.
ModItem.
ModNPC.
ModPlayer.
ModSystem.
UserInterface.
World generation.
Rendering.
Multiplayer integration.
World persistence.

The industrial simulation layer should not directly depend on Terraria rendering, UI or world APIs.

Terraria integration should communicate with the simulation through clearly defined interfaces and adapters.

3.2 Proposed directory structure
text
IndustrialTerraria/
│
├── IndustrialTerraria.cs
├── build.txt
├── description.txt
│
├── Core/
│   ├── Bootstrap/
│   ├── Configuration/
│   ├── Constants/
│   ├── Events/
│   ├── Interfaces/
│   └── Utilities/
│
├── Simulation/
│   ├── Energy/
│   ├── Fluids/
│   ├── Machines/
│   ├── Networks/
│   ├── Logistics/
│   └── Scheduling/
│
├── Content/
│   ├── Items/
│   ├── Materials/
│   ├── Ores/
│   ├── Machines/
│   ├── Generators/
│   ├── Cables/
│   ├── Tools/
│   ├── Weapons/
│   ├── Armor/
│   ├── NPCs/
│   ├── Bosses/
│   └── Structures/
│
├── Recipes/
│   ├── MachineRecipes/
│   ├── VanillaRecipes/
│   └── RecipeConditions/
│
├── Research/
│   ├── Technologies/
│   ├── ResearchManager.cs
│   └── ResearchTree.cs
│
├── Mining/
│   ├── Deposits/
│   ├── Scanners/
│   └── Extraction/
│
├── Space/
│   ├── Planets/
│   ├── Missions/
│   ├── Rockets/
│   ├── Resources/
│   └── MissionControl.cs
│
├── World/
│   ├── Generation/
│   ├── Structures/
│   └── WorldProgression/
│
├── UI/
│   ├── Components/
│   ├── Machines/
│   ├── Research/
│   ├── PowerGrid/
│   ├── PlanetaryMap/
│   └── MissionControl/
│
├── Networking/
│   ├── Packets/
│   └── Synchronization/
│
├── Assets/
└── Localization/


Initially, use one tModLoader mod project.

Do not split the architecture into multiple mods or assemblies unless there is a clear technical reason.

PART 4 — CORE INDUSTRIAL SYSTEMS
4.1 Machine framework

All machines should use a common framework.

Separate:

MachineDefinition.
MachineInstance.
MachineProcessor.
MachineDefinition

Describes the machine's static properties:

Machine ID.
Maximum energy capacity.
Energy consumption.
Inventory size.
Processing speed.
Supported recipe types.
Fluid support.
Upgrade support.
Machine tier.
MachineInstance

Stores the state of an individual placed machine:

Persistent identity.
World position.
Inventory.
Stored energy.
Processing progress.
Current recipe.
Operational state.
Upgrade configuration.
MachineProcessor

Responsible for:

Finding matching recipes.
Checking input requirements.
Checking output capacity.
Checking energy requirements.
Consuming inputs.
Advancing processing.
Producing outputs.
Updating machine state.

Do not duplicate machine processing logic across individual machine implementations.

Machine interfaces

Introduce reusable capabilities:

IIndustrialMachine.
IEnergyConsumer.
IEnergyProducer.
IEnergyStorage.
IFluidHandler.
IInventoryProvider.

Machines should only implement the capabilities they support.

Initial machine catalogue

Material processing

Crusher.
Enrichment Chamber.
Electric Furnace.
Alloy Smelter.
Compressor.
Centrifuge.
Industrial Assembler.

Chemical processing

Chemical Reactor.
Chemical Infuser.
Electrolytic Separator.
Chemical Washer.
Refinery.

Resource extraction

Geological Scanner.
Industrial Drill.
Deep Mining Drill.
Oil Pump.
Advanced Resource Extractor.

Advanced technology

Research Station.
Quantum Assembler.
Advanced Fabricator.
Rocket Assembly Station.
PART 5 — ENERGY SYSTEM
5.1 General requirements

Create an independent electricity simulation system.

The system must support:

Electricity generation.
Electricity consumption.
Energy storage.
Cable networks.
Transfer limits.
Machine power requirements.
Network discovery.
Network merging.
Network splitting.
Power priorities.
Configurable machine connections.
Power monitoring.
5.2 Architecture
text
EnergySystem
├── EnergyNetworkManager
├── EnergyNetwork
├── EnergyNode
├── EnergyConnection
├── EnergyCable
├── EnergyProducer
├── EnergyConsumer
├── EnergyStorage
└── EnergyDistributor


Use graph-based logical networks.

Rebuild networks only when topology changes.

Do not perform expensive world-wide network searches every game tick.

5.3 Energy units

Use EU/t as the internal representation.

Use long for internal energy quantities where appropriate.

Separate:

Energy capacity.
Stored energy.
Energy generation per tick.
Energy consumption per tick.
Cable transfer capacity.
5.4 Voltage tiers

Support voltage tiers as an infrastructure and progression concept.

Suggested tiers:

Tier	Name
1	Low Voltage
2	Medium Voltage
3	High Voltage
4	Extreme Voltage
5	Quantum Voltage

Voltage tiers should affect machine compatibility, cable capacity and transformers.

Avoid simulating electrical physics at individual cable-segment level.

5.5 Cables

Initial cable types:

Copper Cable.
Gold Cable.
Advanced Cable.
Superconducting Cable.
Quantum Cable.

Machine connections should support configurable sides.

Use logical network distribution with physical cable transfer limits.

5.6 Generators

Progression:

Coal Generator.
Steam Generator.
Diesel Generator.
Solar Generator.
Wind Generator.
Nuclear Reactor.
Advanced Nuclear Reactor.
Fusion Reactor.

Generators should use fuel inventories and appropriate production mechanics.

Advanced generators should introduce additional systems such as cooling, waste handling and temperature management where appropriate.

5.7 Energy storage
Basic Battery.
Industrial Battery.
Advanced Energy Storage.
Quantum Energy Storage.
PART 6 — FLUID SYSTEM

Create an independent fluid simulation system.

Architecture:

text
FluidSystem
├── FluidRegistry
├── FluidNetworkManager
├── FluidNetwork
├── FluidPipe
├── FluidTank
├── FluidPump
├── FluidDistributor
└── FluidTransferRules


Support:

Fluid identification.
Fluid storage.
Input and output tanks.
Pipe networks.
Pumps.
Transfer limits.
Fluid compatibility.
Machine fluid requirements.

Initial fluids:

Water.
Steam.
Crude Oil.
Heavy Oil.
Diesel.
Hydrogen.
Oxygen.
Sulfuric Acid.
Liquid Nitrogen.

Start with one fluid per tank.

Do not implement complex thermodynamics or fluid mixing in the initial release.

PART 7 — RECIPE SYSTEM

Create a centralized RecipeRegistry.

Recipes should be independent of machine implementation.

A machine recipe should support:

Recipe ID.
Required machine type.
Item inputs.
Item outputs.
Fluid inputs.
Fluid outputs.
Energy consumption.
Processing duration.
Required technology.
Additional conditions.

Example:

json
{
  "id": "iron_enrichment",
  "machine": "enrichment_chamber",
  "inputs": [
    {
      "item": "iron_ore",
      "amount": 2
    }
  ],
  "outputs": [
    {
      "item": "iron_dust",
      "amount": 3
    }
  ],
  "energyPerTick": 40,
  "duration": 200,
  "technology": "basic_metallurgy"
}


Initially implement recipes in C# if that provides faster development.

Move stable definitions to JSON once the schema is established.

Use registered content identifiers rather than hardcoded Terraria numeric item IDs.

Ensure output capacity is validated before inputs are consumed.

PART 8 — RESOURCE EXTRACTION
8.1 Resource categories

Three resource categories:

Naturally generated Terraria ores.
Machine-extracted underground resources.
Extraterrestrial resources.
8.2 Virtual resource deposits

Machine-extracted resources should primarily use virtual deposits rather than dynamically generating thousands of ore tiles.

A deposit should contain:

Unique identifier.
World position.
Resource ID.
Remaining quantity.
Depth.
Richness.
Extraction compatibility.
8.3 Geological scanners

Scanner progression:

Basic Geological Scanner.
Advanced Geological Scanner.
Deep Geological Scanner.
Quantum Geological Scanner.

Scanners should identify deposits within their operating radius.

8.4 Drilling machines
Basic Industrial Drill.
Advanced Industrial Drill.
Deep Mining Drill.
Quantum Drill.

Drills should require electricity, compatible deposits and appropriate mining technology.

Deposits can be finite or renewable depending on their resource definition.

PART 9 — RESEARCH SYSTEM

Implement a world-shared technology progression system.

Use a directed acyclic graph (DAG) to represent research dependencies.

Research should unlock:

Machines.
Recipes.
Materials.
Generators.
Mining technology.
Rocket components.
Advanced infrastructure.
9.1 Research data

Introduce physical research items:

Basic Research Data.
Mechanical Research Data.
Chemical Research Data.
Electrical Research Data.
Advanced Research Data.
Space Research Data.

Research Stations consume research materials and produce technology progress.

9.2 Example progression
text
Basic Engineering
       |
       v
Basic Metallurgy
       |
       +---------> Advanced Alloys
       |
       v
Industrial Mining
       |
       +---------> Advanced Machinery
                       |
                       v
                 Rocket Engineering
                       |
                       v
                 Space Exploration


Research should be stored in world data.

PART 10 — SPACE EXPLORATION
10.1 Design

Use mission-based planetary exploration.

Do not initially create separate Terraria worlds for planets.

Players construct rockets, provide fuel and equipment, select a destination and launch expeditions.

Missions progress in the background while the player continues playing.

10.2 Architecture
text
SpaceSystem
│
├── PlanetRegistry
│   └── PlanetDefinition
│
├── RocketSystem
│   ├── RocketDefinition
│   ├── RocketInstance
│   └── RocketBuilder
│
├── MissionSystem
│   ├── MissionDefinition
│   ├── MissionInstance
│   ├── MissionManager
│   └── MissionResolver
│
└── SpaceInventory

10.3 Planet definitions

Each planet should define:

Planet ID.
Display name.
Difficulty.
Minimum rocket tier.
Available resources.
Available mission events.
Mission duration.
Resource distribution.
Unlock conditions.

Initial destinations:

Planet	Difficulty	Resources
Luna	Low	Helium-3, Lunar Iron
Ares	Medium	Titanium, Rare Earth Elements
Vulcan	High	Tungsten, Sulfur
Cryos	Extreme	Cryogenic Materials, Exotic Crystals
10.4 Mission lifecycle
text
Created
   |
Preparing
   |
ReadyToLaunch
   |
InTransit
   |
Exploring
   |
Returning
   |
Completed


Additional states:

Failed.
Aborted.
WaitingForRecovery.

Use world ticks for mission timing.

Store enough information to restore missions correctly after saving and reloading.

Use deterministic random seeds for reproducible mission outcomes.

10.5 Rocket components
Rocket Engine.
Fuel Tank.
Guidance System.
Cargo Module.
Mining Module.
Scanner Module.
Defense Module.
Research Module.

Rocket upgrades should affect mission efficiency and capabilities.

PART 11 — TERRARIA INTEGRATION
11.1 New content

Introduce:

New naturally generated ores.
Machine-exclusive resources.
New materials and alloys.
Industrial tools.
Industrial weapons.
Armor sets.
Hostile robots.
Industrial structures.
Bosses.
Unique drops.
11.2 Structures

Potential structures:

Abandoned Factory.
Underground Research Facility.
Nuclear Facility.
Oil Extraction Site.
Ancient Laboratory.
Alien Outpost.

Structures should contain machines, loot, enemies and progression materials.

11.3 Bosses

Potential bosses:

Scrap Golem.
Excavator.
Overseer.
Planetary Core.

Bosses should unlock technological progression and new industrial capabilities.

11.4 Vanilla recipe integration

Use supported tModLoader recipe hooks.

Modify selected vanilla recipes to introduce industrial processing and advanced materials.

Introduce configuration options:

Default industrial progression.
Reduced vanilla recipe changes.
Full industrial overhaul.

Avoid unnecessarily breaking compatibility with other content mods.

PART 12 — UI ARCHITECTURE

Create a reusable industrial UI framework.

text
IndustrialUI
│
├── Components
│   ├── EnergyBar
│   ├── ProgressBar
│   ├── ItemSlot
│   ├── FluidTankDisplay
│   ├── MachineStatus
│   └── UpgradeSlot
│
├── Screens
│   ├── MachineUI
│   ├── GeneratorUI
│   ├── ChemicalPlantUI
│   ├── ResearchUI
│   ├── PowerNetworkUI
│   ├── RocketAssemblyUI
│   └── MissionControlUI
│
└── Themes
    └── IndustrialTheme


Use Terraria's UI framework.

All UI components should be reusable.

UI should display simulation state and request actions, never own the underlying machine inventory or simulation state.

PART 13 — MULTIPLAYER AND NETWORKING

The server must be authoritative.

Clients request interactions.

The server validates and executes them.

Clients receive synchronized state.

Requirements:

Server-side machine processing.
Server-side energy distribution.
Server-side inventory operations.
Server-side resource extraction.
Server-side research progression.
Server-side mission progression.
Validated machine interactions.
Efficient state synchronization.

Do not synchronize every machine's complete state every tick.

Use incremental synchronization where practical.

Use ModTileEntity networking and Terraria's supported packet mechanisms.

Test multiplayer early rather than treating it as a final development phase.

PART 14 — PERSISTENCE

Use appropriate tModLoader persistence mechanisms.

Data	Storage
Machine inventories and state	ModTileEntity
Research	ModSystem world data
Resource deposits	ModSystem world data
Planetary missions	ModSystem world data
Player upgrades	ModPlayer

Save:

Machine inventories.
Stored energy.
Machine processing progress.
Resource deposits.
Research progress.
Planetary mission states.
Rocket configurations.

Do not serialize runtime network graphs.

Rebuild networks after loading a world.

Introduce save schema versioning and migration support from the beginning.

PART 15 — PERFORMANCE

The mod must support large industrial installations.

Requirements:

Scheduled machine updates.
Cached network connections.
Dirty flags for network topology.
Incremental synchronization.
Efficient inventory operations.
Controlled UI refresh.
Efficient resource deposit storage.
No unnecessary per-tick allocations.
No full-world scans every tick.
No expensive graph reconstruction every tick.

Create performance tests using simulated factories containing hundreds of machines.

PART 16 — DEVELOPMENT ROADMAP
Release 0.1 — Industrial Core

Implement:

Project structure.
Core interfaces.
Machine definitions.
Machine instances.
Machine inventories.
Recipe registry.
Machine processing.
Coal Generator.
Electric Furnace.

Acceptance criteria:

Machines can be placed and destroyed.
Machine inventories work.
Recipes process correctly.
Machine state persists.
The first production chain works.
Release 0.2 — Energy Networks

Implement:

Cable tiles.
Energy network manager.
Network discovery.
Network merging and splitting.
Generators.
Batteries.
Energy distribution.
Machine power consumption.
Power UI.

Acceptance criteria:

Machines receive energy through cables.
Cable limits are respected.
Network topology updates correctly.
Energy is synchronized in multiplayer.
Release 0.3 — Industrial Production

Implement:

Crusher.
Enrichment Chamber.
Alloy Smelter.
Chemical Reactor.
Fluid tanks.
Fluid pipes.
Fluid network management.
Advanced processing recipes.

Acceptance criteria:

Multi-stage recipes work.
Fluid requirements work.
Output capacity is respected.
Machine state persists.
Release 0.4 — Resource Extraction and Research

Implement:

Geological Scanner.
Virtual resource deposits.
Industrial Drill.
Advanced mining.
Resource depletion.
Research Station.
Technology tree.

Acceptance criteria:

Deposits can be discovered.
Drills extract resources.
Extraction requires electricity.
Research unlocks technologies.
Release 0.5 — Terraria Integration

Implement:

New ores.
World generation.
Structures.
Hostile enemies.
Bosses.
Weapons.
Tools.
Armor.
Mob drops.
Vanilla recipe changes.

Acceptance criteria:

Industrial progression integrates with Terraria progression.
Structures generate correctly.
Bosses unlock technology.
Vanilla recipes work correctly.
Release 0.6 — Space Exploration

Implement:

Rocket Assembly Station.
Rocket components.
Fuel production.
Planet registry.
Mission control.
Mission lifecycle.
Mission simulation.
Planetary resources.
Space research.

Acceptance criteria:

Rockets can be assembled.
Missions can be launched.
Missions progress while playing.
Missions survive saving and reloading.
Rewards are correctly delivered.
PART 17 — TESTING STRATEGY

Create automated tests for the independent simulation systems.

Test:

Energy generation and consumption.
Cable transfer limits.
Network splitting.
Network merging.
Machine processing.
Inventory capacity.
Fluid transfer.
Resource extraction.
Research prerequisites.
Mission lifecycle.
Save/load consistency.

Important edge cases:

Cable destroyed during operation.
Generator runs out of fuel.
Machine loses power halfway through processing.
Machine output inventory becomes full.
Two networks become connected.
A mission is in transit during a world save.
Machine destroyed during processing.
Player disconnects during machine interaction.

Create automated simulation benchmarks independent of Terraria rendering.

PART 18 — IMPLEMENTATION PRIORITIES

Build a complete vertical slice before adding large quantities of content.

The first playable factory should contain:

Coal Generator.
Copper Cable.
Crusher.
Electric Furnace.
Iron Ore.
Iron Dust.
Iron Bars.

Production chain:

text
Coal
 |
 v
Coal Generator
 |
 v
Electricity
 |
 v
Copper Cable Network
 |
 v
Crusher
 |
 v
Iron Dust
 |
 v
Electric Furnace
 |
 v
Iron Bars


Do not proceed to advanced industrial content until this system is stable.

PART 19 — MASTER AI DEVELOPMENT PROMPT

The following prompt is intended for an AI coding agent with access to the project workspace, file creation and editing capabilities, terminal access and the ability to build and test the mod.

MASTER PROMPT

You are a senior C# software architect and experienced Terraria tModLoader mod developer.

Your task is to develop a large-scale Terraria industrial technology overhaul mod named IndustrialTerraria.

You are responsible for implementing the mod according to the project specification below.

1. Project objectives

Develop a complete industrial simulation and technological progression system inside Terraria.

The mod is inspired by:

Factorio.
Mekanism.
Immersive Engineering.
Industrial Foregoing.
Create.
Extreme Reactors.

The main gameplay features include:

Industrial machines.
Electricity networks.
Generators.
Energy storage.
Fluid networks.
Chemical processing.
Machine recipes.
Resource extraction.
Geological scanners.
Virtual resource deposits.
Research progression.
New ores and materials.
Structures.
Bosses.
Weapons and tools.
Modified vanilla recipes.
Rocket construction.
Planetary missions.
Extraterrestrial resources.
2. Architectural requirements

Use a modular monolith architecture.

Separate the industrial simulation engine from Terraria-specific integration.

The simulation engine must not directly depend on Terraria rendering or UI.

Use well-defined interfaces and reusable domain models.

Avoid unnecessary abstractions and premature optimization.

Prefer maintainable, testable and understandable code.

Follow SOLID principles where appropriate.

Use dependency injection only where it provides a genuine architectural benefit.

Do not introduce unnecessary external dependencies.

3. Development environment

Before writing code:

Inspect the existing project workspace.
Determine whether a tModLoader project already exists.
Inspect the installed tModLoader version and supported .NET environment.
Verify available APIs against the installed version.
Inspect existing code and preserve valid implementations.
Create a project structure following the architecture specification.

Do not assume API methods exist without verification.

Prefer official tModLoader documentation and installed ExampleMod source code.

Never invent tModLoader APIs.

If a requested implementation is incompatible with the installed version, explain the limitation and implement the closest supported alternative.

4. Core simulation systems

Implement the following systems as independent modules:

Machine System

Machine definitions.
Machine instances.
Machine inventories.
Machine processors.
Processing states.
Recipe matching.
Processing progress.
Output capacity validation.

Energy System

Energy networks.
Network nodes.
Cable connections.
Network discovery.
Network merging and splitting.
Energy producers.
Energy consumers.
Energy storage.
Energy distribution.
Transfer limits.
Voltage tiers.
Configurable machine connection sides.

Use EU/t internally.

Use logical graph-based networks with physical cable transfer limits.

Do not rebuild networks every tick.

Fluid System

Fluid definitions.
Fluid stacks.
Tanks.
Pipes.
Pumps.
Fluid networks.
Transfer limits.
Input and output handling.

Start with one fluid per tank.

Recipe System

Central recipe registry.
Machine requirements.
Item inputs and outputs.
Fluid inputs and outputs.
Energy requirements.
Processing durations.
Technology requirements.

Validate all inputs and outputs before consuming resources.

Mining System

Geological scanners.
Virtual resource deposits.
Deposit discovery.
Industrial drills.
Resource extraction.
Deposit depletion.

Research System

Technology definitions.
Research dependencies.
Research DAG.
Research data.
Unlock management.
World-shared progression.

Space System

Planet definitions.
Rocket definitions.
Rocket assembly.
Mission definitions.
Mission lifecycle.
Mission simulation.
Planetary resource rewards.
Deterministic mission resolution.
5. Terraria integration

Use appropriate tModLoader classes:

ModTile.
ModTileEntity.
ModItem.
ModNPC.
ModPlayer.
ModSystem.
ModConfig.
Terraria UI framework.

Use ModTileEntity as the connection between placed machines and the simulation engine.

Keep simulation state independent of visual representation.

Use supported world generation hooks for new ores and structures.

Use supported recipe modification hooks for vanilla recipe changes.

6. Multiplayer

Design all industrial simulation logic to be server-authoritative.

Clients must not independently modify authoritative machine inventories, energy networks, resource deposits or missions.

Validate all incoming interaction requests.

Use efficient state synchronization.

Avoid sending complete machine state every tick.

Ensure machines and networks behave consistently in multiplayer.

7. Persistence

Implement persistent storage for:

Machine state.
Machine inventories.
Stored energy.
Processing progress.
Resource deposits.
Research progression.
Rocket configurations.
Planetary missions.

Rebuild runtime network graphs after world loading.

Introduce save versioning and migration support.

8. User interfaces

Create reusable UI components.

Implement:

Machine UI.
Energy indicators.
Processing progress.
Fluid tank displays.
Research interface.
Power network monitor.
Rocket assembly interface.
Mission control interface.

UI components must not own authoritative gameplay state.

All state-changing actions must be validated by the server.

9. Coding standards

Follow these rules:

Use descriptive names.
Keep classes focused.
Avoid excessively large classes.
Avoid duplicated machine logic.
Avoid magic numbers.
Use strongly typed identifiers where appropriate.
Use immutable definitions where practical.
Validate input data.
Handle invalid states safely.
Use structured logging for important failures.
Avoid unnecessary per-tick allocations.
Document complex algorithms.
Write unit tests for simulation logic.

Do not use placeholder implementations that pretend to provide working functionality.

If a feature cannot yet be implemented, clearly identify it as unfinished.

10. Development workflow

Do not attempt to implement the entire mod in one operation.

Work incrementally through these milestones:

Milestone 1: Project Foundation

Set up the tModLoader project.
Create the directory structure.
Create core interfaces.
Implement machine definitions.
Implement machine instances.
Implement machine inventory.
Implement recipe registry.

Build and verify.

Milestone 2: First Machines

Implement ModTile.
Implement ModTileEntity.
Implement Coal Generator.
Implement Electric Furnace.
Implement machine UI.
Implement basic recipe processing.

Build and test.

Milestone 3: Energy Networks

Implement cables.
Implement graph-based networks.
Implement network discovery.
Implement energy distribution.
Implement batteries.
Implement network synchronization.

Build and test.

Milestone 4: Industrial Processing

Implement Crusher.
Implement Enrichment Chamber.
Implement Alloy Smelter.
Implement fluid system.
Implement Chemical Reactor.

Build and test.

Milestone 5: Mining and Research

Implement virtual deposits.
Implement Geological Scanner.
Implement Industrial Drill.
Implement research data.
Implement technology progression.

Build and test.

Milestone 6: Terraria Content

Implement ores.
Implement structures.
Implement enemies.
Implement bosses.
Implement weapons.
Implement tools.
Implement recipe modifications.

Build and test.

Milestone 7: Space Exploration

Implement rockets.
Implement planets.
Implement missions.
Implement mission simulation.
Implement planetary resources.
Implement mission control UI.

Build and test.

11. Verification requirements

After each milestone:

Compile the mod.
Resolve compilation errors.
Run available automated tests.
Verify tModLoader API compatibility.
Inspect multiplayer implications.
Verify save/load behavior where relevant.
Report completed features.
Report unfinished features.
Identify technical risks.
Explain the next milestone.

Never claim that a feature works without implementing and testing it.

Do not silently remove existing functionality.

Do not rewrite unrelated parts of the project.

Keep the project buildable after every milestone.

12. Immediate task

Start with Milestone 1: Project Foundation.

First inspect the development environment and the existing workspace.

Then create the project skeleton and implement the foundational machine, inventory and recipe abstractions.

Do not start implementing planetary missions, bosses or large quantities of content yet.

The immediate objective is a clean, extensible foundation that can support the entire industrial simulation.

Before finishing, provide:

Created files.
Architecture decisions.
Implemented functionality.
Build results.
Test results.
Known limitations.
Next recommended implementation step.

You must prioritize working, maintainable code over generating large amounts of incomplete code.

END OF PROJECT SPECIFICATION

Project name: IndustrialTerraria

First development target: Industrial Core (Release 0.1)

Primary objective: Build a reliable, extensible industrial simulation foundation before expanding into advanced content.
