## Introduction ##
Since there are 2 of us, we can pick and choose our individual areas to work on. Some areas will require both of us to plan and develop smart strategies. The overall idea is that, given GameLib\_01 and Engine\_01, anyone should be able to render a WinForms hosted XNA game.

### Items ###
  * Content loading
  * Time synced events (EngineClock)
  * RandomGenerator
  * Database
  * Player objects and classes
  * Images
  * Debugging & Logging

### Details ###

**Content** - it makes sense that content loading be done by the library. The library should be thought of as more of an exchange. Usually libraries are resources for _checking out_ subject matter. Here, our library is also a resource for _checking in_ subject matter.

**TimeSyncedObjects** - this is still a rough idea and will take some design work. The idea is that multiple events can fire at pre-disposed periods. This will be part of the work of the EngineClock. By adding TimeSyncedObjects to a _timeSyncedList_, multiple callback functions can be called near simultaneously.

**RandomGenerator** - taking my time with this because I want something that is pretty good. Right now it is designed to be a _use and replace_ mechanism. This is still in early stages.

**Database** - <strike>this is going to start off fairly simple. I plan on using OpenOffice Calc as the data store and MySql data connection to read data.</strike> Data will be conveniently stored in .CSV format during early stages of development. We need to be able to tweak values, test numerical algorithms and value ranges. Once we have algorithms working correctly and not variable values set, we can serialize into XML during later stages.

1/22/2011: see http://www.codeproject.com/KB/database/CsvReader.aspx

**Player objects & classes** - Designing the player areas on the game board and the player classes.

**Images** - to be determined.
  * Facilities
  * Terrain

**Debugging & Logging** - we need a logging system preferably embedded in the Engine. Also, as a debugging feature, I would like to be able to show the game board grid. Other features may be added to the graphic gridview to help in image alignments, designating player areas, neutral areas, and buildings.