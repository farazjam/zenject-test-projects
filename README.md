# Zenject-test-projects
- These Example Projects using Zenject to have more clarity and understanding. As the ones that come along are already dense enough to confuse or repel newbies
- Zenject is now [Extenject](https://github.com/Mathijs-Bakker/Extenject)

## Installation
- Make sure your development directory or unity project path doesn't get too long as Extenject also have some long file names that make it beyond the allowed 260 path limit in Windows OS. Nonetheless allow [Long Path Names](https://gearupwindows.com/resolved-destination-path-too-long-when-moving-copying-a-file/) to let zenject long file names work properly
- Make an empty unity project, setup scripting backend to IL2CPP and .Net 4.0
- Download latest version from [release page](https://github.com/Mathijs-Bakker/Extenject/releases) as zip, unpack it and copy-paste UnityProject/Assets/Plugins/Zenject folder into your Unity project under /Plugin preferrably
- Once zenject files are copied, make a solution build and ensure all assemblies are building correctly. Check sample games and it's installers if they are loaded correctly before using

## Project1: Z1-hello-world
> Checkout consol for the output
- Most basic level hello world in standard unity style and with basic dependency injection
- This helps distinguish how DI (Dependency Injection) and Binding will change things for us
- Standard and DI's version both exists for comparison/understanding

## Project2: Z2-cube-spawner
> Usage: Click on screen to spawn cubes
- Demonstrates how to spawn cubes in a standard unity way vs DI's way
- In standard way the spawner-script need to have prefab (dependency) and have to do instantiation on it's own
- This project also uses a simple **Factory** to take the instation-responsibility out of spawn-script, the Factory holds prefab and provides instantated objects when asked to. Better, but I had to write Factory on my own, Factory is monobehavior class, an active gameobject in hierarchy and Factory does have its dependencies

- In DI's way, Zenject's readymade **PlaceholderFactory<T>** is used. It's not a Monobehavior. The flow is that MonoInstaller have prefab (Main root), it binds the said factory with a type. The factory is injected to Spawner which can ask the Factory to spawn when it asks it to. This way the spawner doesn't know which objects are provided to it, there is no need to create a factory - zenject provides it. Installer binds and injects the factory in start to whereever it's needed. Therefore we have 1 single point (root scene context), that have the prefab, injects the factory. Factory and it's type can be changed with just 1 line in Installer without modifying other classes

- MemoryPool also provides instantiation and pooling, provided by Zenject. With this, we don't need a factory only pool and use Spawn(), Despawn() methods that are handled by pool's OnSpawned(), OnDespawned(). Until now, it was all runtime instatiaion and no pooling

## Project3: Z3-cube-mini-game
### About Game

  - A mini game where you can move player block left/right with [A],[D]. Coin and hurdle blocks randomly spawn and fall like rain from top. Collect coins to get score and avoid hurdles. Get a significant amount of coins to clear level and if you touch hurdle, it will be level failed 

  - When start button is pressed, the supreme GameManager asks all Systems to Clear() and Start(). For example, now the InputManager (A system) is active and taking inputs. When all systems are active, gameplay is conducted. Now let's say player catches a hurdle, it's a gameover. Gamestate is managed by GameStateHandler (Another system) that fires an event regarding this and GameManager (the supreme) decides to shut down all other systems as game is now over.

![Alt Text](/z3/mini-game.gif)
  
### Standard Project
> Managers and Systems have their distinct roles that they perform to conduct gameplay
- **Managers**
  - GameManager
    - Responsibility: Supreme Manager that is incharge of all the systems. It starts and stops them while managing the system state
    - GameView informs it when the start and stop buttons are clicks so that it acts accordingly
  - DataManager
    - Responsibility: Data persistence. Keeps score and level data. Persists level only
- **Systems**
  - Game State Handler:
    - System's and their state may be different that Game's State. Systems only Starts, Stops and Clears while Game can have many states
    - Responsibility: Manages Game State and act accordingly. Entities talk to it for any change in Game state like add score if coin is picked
  - Input Manager:
    - Responsibility: Process user input and fire events with direction information the player should move towards
  - Player Controller:
    - Responsibility: Holds ther player-block, Spawn() Despawn() according to system's state 
  - Swerve Movement:
    - Responsibility: Given block will do swerve-movement according to the user-input
  - Coin Generator:
    - Responsibility: Spawns coins non-stop but according to game settings
  - Hurdle Generator:
    - Responsibility: Spawns hurdles non-stop but according to game settings
- **Entities**
  - PlayerBlock
  - CoinBlock
  - HurdleBlock
- **Views**
  - GameView: Shows data for score, level and have buttons for starting and stopping game
- **Data**
  - CubeGameData
    - Scriptable object to have game settings
> Out of scope: Pooling, SpawnManagement. For now runtime instantiaion/destruction is happening
  
### Zenject Project
- The core project and approach is still the same, it's just that the previous project is converted into a DI project to demonstrate how it works
- All the dependencies are now injected via CubeGameInstaller.cs
- No System or Manager derives from MonoBehavior, they all uses IInitializable, IDisposable provided by Zenject. They are also not present as Gameobjects in hierarchy. This also implies game logic are not MonoBehaviors
- Since Systems/Managers are not MonoBehaviors but have gamelogic, therefore Coroutine and Update could not be used. ITickable's Tick() is Zenject's alternate of Update() and [UniTask](https://github.com/Cysharp/UniTask) is a thread safe alternate of Tasks which are used here
- The only thing that exists in hierarchy is View which is a single MonoBehavior and a GameObject
- All kind of Blocks derive from Block.cs which is a MonoBehavior and an active participant of gameplay like collision/trigger happens on them
- [Signals](https://github.com/modesttree/Zenject/blob/master/Documentation/Signals.md) are used instead of events, all signals are in Signals.cs
- CoinBlock and HurdleBlock uses MonoMemoryPool provided by the framework for object pooling. Now no runtime instantation/destruction is happening
- A Despawner (Cube with collider) is used that wasn't present in previous/standard project for object pooling purposes
- It may feel odd for first timer on why most of the systems/managers have interfaces. The reason is that these are classes not MonoBehavior GameObjects that are already present in hierarchy and active. Zenject binds them with their classtypes on compile time and need their interfaces to inject these dependencies where ever they are needed. If someone asks for PlayerBlock it will get an exception, IPlayerBlock is the way to go

## Project4 : Z4-snake-game
### About Game
- A typical snake game where you get the food blocks (green) and avoid hurdle blocks (red) to get score. After a certain score the level completes. If snake block (blue) touches hurdle block then level failed
- Sumpreme Game Manager starts, stops all systems and systems work together to conduct gameplay
  
### Standard Project

#### Managers
- **GameManager**: Supreme Manager that starts and stops systems. It works in conjunction with GameState Handler
- **DataManager**: Responsible for game data & it's persistence at one point througout the game. Persists score and level no.
- **Map**: Utility class for direction determination && coordinates validation

#### Systems
> Systems are indepedent game units that have a single responsibility.

GameManager is responsible for ***Clearing***, ***Starting*** and ***Stopping*** all systems according to game state

- **Input Manager**: Responsible for keyboard inputs (W.A.S.D.)
- **Snake Movement Controller**: Moves the snake, works with Map to check if the snake can be moved on to next tile
- **Occupancy Handler**: Keeps a record of which tiles are occupied with certain blocks. Responsible for keeping 1 block at a tile
- **Food Manager**: Spawns/Despawns Food Blocks according to game settings. Requests score addition if block is acquired
- **Hurdle Manager**: Spawns/Despawns Hurdle Blocks according to game settings. Requests level failure if block is touched

#### Views
> Views as in MVC are only allowed to be reflect data and not process any gamelogic. Views are fed from their respective Managers
- **GameManagerView** : Shows Current Score, Current Level Text. Have Start and Stop Game Buttons
- **MapView** : Scales the 3D ground object according to game settings map size to achieve dynamic map

#### Entities
Since the game consists of blocks, there are different block types. All blocks share smake interface, have coordinate and type
- FoodBlock: Get it to increase score that leads to level completion
- HurdleBlock: Avoid touching it or it will be a level fail. No physics used
- SnakeBlock: Player

#### Potential Improvements
These are not implemented and considered out of scope for this demo project but are a good to have
- Pooling: Currently objects are instantiated/destroyed on runtime which is not a best practice. 
- Snake Body: Curreny snake is with head block only, a body will look nice
- Both Food and Hurdle Managers try to find an empty tile before spawning a block. With max occupancy they will go into an infinite loop. Should be handled

#### Misc
- SnakeGameData: is scriptable object, all game settings can be changed from there (_SnakeGame/SnakeGameData)
- Blue Color = Snake Head Block
- Red Color = Hurdle
- Green Color = Food

### Zenject Project
- Do it as an assignment
