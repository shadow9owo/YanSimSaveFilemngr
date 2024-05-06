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
* DLL injection (unstable but works) fork of <a href="https://github.com/ihack4falafel/DLL-Injection/">https://github.com/ihack4falafel/DLL-Injection/</a>

# Executable info

* virustotal - <a href="https://www.virustotal.com/gui/file/a534a60a93ca0a15363a0f1496de3e420ef18b74fda40e1e5bd7362f14dae527?nocache=1)">9/73 most of the detections are mainly cus of the dll injection feature</a>
* md5 checksum - 07f7b8e512e8c36e3f24791302a1decb (1.0.1.4)

# notes

* True pacifist was removed due to the kill playerprefs variable being updated too late (so it was useless)

* !!! if a value is null or not found try doing something that triggers it for example if the study points value is null just study and then go home to save it and then try again !!!
