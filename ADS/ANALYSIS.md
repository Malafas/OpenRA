#Analysis

##Introduction

OpenRA is a project meant to recreate and modernize the classic Command & Conquer real time strategy (RTS) games. It is made of two major elements: the OpenRA engine, a cross-platform open source game engine that provides a common platform for building RTS games; and OpenRA mods, the tweaks and changes that can be made to the common RTS platform provided by the OpenRA engine.

The OpenRA engine is free comes with three distinct mods: Red Alert, Tiberian Dawn and Dune 2000. It includes native support for modern operating systems (Windows 10, macOS, and most Linux distros) without relying on emulation or binary hacks.

##Technology
###.Net
OpenRA's engine and mods were developed in C# making use of Microsoft's .Net Framework. In order to make it cross-platform, OpenRa uses the Mono framework to extend the Microsoft based development to other platforms.
###Lua API
OpenRA allows custom maps and missions to be scripted using Lua 5.1. These scripts run in a sandbox that prevents access to unsafe functions (e.g. OS or file access), and limits the memory and CPU usage of the scripts.



##OpenRA Engine
###Game
All units/structures/most things in the map are Actors. Actors contain a collection of traits. Traits consist of an info class and a class that does stuff. There is one instance of the infoclass shared across all actors of the same type. Each actor gets its own instance of the trait class itself. Infoclasses are responsible for instantiating their corresponding trait class -- see ITraitInfo, and TraitInfo for the trivial implementation of this. In some cases the trait class's constructor needs some args, in which case TraitInfo can't be used. This is a limitation of C# generics.

####Online
OpenRA also supports online multiplayer where players can play against each other in a battle for full battleground control.
Despite being out of the scope of this project, the game engine interacts with the OpenRA Master Server that handles all requests regarding online multiplayer battles.

##Artificial Intelligence
Given that it is a game, many of the units

##OpenRA Mods
####Mapping
OpenRA comes with an in-game map editor. It allows users to create, edit and delete maps.
One can also import maps from the original Command & Conquer games into the OpenRA engine. This can be done by running the `OpenRA.Utility.exe` command
####Audio
WAV and AUD file formats are the only ones accepted by the OpenRA engine. These files must be in a 16-bit 22050 Hz format.

##Branching Structure

##Repository Structure

YAML files

Use Cases 
