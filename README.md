Welcome Professor Hugo!

In this project we will be talking about the OpenRA Game Engine, how it was designed and its architecture. The OpenRA project involves a lot more than just the OpenRA Game Engine, but for the purpose of the work developed, this will be our main focus.

#[Analysis and Overview](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ANALYSIS.md)
In order to be able to understand how this system works and what patterns and styles have been used during its development, we will first be doing a quick analysis and overview of the systems features and project structure.

#Architecture

This system's architectural analysis will be based on three main aspects:
* It's [**architectural patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ARCHITECTURE.md) (MVC, MVVM, MVP...) ;
* It's [**architectural styles**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/STYLE.md);
* The systems [**"4 + 1" view model**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/4+1.md), which describes its developments, logical, physical and processes views; as well as its scenarios.

#[Design]()

Finally, the main design aspect that will be evaluated in this project, is going to be its [**design patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/DESIGN.md) (GoF)


#Scale Me Up!
As requested we will now proceed to refer what we would do to turn this RTS game engine into an Massive Multiplayer Online RTS (MMORTS) game engine.

The OpenRA Game Engine currently allows for multiplayer online engagements between parties of up to 16 players. In order for it to become an MMO-Supported Engine it would need to enable parties of hundreds to thousands of players and for this there are three basic aspects that need to be changed before any major architectural adjustments is made:

1. The maps need to be much larger, they should become "game worlds" with separate sections with the capabilty of communicating with each other to allow the exchange of resources between players;
2. More diversity when it comes to team choices;
3. And a lot more customization so that players can distinguish themselves.


##Current Architecture
Currently, the games online experience is conducted by two main components:


![](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/PhysicalView/OpenRA Not MMO.png)


* **Game Server-** in which games and matches are running
* **Client Machine-** the representation of the game running on the game server, which the user can interact with

This simple HTTP Request-based Service Oriented Architecture works as follows:
* The Client Machine registers in the Game Server as interested in the state of the game running on the server;
* The Game Server sends state changes information to the Client Machine;
* The previous steps repeat as long as the player is connected to that game session;
* While this happens, the user interacts with their machine which then proceeds to post that information to the Game Server.

With this Architecture and today's technology, we can just have a single physical machine to host every game server since the vast majority of players are European (as seen by the currently active online servers).

This type of architecture may work for a small scale online experience, but the amount of HTTP requests handled in an MMO game would quickly burn through this machine's resources in no time. Therefore we need to make big adjustments.

##Persistance Is Key
First of all, every single piece of persistable data needs to be saved in separate machines not only to decrease the systems coupling but also for fault tolerance, which decreases its MTBF (Mean Time Between Failure) and increase its MTTF (Mean Time To Failure).

###Tolerate Your Faults

Since we are already talking about fault-tolerance let's think about how will we be able to persist data and ensure that all saved data is as fault tolerant as possible.

RAID X a.k.a. Redundant Array of Independent Disks is a data storage virtualization technology that combines multiple physical disk drive components into a single logical unit for the purposes of data redundancy, performance improvement, or both.

"X" stands for the amount of different arrangements that can be setup in order to best fit a systems requirements.

####So... Which one?

1. RAID 0: Striping
  * There is no redundancy.
  * If a single disk fails the information is lost.
  * **Answer:** No

2. RAID 1: Mirror
  * It does not assure as much safety as RAID 5 or 6.
  * **Answer:** Maybe

3. RAID 2: ECC (Error Correcting Code)
  * Using disks just for ECC is a relatively outdated technique because nowadays disks already have this internal correction.
  * **Answer:** Nope

4. RAID 3
  * Simplified version of RAID 2.
  * Random undetected errors are not fixed, but it assures that disks faults are fixed.
  * **Answer:** Maybe

5. RAID 4
  * Obsolete.
  * **Answer:** Noperino

6. RAID 5
  * Evolved form of RAID 4, but if a disk fails all reading and writing proccess are slowed down while the failure is getting fixed.
  * **Answer:** May B

7. RAID 6
  * Evolved form of RAID 5. It uses twice as many parity bits which assures data integrity with up to two simultaneous disk failures.
  * **Answer:** Maybe

