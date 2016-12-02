# Development/Implementation View
The development/implementation view illustrates a system from a programmer's perspective and is concerned with software management.

## Component Diagram
From the perspective given by the following package diagram we can see that there are some "cyclic dependencies" which reflect some negative aspects in the system.
![OpenRA's Game Engine Package Diagram](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/DevelopmentView/ComponentDiagram.png)

## Package Diagram
The OpenRA project has a big set of packages in its scope, but in regards to the Game Engine in itself, the only package that we took into consideration was the "OpenRA.Game" given the fact that engine's logic resides in it.
![OpenRA's Game Engine Package Diagram](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/DevelopmentView/PackageDiagram.png)
