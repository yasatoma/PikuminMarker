using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Numerics;

namespace PikuminMarker.Config
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;

        public bool Enabled { get; set; } = true;

        public uint? Target1ObjectID;
        public uint? Target2ObjectID;
        public uint? Target3ObjectID;

        public Vector4 Target1Color = new(1f, 1f, 1f, 1f);
        public Vector4 Target2Color = new(1f, 1f, 1f, 1f);
        public Vector4 Target3Color = new(1f, 1f, 1f, 1f);

        public float scale = 1;

        // the below exist just to make saving less cumbersome

        [NonSerialized]
        private DalamudPluginInterface? pluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            pluginInterface!.SavePluginConfig(this);
        }
    }
}
