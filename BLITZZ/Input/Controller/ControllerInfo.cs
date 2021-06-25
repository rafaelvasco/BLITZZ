using System;
using System.Collections.Generic;

namespace BLITZZ.Input
{
    public class ControllerInfo
    {
        internal IntPtr InstancePointer { get; }
        internal int InstanceId { get; }

        public Guid Guid { get; }
        public int PlayerIndex { get; }
        public string Name { get; }
        public Dictionary<ControllerAxis, ushort> DeadZones { get; }

        internal ControllerInfo(IntPtr instancePointer, int instanceId, Guid guid, int playerIndex, string name)
        {
            InstancePointer = instancePointer;
            InstanceId = instanceId;

            Guid = guid;
            PlayerIndex = playerIndex;
            Name = name;

            DeadZones = new Dictionary<ControllerAxis, ushort>();
        }
    }
}
