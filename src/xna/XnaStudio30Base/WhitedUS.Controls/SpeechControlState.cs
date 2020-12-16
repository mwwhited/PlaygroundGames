using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Speech.Recognition;

namespace WhitedUS.Controls
{
    public struct SpeechControlState
    {
        public SpeechControlState(GamePadDPad emulateDPad, GamePadButtons emulateButtons)
        {
            _emulateDPad = emulateDPad;
            _emulateButtons = emulateButtons;
        }

        public static bool operator !=(SpeechControlState left, SpeechControlState right)
        {
            return !(left._emulateButtons == right._emulateButtons && left._emulateDPad == right._emulateDPad);
        }

        public static bool operator ==(SpeechControlState left, SpeechControlState right)
        {
            return left._emulateButtons == right._emulateButtons && left._emulateDPad == right._emulateDPad;
        }

        //public GamePadThumbSticks ThumbSticks { get; }
        //public GamePadTriggers Triggers { get; }

        //public override bool Equals(object obj);
        //public override int GetHashCode();
        //public bool IsButtonDown(Buttons button);
        //public bool IsButtonUp(Buttons button);
        //public override string ToString();

        private GamePadDPad _emulateDPad;
        public GamePadDPad EmulateDPad { get { return _emulateDPad; } }

        private GamePadButtons _emulateButtons;
        public GamePadButtons EmulateButtons { get { return _emulateButtons; } }

        //public bool IsConnected { get; }
        //public int PacketNumber { get; }

    }
}
