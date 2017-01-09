Welcome Professor Hugo!

In this project we will be talking about the OpenRA Game Engine, how it was designed and its architecture. The OpenRA project involves a lot more than just the OpenRA Game Engine, but for the purpose of the work developed, this will be our main focus.

#[Analysis and Overview](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ANALYSIS.md)
In order to be able to understand how this system works and what patterns and styles have been used during its development, we will first be doing a quick analysis and overview of the systems features and project structure.

#[Architecture]()

This system's architectural analysis will be based on three main aspects:
* It's [**architectural patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/ARCHITECTURE.md) (MVC, MVVM, MVP...) ;
* It's [**architectural styles**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/STYLE.md);
* The systems [**"4 + 1" view model**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/4+1/4+1.md), which describes its developments, logical, physical and processes views; as well as its scenarios.

#[Design]()

Finally, the main design aspect that will be evaluated in this project, is going to be its [**design patterns**](https://github.com/Malafas/OpenRA/blob/bleed/ADS/DESIGN.md) (GoF)


#MMORTS
As requested we will now proceed to refer what we would do to turn this RTS game engine into an Massive Multiplayer Online RTS (MMORTS) game engine.

The OpenRA Game Engine currently allows for multiplayer online engagements between parties of up to 16 players. In order to make it to become an MMO-Supported Engine it would need to enable parties of hundreds to thousands of players and for this there are three basic aspects that need to be changed before any major architectural adjustments is made:

*The maps need to be much larger, they should become "game worlds" with separate sections with the capabilty of communicating with each other to allow the exchange of resources between players;
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

With this Architecture and today's technology, we can just have a single physical machine to host every game server since the vast majority of players are European (as seen by the currently active online servers)

##Scale Me Up!

This type of architecture may work for a small scale online experience, but the amount of HTTP requests handled in an MMO game would quickly burn through this machine's resources in no time. Therefore we need to make big adjustments.

##Persistance is Key
First of all, every single piece of persistable data needs to be saved in separate machines not only to decrease the systems coupling but also for fault tolerance which decreases its MTBF (Mean Time Between Failure) and increase its MTTF (Mean Time To Failure).

Since we are already talking about fault-tolerance let's think about how will we be able to persist data and ensure that all saved data is as fault tolerant as possible.

###RAID X
Redundant Array of Independent Disks is a data storage virtualization technology that combines multiple physical disk drive components into a single logical unit for the purposes of data redundancy, performance improvement, or both.

"X" stands for the amount of different arrangements that can be setup in order to best fit a systems requirements.

####So... Which one?

**RAID 0: Striping**
There is no redundancy. If a single disk fails the information is lost.
**Answer:** No

**RAID 1: Mirror**
It does not assure as much safety as RAID 5 or 6.
**Answer:** Maybe

**RAID 2: ECC (Error Correcting Code)**
Using disks just for ECC is a relatively outdated technique because nowadays disks already have this internal correction.
**Answer:** Nope

**RAID 3**
Simplified version of RAID 2. Random undetected errors are not fixed, but it assures that disks faults are fixed.
**Answer:** Maybe

**RAID 4**
Obsolete.
**Answer:** Noperino

**RAID 5**
Evolved form of RAID 4, but if a disk fails all reading and writing proccess are slowed down while the failure is getting fixed.
**Answer:** May B

**RAID 6**
Evolved form of RAID 5. It uses twice as many parity bits which assures data integrity with up to two simultaneous disk failures.
**Answer:** Maybe

**RAID 01: Mirror w/ Underlying Striping**
Too expensive and it isn't as safe as RAID 5 and 6
**Answer:** Nopez

**RAID 10: Striping w/ Underlying Mirror**
Too expensive and it isn't as safe as RAID 5 and 6
**Answer:** Nop Again

**RAID 50: Striping w/ Underlying Parity**
High Speed rates. Excellent for server implementation but it's expansion is too expensive.
**Answer:** Maybee

**RAID 100: Double Striping w/ Mirror**
Unecessary complexity.
**Answer:** Nope

**FINAL ANSWER:** RAID 50 FTW

Having taken this into consideration our system's data persistency needs to assure the following:
* All persisted data will be managed by a controller independent of the main machine: Jogo não pode parar, hot-swapping (disco falha pode ser trocado sem desligar o sistema)
* Prevê cache maior performance.
* Cache não-volátil (protegido por bateria) escritas pendentes não são perdidas se falha energia.
* Performance garantida, não sobrecarrega processador
* Podem suportar vários sistemas operativos (controlador apresenta ao sistema um disco simples).

Implementação por software apresenda a grande desvantagem de o processamento ser feito pelo CPU, assim como ser necessária uma abstração entre
a operação lógica (RAID) e os discos físicos, a cargo do sistema operativo.

##Mr.Worldwide
Then after choosing the most fitting fault tolerance technique and the amount of physical machines needed for the Database Management System, there is a big problem that needs to be tackled: since game worlds are extremely large and have separate sections, each section should be running as a standalone server instead of having the enterity of the map in a single server.

Not only that but since the game is global, we need to have active servers across the world to allow fast communications between servers and clients, and these servers must be interconnected for transcontinental data transactions

This means that a game world would physically be distributed in a Server Farm, multiply that by the amount of worlds running at the same time and we have multiple sets of server farms distributed across the globe communicating not only within farms but between continents.
