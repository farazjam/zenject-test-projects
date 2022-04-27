# Snake-game-standard
A sample snake game project with best practices followed. It is developed to make a point about independent systems and 
how they can talk to each other on a minimum level to achieve a clean code and are handled by one single supreme manager

## Managers
- **GameManager**: Supreme Manager that starts and stops systems. It works in conjunction with GameState Handler
- **DataManager**: Responsible for game data & it's persistence at one point througout the game. Persists score and level no.
- **Map**: Utility class for direction determination && coordinates validation

## Systems
> Systems are indepedent game units that have a single responsibility.

GameManager is responsible for ***Clearing***, ***Starting*** and ***Stopping*** all systems according to game state

- **Input Manager**: Responsible for keyboard inputs (W.A.S.D.)
- **Snake Movement Controller**: Moves the snake, works with Map to check if the snake can be moved on to next tile
- **Occupancy Handler**: Keeps a record of which tiles are occupied with certain blocks. Responsible for keeping 1 block at a tile
- **Food Manager**: Spawns/Despawns Food Blocks according to game settings. Requests score addition if block is acquired
- **Hurdle Manager**: Spawns/Despawns Hurdle Blocks according to game settings. Requests level failure if block is touched

## Views
> Views as in MVC are only allowed to be reflect data and not process any gamelogic. Views are fed from their respective Managers
- **GameManagerView** : Shows Current Score, Current Level Text. Have Start and Stop Game Buttons
- **MapView** : Scales the 3D ground object according to game settings map size to achieve dynamic map

## Entities
Since the game consists of blocks, there are different block types. All blocks share smake interface, have coordinate and type
- FoodBlock: Get it to increase score that leads to level completion
- HurdleBlock: Avoid touching it or it will be a level fail. No physics used
- SnakeBlock: Player

## Potential Improvements
These are not implemented and considered out of scope for this demo project but are a good to have
- Pooling: Currently objects are instantiated/destroyed on runtime which is not a best practice. 
- Snake Body: Curreny snake is with head block only, a body will look nice
- Both Food and Hurdle Managers try to find an empty tile before spawning a block. With max occupancy they will go into an infinite loop. Should be handled

## Misc
- SnakeGameData: is scriptable object, all game settings can be changed from there (_SnakeGame/SnakeGameData)
- Blue Color = Snake Head Block
- Red Color = Hurdle
- Green Color = Food

![alt text](Assets/_Snake%20Game/Screenshots/Game.png)
