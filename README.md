# Zenject-test-projects
- These Example Projects using Zenject to have more clarity and understanding. As the ones that come along are already dense enough to confuse or repel newbies
- Zenject is now [Extenject](https://github.com/Mathijs-Bakker/Extenject)

## Installation
- Make sure your development directory or unity project path doesn't get too long as Extenject also have some long file names that make it beyond the allowed 260 path limit in Windows OS. Nonetheless allow [Long Path Names](https://gearupwindows.com/resolved-destination-path-too-long-when-moving-copying-a-file/) to let zenject long file names work properly
- Make an empty unity project, setup scripting backend to IL2CPP and .Net 4.0
- Download latest version from [release page](https://github.com/Mathijs-Bakker/Extenject/releases) as zip, unpack it and copy-paste UnityProject/Assets/Plugins/Zenject folder into your Unity project under /Plugin preferrably
- Once zenject files are copied, make a solution build and ensure all assemblies are building correctly. Check sample games and it's installers if they are loaded correctly before using

## Project1: Z1-hello-world
- Most basic level hello world in standard unity style and with basic dependency injection
- This helps distinguish how DI (Dependency Injection) and Binding will change things for us
- Standard and DI's version both exists for comparison/understanding

## Project2: Z2-cube-spawner
- Demonstrates how to spawn cubes in a standard unity way vs DI's way
- In standard way the spawner-script need to have prefab (dependency) and have to do instantiation on it's own
- This project also uses a simple **Factory** to take the instation-responsibility out of spawn-script, the Factory holds prefab and provides instantated objects when asked to. Better, but I had to write Factory on my own, Factory is monobehavior class, an active gameobject in hierarchy and Factory does have its dependencies

- In DI's way, Zenject's readymade **PlaceholderFactory<T>** is used. It's not a Monobehavior. The flow is that MonoInstaller have prefab (Main root), it binds the said factory with a type. The factory is injected to Spawner which can ask the Factory to spawn when it asks it to. This way the spawner doesn't know which objects are provided to it, there is no need to create a factory - zenject provides it. Installer binds and injects the factory in start to whereever it's needed. Therefore we have 1 single point (root scene context), that have the prefab, injects the factory. Factory and it's type can be changed with just 1 line in Installer without modifying other classes

- MemoryPool also provides instantiation and pooling, provided by Zenject. With this, we don't need a factory only pool and use Spawn(), Despawn() methods that are handled by pool's OnSpawned(), OnDespawned(). Until now, it was all runtime instatiaion and no pooling

## Project3: Z3-cube-min-game
### About Game

  - A mini game where you can move player block left/right with [A],[D]. Coin and hurdle blocks randomly spawn and fall like rain from top. Collect coins to get score and avoid hurdles. Get a significant amount of coins to clear level and if you touch hurdle, it will be level failed 

  - When start button is pressed, the supreme GameManager asks all Systems to Clear() and Start(). For example, now the InputManager (A system) is active and taking inputs. When all systems are active, gameplay is conducted. Now let's say player catches a hurdle, it's a gameover. Gamestate is managed by GameStateHandler (Another system) that fires an event regarding this and GameManager (the supreme) decides to shut down all other systems as game is now over.

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
  
  
  
  
