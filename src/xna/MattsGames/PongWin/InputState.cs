#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace PongWin
{
    /// <summary>
    /// Helper for reading input from keyboard and gamepad. This class tracks both
    /// the current and previous state of both input devices, and implements query
    /// properties for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];
        }


        #endregion

        #region Properties
        
        public bool MenuUp
        {
            get
            {
                return IsNewKeyPress(Keys.Up) ||
                       IsNewButtonPress(Buttons.DPadUp) ||
                       IsNewButtonPress(Buttons.LeftThumbstickUp);
            }
        }

        public bool MenuDown
        {
            get
            {
                return IsNewKeyPress(Keys.Down) ||
                       IsNewButtonPress(Buttons.DPadDown) ||
                       IsNewButtonPress(Buttons.LeftThumbstickDown);
            }
        }
        
        public bool MenuSelect
        {
            get
            {
                return IsNewKeyPress(Keys.Space) ||
                       IsNewKeyPress(Keys.Enter) ||
                       IsNewButtonPress(Buttons.A) ||
                       IsNewButtonPress(Buttons.Start);
            }
        }
        
        public bool MenuCancel
        {
            get
            {
                return IsNewKeyPress(Keys.Escape) ||
                       IsNewButtonPress(Buttons.B) ||
                       IsNewButtonPress(Buttons.Back);
            }
        }

        public bool PauseGame
        {
            get
            {
                return IsNewKeyPress(Keys.Escape) ||
                       IsNewButtonPress(Buttons.Back) ||
                       IsNewButtonPress(Buttons.Start);
            }
        }

        public bool DirectionUp
        {
            get
            {
                return  IsNewButtonPress(Buttons.DPadUp, PlayerIndex.One) ||
                        IsNewButtonPress(Buttons.RightThumbstickUp, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.Up);
            }
        }

        public bool DirectionDown
        {
            get
            {
                return  IsNewButtonPress(Buttons.DPadDown, PlayerIndex.One) ||
                        IsNewButtonPress(Buttons.RightThumbstickDown, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.Down);
            }
        }

        public bool DirectionLeft
        {
            get
            {
                return  IsNewButtonPress(Buttons.DPadLeft, PlayerIndex.One) ||
                        IsNewButtonPress(Buttons.RightThumbstickLeft, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.Left);
            }
        }

        public bool DirectionRight
        {
            get
            {
                return  IsNewButtonPress(Buttons.DPadRight, PlayerIndex.One) ||
                        IsNewButtonPress(Buttons.RightThumbstickDown, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.Right);
            }
        }

        public bool DirectionClick
        {
            get
            {
                return IsNewButtonPress(Buttons.RightStick, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.End);
            }
        }

        public bool OtherDirectionUp
        {
            get
            {
                return  IsNewButtonPress(Buttons.LeftThumbstickUp, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.W);
            }
        }

        public bool OtherDirectionDown
        {
            get
            {
                return  IsNewButtonPress(Buttons.LeftThumbstickDown, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.S);
            }
        }

        public bool OtherDirectionLeft
        {
            get
            {
                return  IsNewButtonPress(Buttons.LeftThumbstickLeft, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.A);
            }
        }

        public bool OtherDirectionRight
        {
            get
            {
                return  IsNewButtonPress(Buttons.LeftThumbstickDown, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.D);
            }
        }

        public bool OtherDirectionClick
        {
            get
            {
                return  IsNewButtonPress(Buttons.LeftStick, PlayerIndex.One) ||
                        IsNewKeyPress(Keys.Q);
            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by any player.
        /// </summary>
        public bool IsNewKeyPress(Keys key)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                if (IsNewKeyPress(key, (PlayerIndex)i))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by the specified player.
        /// </summary>
        public bool IsNewKeyPress(Keys key, PlayerIndex playerIndex)
        {
            return (CurrentKeyboardStates[(int)playerIndex].IsKeyDown(key) &&
                    LastKeyboardStates[(int)playerIndex].IsKeyUp(key));
        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update,
        /// by any player.
        /// </summary>
        public bool IsNewButtonPress(Buttons button)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                if (IsNewButtonPress(button, (PlayerIndex)i))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update,
        /// by the specified player.
        /// </summary>
        public bool IsNewButtonPress(Buttons button, PlayerIndex playerIndex)
        {
            return (CurrentGamePadStates[(int)playerIndex].IsButtonDown(button) &&
                    LastGamePadStates[(int)playerIndex].IsButtonUp(button));
        }


        /// <summary>
        /// Checks for a "menu select" input action from the specified player.
        /// </summary>
        public bool IsMenuSelect(PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, playerIndex) ||
                   IsNewKeyPress(Keys.Enter, playerIndex) ||
                   IsNewButtonPress(Buttons.A, playerIndex) ||
                   IsNewButtonPress(Buttons.Start, playerIndex);
        }


        /// <summary>
        /// Checks for a "menu cancel" input action from the specified player.
        /// </summary>
        public bool IsMenuCancel(PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, playerIndex) ||
                   IsNewButtonPress(Buttons.B, playerIndex) ||
                   IsNewButtonPress(Buttons.Back, playerIndex);
        }


        #endregion
    }
}
