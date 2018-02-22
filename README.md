# SLOBSRemoteControl
Named Pipe Client for SLOBS

This software is distributed "as is", with no warranty expressed or implied, and no guarantee for accuracy or applicability to any purpose.

To use this Client you need to activate the Named Pipe Api. To do this you must start SLOBS with " --adv-settings" Parameter, go to Settings and under API activate the Named Pipe.

For remote access use this as Named pipe under config: \\<PC-Name>\pipe\<PipeName>
where PC-Name is the Hostname where SLOBS run and PipeName is the name which is set in SLOBS (default: slobs) 
This is currently untested because i haven't acces to an other PC in my network.

-h : Shows this Help

-g : Shows the Config Gui

-m : Select Mode: help (same as -h) ||| gui (same as -g) ||| switch_scene ||| toggle_source ||| toggle_audio
                      
-s : Scene (required for switch_scene, optional for toggle_source)

-i : Source to toggle (required for toggle_source)

-a : Audio Source to toggle


Examples:
Switch Scene to "Idle Scene":
SLOBSRemoteControl.exe -mswitch_scene -s"Idle Scene"

Toggle Source "Background Wallpaper" in Scene "Idle Scene":
SLOBSRemoteControl.exe -mtoggle_source -s"Idle Scene" -i"Background Wallpaper"

Toggle Source "Background Wallpaper" in current active scene:
SLOBSRemoteControl.exe -mtoggle_source -i"Background Wallpaper"

Toggle Audio Source "Sound":
SLOBSRemoteControl.exe -mtoggle_audio -a"Sound"


You like it and want to support me? Leave a follow on Twitch (http://twitch.bootscreen.net) or when you want to support me more you can donate here (https://www.paypal.me/TwitchBootscreen/5)
