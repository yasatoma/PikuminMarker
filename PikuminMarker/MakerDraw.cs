using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using ImGuiNET;
using Dalamud.Plugin;
using Dalamud.Interface;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Logging;
using Dalamud.Plugin.Services;

using PikuminMarker.Service;

namespace PikuminMarker
{
    internal class MakerDraw : IDisposable
    {

        private readonly Plugin plugin;
        private readonly IClientState _cs;
        private readonly IGameGui _gui;

        public MakerDraw(Plugin plugin)
        {
            this.plugin = plugin;
            _cs = PluginServices.ClientState;
            _gui = PluginServices.GameGui;

        }

        public void Draw()
        {

            // Main Process
            if (!this.plugin.Configuration.Enabled) return;

            // Sumomo Alamode 1FEFEA27270: 102B9E5C(271294044)
            if (this.plugin.Configuration.Target1ObjectID != null)
            {

                var obj = PluginServices.ObjectTable.SearchById(((uint)this.plugin.Configuration.Target1ObjectID));

                if (obj != null && obj is PlayerCharacter actor)
                {

                    // Target Player 1 にマーカー
                    if (!_gui.WorldToScreen(
                        new Vector3(actor.Position.X, actor.Position.Y, actor.Position.Z),
                        out var pos)) return;

                    ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
                    //ImGuiHelpers.ForceNextWindowMainViewport();
                    //ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(0, 0));

                    ImGui.Begin("Marker1",
                        ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoTitleBar |
                        ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoBackground);
                    ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);

                    ImGui.GetWindowDrawList().AddTriangleFilled(
                        new Vector2(pos.X, pos.Y),
                        new Vector2(pos.X - (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        new Vector2(pos.X + (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        ImGui.GetColorU32(this.plugin.Configuration.Target1Color)
                        );

                    ImGui.End();
                    ImGui.PopStyleVar();

                    //PluginLog.LogDebug($"{this.plugin.Name}: {actor.Name}/{actor.ObjectId}/{actor.Address}");
                }
            }

            if (this.plugin.Configuration.Target2ObjectID != null)
            {

                var obj = PluginServices.ObjectTable.SearchById(((uint)this.plugin.Configuration.Target2ObjectID));

                if (obj != null && obj is PlayerCharacter actor)
                {

                    // Target Player 2 にマーカー
                    if (!_gui.WorldToScreen(
                        new Vector3(actor.Position.X, actor.Position.Y, actor.Position.Z),
                        out var pos)) return;

                    ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
                    //ImGuiHelpers.ForceNextWindowMainViewport();
                    //ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(0, 0));

                    ImGui.Begin("Marker2",
                        ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoTitleBar |
                        ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoBackground);
                    ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);

                    ImGui.GetWindowDrawList().AddTriangleFilled(
                        new Vector2(pos.X, pos.Y),
                        new Vector2(pos.X - (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        new Vector2(pos.X + (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        ImGui.GetColorU32(this.plugin.Configuration.Target2Color)
                        );

                    ImGui.End();
                    ImGui.PopStyleVar();

                    //PluginLog.LogDebug($"{this.plugin.Name}: {actor.Name}/{actor.ObjectId}/{actor.Address}");
                }
            }

            if (this.plugin.Configuration.Target3ObjectID != null)
            {

                var obj = PluginServices.ObjectTable.SearchById(((uint)this.plugin.Configuration.Target3ObjectID));

                if (obj != null && obj is PlayerCharacter actor)
                {

                    // Target Player 3 にマーカー
                    if (!_gui.WorldToScreen(
                        new Vector3(actor.Position.X, actor.Position.Y, actor.Position.Z),
                        out var pos)) return;

                    ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
                    //ImGuiHelpers.ForceNextWindowMainViewport();
                    //ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(0, 0));

                    ImGui.Begin("Marker3",
                        ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoTitleBar |
                        ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoBackground);
                    ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);

                    ImGui.GetWindowDrawList().AddTriangleFilled(
                        new Vector2(pos.X, pos.Y),
                        new Vector2(pos.X - (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        new Vector2(pos.X + (10 * this.plugin.Configuration.scale), pos.Y + (15 * this.plugin.Configuration.scale)),
                        ImGui.GetColorU32(this.plugin.Configuration.Target3Color)
                        );

                    ImGui.End();
                    ImGui.PopStyleVar();

                    //PluginLog.LogDebug($"{this.plugin.Name}: {actor.Name}/{actor.ObjectId}/{actor.Address}");
                }
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
