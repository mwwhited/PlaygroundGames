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

#if !ZUNE
        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly KeyboardState[] LastKeyboardStates;
#endif

        public readonly GamePadState[] CurrentGamePadStates;
        public readonly GamePadState[] LastGamePadStates;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
#if !ZUNE
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            LastKeyboardStates = new KeyboardState[MaxInputs];
#endif

            CurrentGamePadStates = new GamePadState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];
        }


        #endregion

        #region Properties

        public bool MenuUp
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadUp) ||
#if !ZUNE
                        IsNewKeyPress(Keys.Up) ||
#endif
 IsNewButtonPress(Buttons.LeftThumbstickUp);
            }
        }

        public bool MenuDown
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadDown) ||
#if !ZUNE
                        IsNewKeyPress(Keys.Down) ||
#endif
 IsNewButtonPress(Buttons.LeftThumbstickDown);
            }
        }

        public bool MenuSelect
        {
            get
            {
                return IsNewButtonPress(Buttons.A) ||
#if !ZUNE
                        IsNewKeyPress(Keys.Space) ||
                       IsNewKeyPress(Keys.Enter) ||
#endif
 IsNewButtonPress(Buttons.A) ||
                       IsNewButtonPress(Buttons.Start);
            }
        }

        public bool MenuCancel
        {
            get
            {
                return IsNewButtonPress(Buttons.B) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Escape) ||
#endif
 IsNewButtonPress(Buttons.Back);
            }
        }

        public bool PauseGame
        {
            get
            {
                return IsNewButtonPress(Buttons.Back) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Escape) ||
#endif
 IsNewButtonPress(Buttons.Start);
            }
        }

        public bool DirectionUp
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadUp, PlayerIndex.One) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Up) ||
#endif
 IsNewButtonPress(Buttons.RightThumbstickUp, PlayerIndex.One);
            }
        }

        public bool DirectionDown
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadDown, PlayerIndex.One) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Down) ||
#endif
 IsNewButtonPress(Buttons.RightThumbstickDown, PlayerIndex.One);
            }
        }

        public bool DirectionLeft
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadLeft, PlayerIndex.One) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Left) ||
#endif
 IsNewButtonPress(Buttons.RightThumbstickLeft, PlayerIndex.One);
            }
        }

        public bool DirectionRight
        {
            get
            {
                return IsNewButtonPress(Buttons.DPadRight, PlayerIndex.One) ||
#if !ZUNE
                    IsNewKeyPress(Keys.Right) ||
#endif
 IsNewButtonPress(Buttons.RightThumbstickDown, PlayerIndex.One);
            }
        }

        public bool DirectionClick
        {
            get
            {
                return IsNewButtonPress(Buttons.RightStick, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.End);
#else
;
#endif
            }
        }

        public bool OtherDirectionUp
        {
            get
            {
                return IsNewButtonPress(Buttons.LeftThumbstickUp, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.W);
#else
;
#endif
            }
        }

        public bool OtherDirectionDown
        {
            get
            {
                return IsNewButtonPress(Buttons.LeftThumbstickDown, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.S);
#else
;
#endif
            }
        }

        public bool OtherDirectionLeft
        {
            get
            {
                return IsNewButtonPress(Buttons.LeftThumbstickLeft, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.A);
#else
;
#endif
            }
        }

        public bool OtherDirectionRight
        {
            get
            {
                return IsNewButtonPress(Buttons.LeftThumbstickDown, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.D);
#else
;
#endif
            }
        }

        public bool OtherDirectionClick
        {
            get
            {
                return IsNewButtonPress(Buttons.LeftStick, PlayerIndex.One)
#if !ZUNE
                         || IsNewKeyPress(Keys.Q);
#else
;
#endif
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
#if !ZUNE
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
#endif

                LastGamePadStates[i] = CurrentGamePadStates[i];
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
        }

#if !ZUNE
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
#endif

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
            return IsNewButtonPress(Buttons.A, playerIndex) || 
#if !ZUNE
                IsNewKeyPress(Keys.Space, playerIndex) ||
                   IsNewKeyPress(Keys.Enter, playerIndex) ||
#endif  
                   IsNewButtonPress(Buttons.Start, playerIndex);
        }


        /// <summary>
        /// Checks for a "menu cancel" input action from the specified player.
        /// </summary>
        public bool IsMenuCancel(PlayerIndex playerIndex)
        {
            return IsNewButtonPress(Buttons.B, playerIndex) ||
#if !ZUNE
                IsNewKeyPress(Keys.Escape, playerIndex) ||
#endif          
                   IsNewButtonPress(Buttons.Back, playerIndex);
        }


        #endregion
    }
}
