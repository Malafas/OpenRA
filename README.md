Welcome Professor Hugo!

In this project we will be talking about the OpenRA Game Engine, how it was designed and its architecture. The OpenRA project involves a lot more than just the OpenRA Game Engine, but for the purpose of the work developed, this will be our main focus.

#[Analysis and Overview](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ANALYSIS.md)
In order to be able to understand how this system works and what patterns and styles have been used during its development, we will first be doing a quick analysis and overview of the systems features and project structure.

#[Architecture]()

This system's architectural analysis will be based on three main aspects:
* It's [**architectural patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ARCHITECTURE.md);
* It's [**architectural styles**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/STYLE.md);
* The systems [**"4 + 1" view model**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/4+1.md), which describes its developments, logical, physical and processes views; as well as its scenarios.

#[Design]()

Finally, the main design aspect that will be evaluated in this project, is going to be its [**design patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/DESIGN.md)


#MMORTS
As requested we will now proceed to refer what we would do to turn this RTS game engine into an Massive Multiplayer Online RTS (MMORTS) game engine.

The OpenRA Game Engine currently allows for multiplayer online engagements between parties of up to 16 players. In order to make it to become an MMO-Supported Engine it would need to enable parties of hundreds to thousands of players and for this there are three basic aspects that need to be changed before any major architectural adjustments is made:

*The maps need to be much larger;
*More diversity when it comes to team choices;
*And a lot more customization so that players can distinguish themselves.


##Current Architecture
Currently, the games online experience is conducted by two main components:

* **Game Server-** in which games and matches are running
* **Client Machine-** the representation of the game running on the game server, which the user can interact with

This simple HTTP Request-based Service Oriented Architecture works as follows:
* The Client Machine asks the Game Server for the state of every actor in the game;
* The Game Server then responds to the Client Machine;
* The previous steps repeat as long as the player is connected to that game session;
* While this happens, the user interacts with their machine which then proceeds to post that information to the Game Server.

With this Architecture and today's technology, we can just have a single physical machine to host every game server since the vast majority of players are European (as seen by the curently active online servers)

##Scale Me Up!
