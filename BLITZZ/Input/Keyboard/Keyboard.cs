using BLITZZ.Native.SDL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BLITZZ.Input
{
    public static class Keyboard
    {
        private static readonly Dictionary<KeyCode, bool> _keyCodeStates;
        private static readonly Dictionary<ScanCode, bool> _scanCodeStates;

        private static readonly TextInputEventArgs _textInputEventArgs;

        public static event Action<KeyEventArgs> KeyPressed;
        public static event Action<KeyEventArgs> KeyReleased;
        public static event Action<TextInputEventArgs> TextInput;

        static Keyboard()
        {
            _keyCodeStates = new Dictionary<KeyCode, bool>();
            _scanCodeStates = new Dictionary<ScanCode, bool>();

            _textInputEventArgs = new TextInputEventArgs(string.Empty);
        }

        public static bool IsKeyDown(KeyCode keyCode)
        {
            if (_keyCodeStates.TryGetValue(keyCode, out var down))
            {
                return down;
            }

            return false;
        }

        public static bool IsKeyDown(ScanCode scanCode)
        {
            if (_scanCodeStates.TryGetValue(scanCode, out var down))
            {
                return down;
            }

            return false;
        }

        public static bool IsKeyUp(KeyCode keyCode) => !IsKeyDown(keyCode);
        public static bool IsKeyUp(ScanCode scanCode) => !IsKeyDown(scanCode);

        internal static void ProcessKeyEvent(SDL2.SDL_Event ev)
        {
            var eventArgs = new KeyEventArgs(
                (ScanCode)ev.key.keysym.scancode,
                (KeyCode)ev.key.keysym.sym,
                (KeyModifiers)ev.key.keysym.mod,
                ev.key.repeat != 0
            );

            switch (ev.type)
            {
                case SDL2.SDL_EventType.SDL_KEYDOWN:
                    OnKeyPressed(ref eventArgs);
                    break;
                case SDL2.SDL_EventType.SDL_KEYUP:
                    OnKeyReleased(ref eventArgs);
                    break;
            }
        }

        private static void OnKeyPressed(ref KeyEventArgs ev)
        {
            _keyCodeStates[ev.KeyCode] = true;
            _scanCodeStates[ev.ScanCode] = true;

            KeyPressed?.Invoke(ev);
        }

        private static void OnKeyReleased(ref KeyEventArgs ev)
        {
            _keyCodeStates[ev.KeyCode] = false;
            _scanCodeStates[ev.ScanCode] = false;

            KeyReleased?.Invoke(ev);
        }

        internal static unsafe void ProcessTextInputEvent(SDL2.SDL_Event ev)
        {
            if (TextInput == null)
            {
                return;
            }

            string textInput;
            unsafe
            {
                textInput = Marshal.PtrToStringUTF8(
                    new IntPtr(ev.text.text)
                );
            }

            _textInputEventArgs.Text = textInput;

            TextInput.Invoke(_textInputEventArgs);
        }
    }
}
