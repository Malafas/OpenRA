Style
=====

##Call and Return & Event-based

OpenRA is a Call and Return(Object Oriented)/Event system. It uses configuration files, bootstrap files for the world objets (which
are the actors of the game). 
Everything is prepared before the start of the game itself, so it works like:

* Get configurations files, send it to someone and work with the result.
* Get bootstrap files and define what objects will be used i.e. the mods.
* Start the game.

Depending on the used mod, object actions vary acording to their specific implementation, as well as their appearance.
Activities are ran with the help of listeners in the user interface. A typical situation in computer video games: 
click in an object, object was clicked, object reacts accordingly.
It also has an artificial inteligence component, typically a blackboard architecture.
