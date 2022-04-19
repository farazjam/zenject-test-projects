# Zenject-test-projects
Example Projects using Zenject to have more clarity and understanding. 
As the ones that come along are already dense enough to confuse or repel newbies

## Installation
- Make an empty unity project, setup scripting backend to IL2CPP and .Net 4.0
- Do not create any script, try to import [Extenject from Asset store](https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735) first and build it's assemblies
- Edit Tests assembly in Visual Studio may not load, feel free to delete it
- Now you may create your own scripts and use Zenject source files

## Project1: Z1-hello-world
- Most basic level hello world in standard unity style and with basic dependency injection
- This helps distinguish how DI (Dependency Injection) and Binding will change things for us

## Project2: Z2-cube-spawner
- Demonstrates how to spawn cubes in a standard unity way vs DI's way
- In standard way the spawner-script need to have prefab (dependency) and have to do instantiation on it's own
- This project also uses a simple **Factory** to take the instation-responsibility out of spawn-script, the Factory holds prefab and provides instantated objects when asked to. Better, but I had to write Factory on my own, Factory is monobehavior class, an active gameobject in hierarchy and Factory does have its dependencies
- In DI's way, Zenject's readymade **PlaceholderFactory<T>** is used. It's not a Monobehavior. The flow is that MonoInstaller have prefab (Main root), it binds the said factory with a type. The factory is injected to Spawner which can ask the Factory to spawn when it asks it to. This way the spawner doesn't know which objects are provided to it, there is no need to create a factory - zenject provides it. Installer binds and injects the factory in start to whereever it's needed. Therefore we have 1 single point (root scene context), that have the prefab, injects the factory. Factory and it's type can be changed with just 1 line in Installer without modifying other classes
  - MemoryPool also provides instantiation and pooling, provided by Zenject. With this, we don't need a factory only pool and use Spawn(), Despawn() methods that are handled by pool's OnSpawned(), OnDespawned(). Until now, it was all runtime instatiaion and no pooling

