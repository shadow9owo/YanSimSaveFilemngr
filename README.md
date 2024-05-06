# YanSimSaveFilemngr
A basic Yandere Simulator Save File manager / external cheat.

use with caution as you may corrupt your save file

# Requirements 
* .net 4.0

# Features

* infinite money
* set reputation
* set study points
* set info points
* the ability to export saves to a .reg file
* numbness lock (mostlikely dosent work as expected this is suppoused to lock the sanity)
* school population managment (expel / unexpel everyone)
  - note this dosent work on monday as the values this is trying to change dont exist on monday
* the ability to inject text to the title of the yansim window
* works with 89s mode
* works with mission mode (unteseted)

# Executable info

* virustotal - <a href="https://www.virustotal.com/gui/file/9aec5f2cf6add717e2c5c528cf0edb934f3b4fc8cecf55049afad8d4ca91c677?nocache=1">5/72 virustotal (AI antiviruses mostly flag it as a false positive)</a>
* md5 checksum - 3450ee6720423034ae3a0c9f68beec9b (1.0.1.2)

# notes

* True pacifist was removed due to the kill playerprefs variable being updated too late (so it was useless)

* !!! if a value is null or not found try doing something that triggers it for example if the study points value is null just study and then go home to save it and then try again !!!
