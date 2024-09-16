# Guessing Coordinate Game

https://github.com/user-attachments/assets/4a4ab0a4-1a7b-4fd6-879d-854575d8d9e5

### How to play
In this game, a player finds the goal point the computer chose randomly by selecting a direction using arrow keys up, bottom, left, and right. For each game, the computer chooses a different location representing the x and y coordinates as a goal point (maybe around 6×6 square). The up key represents +1 for the y coordinate, the bottom does -1 for y, the right does +1 for the x, and the left does -1 for the x.

### Environment
Every step a player selects a direction, the player might encounter a monster. The percentage of win or lose for a monster is 50% and the player can select to fight it or run away. If the player selects "fight" and in case the player wins the monster, the player can get a hint for the recommended direction to the next step, in case he loses, the player goes back previous point. If the player selects "run away," the player goes to the direction changed x(y) into y(x) the player selected. 
ex) In case a player selects up and "run away", the player moves to +1 for x instead of +1 for y.

### Win or lose
When a player loses to a monster 3 times in a row, the game is over and when a player reaches the goal point, the player wins!

## Flowchart
![flowchart-guessing-coordinate-game](https://github.com/user-attachments/assets/29e3de28-79ca-4217-b1d0-d48243174ccf)
