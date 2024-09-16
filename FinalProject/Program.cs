using System;
using System.Dynamic;

namespace FinalProject
{
    class GuessingCoordinateGame
    {
        // declare variables
        int goalX;
        int goalY;
        int playerLocationX;
        int playerLocationY;
        int initial = 0; // the number for switching starting message 
        int hitPoints = 3;
        int monster = 0;
        bool isAchieved = false;
        bool isOver = false;

        // arrays for showing messages
        string[] introductionMessage = new string[] {
            "<< Introduction >>",
            "1. Player starts from the coordinate (x,y) = (0,0) point.",
            "2. Player finds the goal point located from (-3,-3) to (3,3) except from (-1,-1) to (1,1).",
            "3. Player can select a direction each turn and might encounter a monster on every step.",
            "4. If player meets the monster, select to 'fight' or 'avoid'.(Winning rate is 1/2)",
            "   Fight:",
            "   - When player wins, player can move to the direction chosen and get a hint!",
            "   - When player loses, player cannot move.",
            "   Avoid: player move to the direction changed x(y) into y(x) chose.",
            "5. When player reaches the goal, the game finish",
            "6. If player loses 3 times to monsters, the game is over.",
            "",
            "GOOD LUCK!!",
            "",
            "Please press any keys to start."
            };
        string[][] startMessages = new string[][] {
            new string []{
                "Please enter the direction you want to go using the keyboard:",
                "'U' for Up",
                "'D' for Down",
                "'R' for Right",
                "'L' for Left"
                },
            new string []{
                    "Please enter the direction as 'U', 'D', 'R', 'L'.",
                }
            };

        string[][] monsterMessages = new string[][] {
            new string[] {
            "Monster appears!",
            "Do you fight or avoid?",
            "Please press 'Y' for fight or 'N' for avoid."
             },
             new string[] {"You won the monster!"},
             new string[] {"You lost to the monster"},
             new string[] {"You run away from the monster"},
        };
        string[] confirmationMessage = new string[] {
            "Finish the game: please press 'E'.",
            "Play again: please press any other keys."
        };
        string[] thankYouMessage = new string[] { "Thank you for playing. See you soon!" };
        string[][] errorMessages = new string[][] {
             new string[] { "*** Please enter ONLY 'U', 'D', 'L', or 'R' ***" },
             new string[] { "*** Please enter ONLY 'Y' or 'N' ***" },
             new string[] { "*** You are out of the goal point area! ***" }
        };
        string[] completedMessage = new string[] { "Congratulation!", "You achieved the goal" };
        string[] gameOverMessage = new string[] { "Sorry, the game is over..." };

        // constructor
        public GuessingCoordinateGame()
        {
            // set a new goal point
            bool isValid = false;
            int[] invalidRange = new int[] { -1, 0, 1 };
            while (!isValid) // not to include the area from (-1, -1) to (1, 1) in the goal
            {
                bool isInvalid = false;
                int x = getRandomNum(-3, 4);
                int y = getRandomNum(-3, 4);
                for (int i = 0; i < invalidRange.Length; i++)
                {
                    if (invalidRange[i] == x)
                    {
                        for (int j = 0; j < invalidRange.Length; j++)
                        {
                            if (invalidRange[j] == y)
                            {
                                isInvalid = true;
                            }
                        }
                    }
                }
                if (!isInvalid)
                {
                    this.goalX = x;
                    this.goalY = y;
                }
                isValid = !isInvalid;
            }
        }

        // the function to get a random number
        static int getRandomNum(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max); // the number min <= i < max
        }

        // the function to get player input (changed to Upper-case)
        static string getPlayerInput()
        {
            string input = Console.ReadLine() ?? "";
            return input.ToUpper(); ;
        }

        // the function to show console messages
        public void showMessages(string[] messages)
        {
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }

        // the function to set player location
        public void setPlayerLocation(string direction)
        {
            switch (direction)
            {
                case "U":
                    this.playerLocationY += 1;
                    break;
                case "D":
                    this.playerLocationY -= 1;
                    break;
                case "R":
                    this.playerLocationX += 1;
                    break;
                case "L":
                    this.playerLocationX -= 1;
                    break;
            }

        }

