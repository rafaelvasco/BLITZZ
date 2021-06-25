using System.Numerics;

namespace BLITZZ.Input
{
    public class MouseMoveEventArgs
    {
        public Vector2 Position { get; internal set; }
        public Vector2 Delta { get; internal set; }

        public MouseButtonState ButtonState { get; internal set; }

        internal MouseMoveEventArgs(Vector2 position, Vector2 delta, MouseButtonState buttonState)
        {
            Position = position;
            Delta = delta;

            ButtonState = buttonState;
        }
    }
}
