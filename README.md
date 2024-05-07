# YanSimSaveFilemngr
An Yandere Simulator Save File manager / external cheat.

use with caution as you may corrupt your save file

# Requirements 
* .net 3.5

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

# Executable info

* virustotal - <a href="https://www.virustotal.com/gui/file/47d66630b77eaab4e132fe35965e08fbfd97d39a97b11c7a739fb12067da67cc?nocache=1">5/73 mostly ai avs detect this</a>
* md5 checksum - 1323bb4513180b25472947ad7f49021b (1.0.1.5)

# notes

* True pacifist was removed due to the kill playerprefs variable being updated too late (so it was useless)

* !!! if a value is null or not found try doing something that triggers it for example if the study points value is null just study and then go home to save it and then try again !!!