8. RAID 01: Mirror w/ Underlying Stripping
  * Too expensive and it isn't as safe as RAID 5 and 6
  * **Answer:** Nopez

9. RAID 10: Stripping w/ Underlying Mirror
  * Too expensive and it isn't as safe as RAID 5 and 6
  * **Answer:** Nop Again

10. RAID 50: Stripping w/ Underlying Parity
  * High Speed rates. Excellent for server implementation but it's expansion is too expensive.
  * **Answer:** Maybee

11. RAID 100: Double Stripping w/ Mirror
  * Unecessary complexity.
  * **Answer:** Nope



Given all of the aspects refered previsouly we found RAID 50 to be the most fitting. So all we have to do now is implement this technology.

####Ok So... How?

First we need to ask ourselves another question:

*Will the persisted data be distributed within the RAID via a controller separate from the main machine (Hardware Implementation) or via a software that is installed in the main machine (Software Implementation)?*

Given the high coupling profile of the Software implementation we pre-empted that, for the purpose of this system, the Hardware Implementation will be a more fitting approach. But let us check the Pros and Cons of each implementation first.

**Software Implementation**
* The processing of the tasks would have to be done by the CPU of a non-specific machine.
* It would require an abstraction level between logic (RAID) and the physical disks done by the OS.
* Less expensive.

**Hardware Implementation**
* With hot-swapping if a disk fails, it can be swapped without turning off the system.
* Non-volatile Cache (with a backup battery) so that pending writing processes are not lost in case of sudden power cut.
* It won't overload another machine with data processing.
* It allows for the support of multiple operating systems because the controller will just be a simple driver.
* More expensive.

As we predicted, this system will greatly improve with a Hardware Implementation of the RAID 50.

### No Data Left Behind!
Now that our data is significantly fault-tolerant we can decide what kind of data base systems do we want to have for our Game Engine.
We have a substancial amount data being persisted in our data bases, but they all have different purposes. We can already imagine a series of apparently similar, but very different types of data:
* Players' unit movements;
* The creation of new units;
* The destruction or theft of other player's resources and units;
* Players' score;
* Players' partnerships;
* Players' number of units and resources.

Some of these are relevant to the current game session (unit movements, units created, units destructed, resources spent, etc), but have no real value for the players statistical data e.g. how many units have crossed coordinates (53004, 40053) in the Dune map. And other data, like a Players score and total number of enemy units destroyed are important in Leaderboards. We can clearly see that there are two types of data here, short-term and long-term data.

These two distinct types of data also have a very distinctive "flow":
* Short-Term Data: is being created, sent and deleted extremely often;
* Long-Term Data: is usually only sent at the end of a game session and some of it is never deleted.

Therefore we have decided that we would have two different types of databases:
* NoSQL: databases which would store short-term data;
* SQL: databases which would store long-term data.

##Mr.Worldwide
Then after choosing the most fitting fault tolerance technique and the amount of physical machines needed for the Database Management System, there is a big problem that needs to be tackled: since game worlds are extremely large and have separate sections, each section should be running as a standalone server instead of having the enterity of the map in a single server. Not only that but since the game is global, we need to have active servers across the world to allow fast communications between servers and clients, and these servers must be interconnected for transcontinental data transactions

This means that a game world would physically be distributed in a Server Farm, multiply that by the amount of game worlds running at the same time and we have multiple sets of server farms distributed across the globe communicating not only within farms but between continents.

###How should we approach this?

If we were to stick with our current architecture, we would end up having a Client side communicating with a Server side made up of several Server Farms that knew each other and could direct you in every direction if you were to ask them were the other Servers were. This would mean that every server would have to keep track of other Server's locations within the network while processing their own game servers.

This, of course, is a very bad idea not only for inefficiency but for latency purposes as well.

So our solution for this problem would be to add several "Game Selection" Servers which would serve as "doormen" or "router" for all the Game Servers currently running. When the player wants to change to a different game world or go to a different part of the world that is running on another server, the "Game Selection" server tells the client were that game world is located and proceeds to tell them were to communicate. After that the client connects "directly" to the game world.

![](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/PhysicalView/OpenRA Tryin' MMO.png)

##Change Doesn't Have To Be Scary (games persist across platforms)
