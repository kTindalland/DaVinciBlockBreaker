using System;
using System.Text.RegularExpressions;
using BlockBreaker.Highscores.Resources;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.EnterName
{
    public class EnterNamePortrait : Portrait
    {
        private Text[] _username; // The list of text objects that will make up the username
        private int _currentPosition; // The current position of the cursor in the username
        private int _amountOfLetters; // The amount of letters the user can utilise

        public EnterNamePortrait()
        {
            _amountOfLetters = 10;
            _currentPosition = 0;

            _username = new Text[_amountOfLetters]; // Create a list of text objects that will become the username, each text object will be a single letter
            _register = new Register();


            FillRegister();
        }

        private void Reset()
        {
            _currentPosition = 0; // Put the position back to zero
            FillRegister(); // Recreate the register with new elements
        }

        public override void SelectThisPortrait()
        {
            base.SelectThisPortrait();

            if (GetName().Length > 0) // If this portrait has already been accessed and used
                Reset(); // Reset the portrait
        }

        private void FillRegister()
        {
            // Register the characters
            var spacing = 3; // Spacing multiplier for the characters
            var leftPad = (Console.WindowWidth / 2) - (spacing * (_amountOfLetters / 2)); // The amount from the left the characters are
            var topPad = (Console.WindowHeight / 2) - 1; // The amount from the top the characters are

            for (int i = 0; i < _amountOfLetters; i++) // Go through and register each character
            {
                _username[i] = new Text("-", ConsoleColor.White, new double[] { (spacing * i) + leftPad, topPad });
            }

            for (int i = 0; i < _amountOfLetters; i++) // Go through and register each character
            {
                _register.RegisterItem(_username[i]);
            }

            // Register title
            var title = "Please enter a username:";
            _register.RegisterItem(new Text(title, ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (title.Length / 2), 5 }));

            // Register info
            var info = "Press ESCAPE to return to the main menu";
            _register.RegisterItem(new Text(info, ConsoleColor.White, new double[] { 0, Console.WindowHeight - 2 }));

            var info2 = "Press ENTER to continue to main game once you enter a username";
            _register.RegisterItem(new Text(info2, ConsoleColor.White, new double[] { 0, Console.WindowHeight - 1 }));
        }

        private void IncreasePosition()
        {
            _currentPosition++; // Increase position by one

            if (_currentPosition > (_amountOfLetters)) // Validate the position
                _currentPosition = (_amountOfLetters);
        }

        private void DecreasePosition()
        {
            _currentPosition--; // Decrease position by one

            if (_currentPosition < 0) // Validate position
                _currentPosition = 0;
        }

        private string GetName()
        {
            var name = ""; // Create a blank name
            string currentLetter;

            for (int i = 0; i < _amountOfLetters; i++)
            {
                currentLetter = _username[i].ReturnText();

                if (currentLetter == "-") // Check there is a letter there
                    currentLetter = ""; // Ignore it if there isn't

                name += currentLetter; // Add current letter to name
            }

            return name;
        }

        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$"); // Regular expression for alphanumeric characters
                var input = args.KeyPressed.ToString(); // The string of the input

                if (input.Length == 2 && input[0] == 'D') // Check if it is a number (Numbers come in from the keypress as D1, D2, etc.)
                {
                    input = input.Substring(1, 1); // Get the number out of it
                }

                if (alphanumeric.IsMatch(input) && input.Length == 1 && _currentPosition < (_amountOfLetters)) // Check it's alphanumeric and only one length (length prevents words like SPACEBAR being put into a single character)
                {
                    _username[_currentPosition].ChangeText(input); // Change the text
                    _username[_currentPosition].ChangeTextColor(ConsoleColor.Cyan); // Change the color
                    IncreasePosition(); // Increase the position
                }
                else if (args.KeyPressed == ConsoleKey.Backspace) // If the user presses backspace
                {
                    DecreasePosition(); // Decrease the position
                    _username[_currentPosition].ChangeText("-"); // Replace the character with an empty space
                    _username[_currentPosition].ChangeTextColor(ConsoleColor.White); // Change the color to white

                }


                if (args.KeyPressed == ConsoleKey.Enter && (GetName().Length > 0))
                {
                    ResourceManager.PlayerDetails = new Score(GetName(), 0);
                    Console.Title = "Block Breaker - " + GetName() + "'s Game";
                    _manager.SwitchCurrentPortrait((int)PageHandles.MainGame);
                }


                if (args.KeyPressed == ConsoleKey.Escape) // Go back to the main menu if escape has been pressed
                    _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu);
            }

        }
    }
}
