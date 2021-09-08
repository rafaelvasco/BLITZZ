using BLITZZ.Logging;
using BLITZZ.Native.SDL;
using System.Collections.Generic;

namespace BLITZZ
{
    internal static class PlatformEvents
    {
        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        internal delegate void PlatformEventHandler(SDL2.SDL_Event ev);
        internal delegate void PlatformWindowEventHandler(SDL2.SDL_WindowEvent ev);

        private static readonly Dictionary<SDL2.SDL_EventType, PlatformEventHandler> _eventHandlers = new();
        private static readonly Dictionary<SDL2.SDL_WindowEventID, PlatformWindowEventHandler> _windowEventHandlers = new();
        private static readonly Dictionary<SDL2.SDL_EventType, bool> _discardedEventTypes = new();

        internal static void Process(SDL2.SDL_Event ev)
        {
            if (_discardedEventTypes.TryGetValue(ev.type, out _))
            {
                return;
            }

            if (ev.type == SDL2.SDL_EventType.SDL_WINDOWEVENT)
            {
                DispatchWindowEvent(ev.window);
                return;
            }

            DispatchEvent(ev);
        }

        public static void On(SDL2.SDL_EventType type, PlatformEventHandler handler)
        {
            if (_eventHandlers.ContainsKey(type))
            {
                _log.Warning($"{type} handler is getting redefined.");
                _eventHandlers[type] = handler;
            }
            else
            {
                _eventHandlers.Add(type, handler);
            }
        }

        public static void On(SDL2.SDL_WindowEventID eventId, PlatformWindowEventHandler handler)
        {
            if (_windowEventHandlers.ContainsKey(eventId))
            {
                _log.Warning($"{eventId} handler is getting redefined.");
                _windowEventHandlers[eventId] = handler;
            }
            else
            {
                _windowEventHandlers.Add(eventId, handler);
            }
        }

        public static void Discard(params SDL2.SDL_EventType[] types)
        {
            foreach (var type in types)
            {
                if (_discardedEventTypes.ContainsKey(type))
                {
                    _log.Warning($"{type} handler is getting discarded yet another time. Ignoring.");
                    continue;
                }

                _discardedEventTypes.Add(type, true);
            }
        }
       
        private static void DispatchWindowEvent(SDL2.SDL_WindowEvent ev)
        {
            if (_windowEventHandlers.TryGetValue(ev.windowEvent, out var handler))
            {
                handler.Invoke(ev);
            }
            
        }

        private static void DispatchEvent(SDL2.SDL_Event ev)
        {
            if (_eventHandlers.TryGetValue(ev.type, out var handler))
            {
                handler.Invoke(ev);
            }
            else
            {
                _log.Debug($"Unsupported generic event: {ev.type}.");
            }
        }
    }
}
