# Timed Torches Stay Lit

## - By TastyChickenLegs
 
- Turn on your torches using a configured timer and keep them lit
- Server Sync with ability to turn off version check
- Can put on the client or the server or both
- More configuration options and easier to use timer settings



### About the Mod
I wanted my torches to turn on at night and off during the day.  I also did not want to fuel them.

### Configuration


<b>If you leave the settings alone, torches will turn off at 7:30am and turn on at 4:30pm and require NO fuel.</b>

<b>To Configure if the server locks the config and forces users to have the same settings</b>

- ServerConfig Locked - locks the config files so all clients have the same config and cannot change. Default true

The config file is located in "<GameDirectory>\Bepinex\config" (You need to start the game at least once, with the mod installed to create the config file).

<b>To view or add items this mod affects:  </b>

- AffectedFireplaceSources - List of objects to be affected by the mod (see below for list of supported objects). By default vanilla torches are added.

<b>To configure the timer:</b>
 
- OnTime - Time of day when torches should turn ON. (The default is set to 4.30pm in-game time)
- OffTime - Time of day when torches should turn OFF. (The default is set to 6.30am in-game time)

- AlwaysOnInDarkBiomes - (Default True) If true, objects listed in AffectedFireplaceSources will always burn in areas that Valheim considers 'always dark'. E.g Mistlands or any biome during a storm.

- AllowAddingFuel - (Default False) If true, fuel can be added and sources will use the timer settings.  If false no fuel is needed to burn torches
 

Note: If OnTime and OffTime is set to the same value, for example 0 and 0 the fireplaces listed in AffectedFireplaceSources will burn 24/7.

The config file is located in "<GameDirectory>\Bepinex\config" (You need to start the game with the mod installed for the config file to be created).

### List of supported objects:
  
|Config Option|Definition
|---|---|
|fe_piece_walltorch_timer | Allow timer fuel for Sconce
|fe_piece_groundtorch_timer | Allow timer for Standing iron torch
|fe_piece_groundtorch_wood_timer | Allow timer for Standing wood torch
|fe_piece_groundtorch_green_timer |Allow timer for Standing green-burning iron torch
|fe_piece_groundtorch_blue_timer | Allow timer for Standing blue-burning iron torch
|fe_piece_brazierfloor01_timer | Allow timer for Standing brazier
|fe_piece_brazierceiling01_timer | Allow timer for Hanging brazier
|fe_piece_jackoturnip_timer | Allow timer for Jack-o-turnip
|fe_custom_instance_timer | Enable Timers for items added by other mods
|keepOnInRain | Keep fires lit even when raining and wet
|configOnTime Hours and Mins  | Convert desired time to military time (24hr) and /24.  Use the new slider for super simple config
|configOffTime Hours and Mins | Convert desired time to military time (24hr) and /24.  Use the new slider for super sipmle config
|configAlwaysOnInDarkBiomes | Always On In Dark Biomes or storming

- Custom modded items can be added as well.  Example rk_campfire for the smokeless campfire in the Bon Appetit mod.

### Other

New timer settings. Choose the Hour and Minutes with sliders and the mod does the rest.
No need to figure out game seconds or gametime.


### Installation (manual)  
Extract DLL from zip file into "<GameDirectory>\Bepinex\plugins"  
Start the game.

### Version Information

Version 1.2.0

- Completely rewritten 
- New Configuration options
- New easier to use timer settings
- ability to add custom items

Version 1.1.0

- ServerSync and versioncheck added.
- Fixed Interact bug when not using fuel
- Fix incompatibility with my NoSmokeStayLit mod

Version 1.0.0

- Intial release of mod - complete rewrite of TimedTorches_Fixed.
- Original Timed Torches mod was by Gurrlin.  Listed here for credit as this was originally his idea.

