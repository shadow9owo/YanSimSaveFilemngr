# YanSimSaveFilemngr
An Yandere Simulator Save File manager / external cheat.

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
* trigger anti-cheat (yes)
* DLL injection (untested)

# Executable info

* virustotal - <a href="https://www.virustotal.com/gui/file/ff7935c8fd792dd4be41dd429d45d28cecc1ede10e10159b1a7040013d089bfe">9/72 most of the detections are mainly cus of the dll injection feature</a>
* md5 checksum - d39d1958e89025a3c756842122e04c5a (1.0.1.3)

# notes

* True pacifist was removed due to the kill playerprefs variable being updated too late (so it was useless)

* !!! if a value is null or not found try doing something that triggers it for example if the study points value is null just study and then go home to save it and then try again !!!
