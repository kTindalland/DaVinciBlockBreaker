using BlockBreaker.Renderables;
using BlockBreaker.Renderables.MenuOption;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;
using System;
using System.Collections.Generic;

namespace BlockBreaker.MainMenu
{
    public class MenuPortrait : Portrait
    {
        private AsciiTitle _title; // The ascii title
        private System.Timers.Timer _colTimer; // The colour timer
        private bool _colFlip; // The bool used to flip the colour
        private int _selectedItem;
        private List<MenuOption> _options;

        public MenuPortrait()
        {
            _register = new Register(); // Create a new register
            _options = new List<MenuOption>();

            FillRegister(); // Fill the register
            CreateColTimer(); // Create the colour timer
        }

        public override void SelectThisPortrait()
        {
            base.SelectThisPortrait();
            _colTimer.Start(); // Start the timer when this portrait is selected
        }

        public override void DeSelectThisPortrait()
        {
            base.DeSelectThisPortrait();
            _colTimer.Stop(); // Stop the timer when this portrait is de-selected
        }

        public void FillRegister()
        {
            // Create and register the title
            _title = new AsciiTitle(ConsoleColor.Cyan, new double[] { (Console.WindowWidth / 2) - (36), 3 });
            _register.RegisterItem(_title);

            RegisterOptions(); // Register the menu options

            // Register Credit
            var credit = "Created by Kai Tindall. :]";
            _register.RegisterItem(new Text(credit, ConsoleColor.White, new double[] { Console.WindowWidth - credit.Length - 1, Console.WindowHeight - 1 }));
        }

        private void RegisterOptions()
        {
            var optionTitles = new[] // The different options availible
            {
                "Play Game",
                "Options",
                "Highscores",
                "Exit"
            };

            var maxLen = 0; // Reset the max length
            foreach (var option in optionTitles) // Go through the option names and see which is longest
            {
                if (maxLen < option.Length)
                    maxLen = option.Length;
            }

            for (int i = 0; i < optionTitles.Length; i++) // Create a renderable menu option for each option string
            {
                _options.Add(new MenuOption(optionTitles[i], ConsoleColor.White, new double[] { 1, 15 + i }));
            }



            foreach (var option in _options) // For each renderable option set the max length and register it
            {
                option.SetMaxLen(maxLen);
                _register.RegisterItem(option);
            }

            _options[_selectedItem].Select(); // Set the selected flag for the current selected item
        }

        private void ChangeSelected(int change)
        {
            _options[_selectedItem].DeSelect(); // Deselect the last item

            _selectedItem += change; // Change the selected item index by the correct amount

            if (_selectedItem >= _options.Count) // Validate the selected item index
                _selectedItem = 0;

            if (_selectedItem < 0)
                _selectedItem = _options.Count - 1;

            _options[_selectedItem].Select(); // Set the selected flag for the new selected item
        }

        private void SelectOption(Renderables.MenuOption.OptionName selectedOption) // Switch the 
        {
            switch (selectedOption) // Switch on the selected options
            {
                case Renderables.MenuOption.OptionName.Exit: // If Exit
                    Environment.Exit(0); // Then exit
                    break;

                case Renderables.MenuOption.OptionName.Highscores: // If highscores
                    _manager.SwitchCurrentPortrait((int)PageHandles.Highscores); // Switch to highscores portrait
                    break;

                case Renderables.MenuOption.OptionName.Options: // If options
                    _manager.SwitchCurrentPortrait((int)PageHandles.Options); // Switch to options portrait
                    break;

                case Renderables.MenuOption.OptionName.Play: // If play
                    _manager.SwitchCurrentPortrait((int)PageHandles.GiveName); // Switch to the give user name portrait
                    break;
            }
        }

        /// <summary>
        /// Creates the timer that switches the colours
        /// </summary>
        public void CreateColTimer()
        {
            _colTimer = new System.Timers.Timer(2000); // Timer every 2 seconds
            _colTimer.AutoReset = true; // Keeps going
            _colTimer.Enabled = true; // Make it publish events
            _colTimer.Elapsed += OnTimedEvent; // subscribe the event
        }


        public void OnTimedEvent(object source, EventArgs args)
        {
            if (_colFlip) // Flip between Cyan and Yellow
                _title.ChangeTextColor(ConsoleColor.Cyan);
            else
                _title.ChangeTextColor(ConsoleColor.Yellow);

            _colFlip = !_colFlip; // Flip the flip
        }

        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active) // Only when active
            {
                switch (args.KeyPressed) // Switch on what key is pressed
                {
                    case ConsoleKey.UpArrow: // On up, decrease the selected index by 1 and beep
                        ChangeSelected(-1);
                        Console.Beep();
                        break;

                    case ConsoleKey.DownArrow: // On down, increase the selected index by 1 and beep
                        ChangeSelected(1);
                        Console.Beep();
                        break;

                    case ConsoleKey.Enter: // On enter, get the selected option and change to the correct portrait
                        var selectedOption = _options[_selectedItem].getOptionName();
                        SelectOption(selectedOption);
                        break;
                }
            }

        }
    }
}