        // the function to show a hint for player for the next step
        public void showHint()
        {
            string hintX = "Right";
            string hintY = "Up";
            string[] message = new string[] { "** Hint for the next step **", "", "" };
            int subX = this.goalX - this.playerLocationX;
            int subY = this.goalY - this.playerLocationY;
            // if subX==0 && subY==0 -> reach goal -> do nothing 
            if (subX != 0 || subY != 0)
            {
                // check the recommend direction
                if (subX < 0) hintX = "Left";
                if (subY < 0) hintY = "Down";
                // hint message
                if (subX == 0) message[1] = "Y direction: " + hintY;
                else if (subY == 0) message[1] = "X direction: " + hintX;
                else
                {
                    message[1] = "X direction: " + hintX;
                    message[2] = "Y direction: " + hintY;
                }
                this.showMessages(message);
            }
        }

        // the function to move player's location to where changed x(y) into y(x)
        public void movePlayerLocation()
        {
            int currentX = this.playerLocationX;
            int currentY = this.playerLocationY;
            this.playerLocationX = currentY;
            this.playerLocationY = currentX;
        }

        // the function to check if the player reaches the goal
        public void checkPlayerLocation()
        {
            // check if the player reach the goal
            if (this.playerLocationX == this.goalX && this.playerLocationY == this.goalY)
            { // reach the goal
                this.isAchieved = true;
                this.showMessages(this.completedMessage);
                return;
            }
            bool isOutX = this.playerLocationX > 3 || this.playerLocationX < -3;
            bool isOutY = this.playerLocationY > 3 || this.playerLocationY < -3;
            // check if the player is out of the goal area
            if (isOutX || isOutY)
            {
                this.showMessages(this.errorMessages[2]);
            }
        }

        // the function to check if the player reaches the goal
        public void showPlayerStatus()
        {
            Console.WriteLine("HP: " + this.hitPoints);
            Console.WriteLine("Current location: (" + this.playerLocationX + ", " + this.playerLocationY + ")");
        }
        static void Main()
        {
            // declare game status
            bool playGame = true;
            // create the class instance
            GuessingCoordinateGame game = new GuessingCoordinateGame();
            // show the introduction message
            game.showMessages(game.introductionMessage);
            Console.ReadKey();

            while (playGame) // game start
            {
                // show starting message
                game.showMessages(game.startMessages[game.initial]);
                if (game.initial == 0) game.initial = 1;

                // check player input 
                string playerInput;
                string direction = getPlayerInput();
                while (direction != "U" && direction != "D" && direction != "L" && direction != "R")
                {
                    // show error message when player press unexpected keys
                    game.showMessages(game.errorMessages[0]);
                    direction = getPlayerInput();
                }
                // check if a monster exists
                game.monster = getRandomNum(0, 3);
                // in case of monster exists
                if (game.monster > 1)
                {
                    // let player select to fight or avoid
                    game.showMessages(game.monsterMessages[0]);
                    playerInput = getPlayerInput();
                    if (playerInput == "Y")
                    {
                        // fight
                        // get 0 or 1 and getting 1 is a win
                        int result = getRandomNum(0, 2);
                        if (result == 1)
                        { // win
                            game.showMessages(game.monsterMessages[1]);
                            // set player location
                            game.setPlayerLocation(direction);
                            // show the hint for the next direction
                            game.showHint();
                        }
                        else
                        { // lose
                            game.showMessages(game.monsterMessages[2]);
                            // minus player's hit points
                            game.hitPoints--;
                            if (game.hitPoints == 0)
                            {
                                // Game is Over
                                game.isOver = true;
                                game.showMessages(game.gameOverMessage);
                            }
                        }
                    }
                    else if (playerInput == "N")
                    {
                        // avoid  
                        game.showMessages(game.monsterMessages[3]);
                        // move to player
                        game.movePlayerLocation();
                    }
                    else
                    {
                        while (playerInput != "Y" && playerInput != "N")
                        {
                            // show error message
                            game.showMessages(game.errorMessages[1]);
                            playerInput = getPlayerInput();
                        }
                    }
                }
                else
                {
                    // set player location
                    game.setPlayerLocation(direction);

                }
                // check player location
                game.checkPlayerLocation();
                if (game.isAchieved || game.isOver)
                {
                    game.showMessages(game.confirmationMessage);
                    string answer = getPlayerInput();
                    // if player select not continue, the game will end
                    if (answer == "E") playGame = false;
                    else game = new GuessingCoordinateGame();
                }
                else
                {
                    game.showPlayerStatus();
                }
            }
            // end the game
            game.showMessages(game.thankYouMessage);
            // wait for player action
            Console.ReadKey();
        }
    }
}