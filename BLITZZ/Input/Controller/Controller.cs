using BLITZZ.Logging;
using System;
using static BLITZZ.Native.SDL.SDL2;

namespace BLITZZ.Input
{
    public static class Controller
    {

        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        public static event Action<ControllerEventArgs> Connected;
        public static event Action<ControllerEventArgs> Disconnected;
        public static event Action<ControllerButtonEventArgs> ButtonPressed;
        public static event Action<ControllerButtonEventArgs> ButtonReleased;
        public static event Action<ControllerAxisEventArgs> AxisMoved;

        public static int DeviceCount => ControllerRegistry.Instance.DeviceCount;

        public static void SetDeadZone(int playerIndex, ControllerAxis axis, ushort value)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return;

            controller.DeadZones[axis] = value;
        }

        public static void SetDeadZoneAllAxes(int playerIndex, ushort value)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return;

            controller.DeadZones.Clear();

            SetDeadZone(playerIndex, ControllerAxis.LeftStickX, value);
            SetDeadZone(playerIndex, ControllerAxis.RightStickX, value);
            SetDeadZone(playerIndex, ControllerAxis.LeftStickY, value);
            SetDeadZone(playerIndex, ControllerAxis.RightStickY, value);
            SetDeadZone(playerIndex, ControllerAxis.LeftTrigger, value);
            SetDeadZone(playerIndex, ControllerAxis.RightTrigger, value);
        }

        public static string GetName(int playerIndex)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return null;

            return SDL_GameControllerName(controller.InstancePointer);
        }

        public static short GetAxisValue(int playerIndex, ControllerAxis axis)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return 0;

            var axisValue = SDL_GameControllerGetAxis(
                controller.InstancePointer,
                (SDL_GameControllerAxis)axis
            );

            if (CanIgnoreAxisMotion(playerIndex, axis, axisValue))
                return 0;

            return axisValue;
        }

        private static bool CanIgnoreAxisMotion(int playerIndex, ControllerAxis axis, short axisValue)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return true;

            if (controller.DeadZones.TryGetValue(axis, out ushort value))
            {
                if (Math.Abs((int)axisValue) < value)
                    return true;
            }

            return false;
        }

     

        public static float GetAxisValueNormalized(int playerIndex, ControllerAxis axis)
            => GetAxisValue(playerIndex, axis) / 32768f;

        public static bool IsButtonPressed(int playerIndex, ControllerButton button)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return false;

            return SDL_GameControllerGetButton(
                controller.InstancePointer,
                (SDL_GameControllerButton)button
            ) > 0;
        }

        public static void Vibrate(int playerIndex, ushort lowFreq, ushort highFreq, uint duration)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return;

            SDL_GameControllerRumble(
                controller.InstancePointer,
                lowFreq,
                highFreq,
                duration
            );
        }

        public static BatteryStatus GetBatteryLevel(int playerIndex)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return BatteryStatus.Unknown;

            var joystickInstance = SDL_GameControllerGetJoystick(controller.InstancePointer);
            return (BatteryStatus)SDL_JoystickCurrentPowerLevel(joystickInstance);
        }

        public static string RetrieveMapping(int playerIndex)
        {
            var controller = ControllerRegistry.Instance.GetControllerInfo(playerIndex);

            if (controller == null)
                return null;

            return SDL_GameControllerMapping(controller.InstancePointer);
        }

        public static void AddMapping(string controllerMapping)
        {
            if (string.IsNullOrEmpty(controllerMapping))
            {
                _log.Warning("Tried to add a null or empty controller mapping.");
                return;
            }

            if (SDL_GameControllerAddMapping(controllerMapping) < 0)
                _log.Error($"Failed to add a controller mapping: {SDL_GetError()}.");
        }


        internal static void ProcessControllerConected(SDL_Event ev)
        {
            var instance = SDL_GameControllerOpen(ev.cdevice.which);
            var joyInstance = SDL_GameControllerGetJoystick(instance);
            var instanceId = SDL_JoystickInstanceID(joyInstance);

            var playerIndex = ControllerRegistry.Instance.GetFirstFreePlayerSlot();
            SDL_GameControllerSetPlayerIndex(instance, playerIndex);

            var name = SDL_GameControllerName(instance);

            var guid = SDL_JoystickGetGUID(joyInstance);

            var controllerInfo = new ControllerInfo(instance, instanceId, guid, playerIndex, name);
            ControllerRegistry.Instance.Register(instance, controllerInfo);

            Connected?.Invoke(new ControllerEventArgs(controllerInfo));
        }

        internal static void ProcessControllerDisconnected(SDL_Event ev)
        {
            var instance = SDL_GameControllerFromInstanceID(ev.cdevice.which);
            var controllerInfo = ControllerRegistry.Instance.GetControllerInfoByPointer(instance);

            ControllerRegistry.Instance.Unregister(instance);

            Disconnected?.Invoke(new ControllerEventArgs(controllerInfo));
        }

        internal static void ProcessButtonDown(SDL_Event ev)
        {
            if (ButtonPressed == null)
            {
                return;
            }

            var instance = SDL_GameControllerFromInstanceID(ev.cbutton.which);
            var controller = ControllerRegistry.Instance.GetControllerInfoByPointer(instance);

            var button = (ControllerButton)ev.cbutton.button;

            ButtonPressed.Invoke(new ControllerButtonEventArgs(controller, button));
        }

        internal static void ProcessButtonUp(SDL_Event ev)
        {
            if (ButtonReleased == null)
            {
                return;
            }

            var instance = SDL_GameControllerFromInstanceID(ev.cbutton.which);
            var controller = ControllerRegistry.Instance.GetControllerInfoByPointer(instance);

            var button = (ControllerButton)ev.cbutton.button;

            ButtonReleased.Invoke(new ControllerButtonEventArgs(controller, button));
        }

        internal static void ProcessAxisMotion(SDL_Event ev)
        {
            if (AxisMoved == null)
            {
                return;
            }

            var instance = SDL_GameControllerFromInstanceID(ev.caxis.which);
            var controller = ControllerRegistry.Instance.GetControllerInfoByPointer(instance);

            var axis = (ControllerAxis)ev.caxis.axis;

            AxisMoved.Invoke(new ControllerAxisEventArgs(controller, axis, ev.caxis.axisValue));
        }
    }
}
