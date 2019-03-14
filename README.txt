Hey Tom,
So this is my Tetris solution.
In essence I used a two dimensional array to keep track of all blocks.
Each random block type is a pattern, which keeps unique color identifiers based on a
random number generated. This number is then concatenated onto the end of the ID number
given to the pattern. Then the Pattern ID becomes this new concatenated number, this way
any blocks associated with the pattern will have a Parent ID that connects them to their
buddy blocks. This way if two squares fall, they will function properly with each other since
they have unique identifiers. I chose to do the game board this way with the idea of saving
and loading in mind, because I knew for a functional game to load I would have to create not only
the same board the user left, but also the same patterns that were left as well. And these patterns
could be manipulated from their base spawn in version. As such, this worked out quite well by just %10
at the end of whatever was written into the game board spot to find out the color, and divide by 10 to
discover the original id if necessary.

The rest of the Tetris game was figuring out how to adjust each block dynamically, which came together in
the end.

There are two points I wanted to mention to you about the game beforehand, one of which was a design choice.
This design choise was to, instead of having a File -> New game area within my menu of the game,
I merely turn off the game timer, and display a game over visual that has the user decide whether they
want to continue. In this time, pause and resume are still nonfunctional as they should be.

Secondly, I have ocassionally gotten the Classic version of the game to break, (not throw an exception or
crash, but just not do row deletion properly), where sometimes a singular block wont fall. I have
very rarely seen this problem, and couldn't for the life of me figure out what was causing it. 
Other than that, I believe I completed everything in the Assignment specifications.

I tried and failed MISERABLY at making a 3D intro canvas of Tetris. You can
look at the code if you want, but the falling blocks are jenky at best.

As for extra credit; I did implement both "naive" gravity - which is when the blocks only fall an equal distance
to however many rows were completed below them rather than fall the complete way. I also implemented
the full gravity effect within cascade mode that will allow blocks to full the entire way, so long as
their pattern allows for such falling.