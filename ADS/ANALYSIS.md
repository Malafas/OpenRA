#Analysis

##Introduction

OpenRA is a project meant to recreate and modernize the classic Command & Conquer real time strategy (RTS) games. It is made of two major elements: the OpenRA engine, a cross-platform open source game engine that provides a common platform for building RTS games; and OpenRA mods, the tweaks and changes that can be made to the common RTS platform provided by the OpenRA engine.

It includes native support for modern operating systems (Windows 10, macOS, and most Linux distros) without relying on emulation or binary hacks.

##Technology
###.Net
OpenRA's engine and mods were developed in C# making use of Microsoft's .Net Framework. In order to make it cross-platform, OpenRa uses the Mono framework to extend the Microsoft based development to other platforms.

###Lua API
OpenRA allows custom maps and missions to be scripted using Lua 5.1. These scripts run in a sandbox that prevents access to unsafe functions (e.g. OS or file access), and limits the memory and CPU usage of the scripts.

##Repository Structure


##Artificial Intelligence
