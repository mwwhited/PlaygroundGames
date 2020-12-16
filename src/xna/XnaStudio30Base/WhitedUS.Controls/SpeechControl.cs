using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WhitedUS.Controls
{
    public class SpeechControl : IDisposable
    {
        private volatile static Dictionary<PlayerIndex, SpeechControl> _speechControls = new Dictionary<PlayerIndex, SpeechControl>();

        private SpeechControlState _currentState;
        private SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "up":
                    _currentState = new SpeechControlState(new GamePadDPad(
                        ButtonState.Pressed,
                        ButtonState.Released,
                        ButtonState.Released,
                        ButtonState.Released
                        ), new GamePadButtons());
                    break;

                case "down":
                    _currentState = new SpeechControlState(new GamePadDPad(
                        ButtonState.Released,
                        ButtonState.Pressed,
                        ButtonState.Released,
                        ButtonState.Released
                        ), new GamePadButtons());
                    break;

                case "left":
                    _currentState = new SpeechControlState(new GamePadDPad(
                        ButtonState.Released,
                        ButtonState.Released,
                        ButtonState.Pressed,
                        ButtonState.Released
                        ), new GamePadButtons());
                    break;


                case "right":
                    _currentState = new SpeechControlState(new GamePadDPad(
                        ButtonState.Released,
                        ButtonState.Released,
                        ButtonState.Released,
                        ButtonState.Pressed
                        ), new GamePadButtons());
                    break;

                case "x":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.X));
                    break;

                case "y":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.Y));
                    break;

                case "a":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.A));
                    break;

                case "b":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.B));
                    break;

                case "back":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.Back));
                    break;

                case "start":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.Start));
                    break;

                case "left shoulder":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.LeftShoulder));
                    break;

                case "right shoulder":
                    _currentState = new SpeechControlState(new GamePadDPad(), new GamePadButtons(Buttons.RightShoulder));
                    break;

                default:
                    break;
            }
        }

        public static SpeechControlState GetState(PlayerIndex playerIndex)
        {
            if (!_speechControls.ContainsKey(playerIndex))
            {
                _speechControls.Add(playerIndex, new SpeechControl());

                var currentControl = _speechControls[playerIndex];

                currentControl.sre.SetInputToDefaultAudioDevice();
                currentControl.sre.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(
                    "up", "down", "left", "right",
                    "x", "y", "a", "b",
                    "back", "start",
                    "left sholder", "right shoulder"
                    ))));
                currentControl.sre.SpeechRecognized += currentControl.sre_SpeechRecognized;
                currentControl.sre.RecognizeAsync(RecognizeMode.Multiple);
            }

            return _speechControls[playerIndex]._currentState;
        }

        #region IDisposable Members

        ~SpeechControl()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (sre != null)
            {
                sre.Dispose();
                sre = null;
            }
        }

        #endregion
    }
}
