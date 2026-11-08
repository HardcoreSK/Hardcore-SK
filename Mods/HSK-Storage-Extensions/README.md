# Storage Extensions
This module is a collection of extensions for interacting with Rimworld's (or third party mods) storage mechanics. While the scope is not limited to any specific aspect of storage, it is limited providing the features for another mod to utilize. As such, no defs or patches are contained within this package to ensure that it is not tightly coupled to any specific solution.

## Extensions
### StorageDefExtension
A simple extension to allow additional properties to be added to a building intended for storage. It is assumed that buildings using this extension will be storage buildings utilizing SlotGroups.  
Patches utilizing this extension only modify the HaulToCell job, as it is this job that handles SlotGroup storage tasks. Hauling to other types of storage such as ThingOwners and Containers is handled by  
different jobs, and thus will not be affected by this extension.  

**StowingProperties**
| Field                             | Description |
| --------                          | ------- |
| baseStowTicks                     | The base number of ticks it will take to stow an allowed item here.  |
| minimumStowTicks                  | The minimum number of ticks, after all other modifiers have been applied. |
| additionalTicksPerStoredStack     | Number of ticks added per already stored stack of any def. The stack-to-be-stowed is not included. |
| additionalTicksPerStoredDef       | Number of ticks added for each unique def already stored. The stack-to-be-stowed is not included. |
| quickToStowItems                  | A filter to define items that are easy to stow in this location. Applies even if other additions to the stowing time have been made. |
| quickStowDurationFactor           | The multiplier to use when and item matches the quickToStowItems filter. |
| slowToStowItems                   | A filter to define items that are difficult to stow in this location. |
| slowStowDurationFactor            | The multiplier to use when and item matches the slowToStowItems filter. |  

Note that due to the complexity of ThingFilter, no config validation is performed to ensure that there is no conflict between slowToStowItems and quickToStowItems. Having Meals be quick to stow while Packaged Survival Meal is slow to stow will cause both features to trigger.  
When the final stowing duration is 0, no change will occur to the base hauling job. When it is greater than 0, a wait toil and progress bar will be added for the final duration.

**RetrievalProperties**
No RetrievalProperties at this time.

## Harmony Patches
Patches are separate into files by their over-all descriptive type - Work, Research, UI, etc.  
Patches are descriptive and documented within the code itself.
