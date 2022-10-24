# Crafting Katas
Example code (in the form of katas) we created to discuss on the podcast.

## Conway's Game of Life

Given a 2-dimensional grid, each position on the grid represents a cell.
Each cell is either alive or dead and has eight neighbors (the surrounding grid positions).

At each transition (a _tick_ or next generation), each cell follows these rules:

1. A live cell will die if it has less than 2 living neighbors.
2. A live cell will continue to live if it has 2 or 3 living neighbors.
3. A live cell will die if it has more than 3 living neighbors.
4. A dead cell will be brought to life if it has exactly 3 living neighbors.

Learn more on [Wikipedia](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).
