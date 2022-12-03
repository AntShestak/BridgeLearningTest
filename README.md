# BridgeLearningTest
Unity 3D demo developed from supplied instructions

### Unity version 
2020.3.25f1

### Screen resolution 
FullHD (1920x1080)

### Play
To play load scene **Level1**. If you want to debug from other scene, set GameController object to active in that scene.

### Black Screen
If your game screen is black it is bacause of screen fader. Press play or change alpha values on screen fader image component. 
Do not deactivate Fader object, it will result in NullReferenceException

### Game End Condition
Game end condition is checked using simplified Floodfill algorithm. No path needs to be tracked.

### Level Sizes
Levels are getting smaller as game progresses as it becomes harder to maneuvre between obstacles.

### Score
Score is being tracked through levels. To get to Level2 player has to score 100. To get to Level3 - 200. 400 on Level3 is a game win condition.

Score is deducted when same type of objects are picked in a row, this might result in **negative score**. This does not seem right but complies with provided instructions.

### Optimization

Game runs slow, optimisation is required but could not be done due to deadline.

### Code Refactoring

SurfaceTilingController.cs responsible for surface segmentation into tiles. Grid class could be extracted. As well as other refactoring must be done to improve usability and efficiency.

Obstacle and collectible controllers could use same spawning mechanism and tile allocation mechanisms. Refactoring is necessary.

### Bugs

Player position is not accounted when issuing tile for spawning of obstacles and collectibles. Which might result is objects spawning into player.

Collectible type to spawn is choosen randomly which might result in prolonged period of same type of collectibles spawning, forcing player to accept negative score.








