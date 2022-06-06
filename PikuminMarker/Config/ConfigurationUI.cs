using System;
using System.Numerics;
using System.Reflection;

using ImGuiNET;
using Dalamud.Logging;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.Colors;

using PikuminMarker.Service;

namespace PikuminMarker.Config
{
    internal class ConfigurationUI : IDisposable
    {
        private Configuration configuration;
        private readonly Plugin plugin;

        private bool visible = false;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public ConfigurationUI(Plugin plugin, Configuration configuration)
        {
            this.plugin = plugin;
            this.configuration = configuration;
        }

        public void Dispose()
        {

        }

        public void Draw()
        {
            try
            {

                DrawConfigWindow();
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, $"{this.plugin.Name}: Failed to Draw.");
            }

        }

        public void DrawConfigWindow()
        {
            if (!Visible)
            {
                return;
            }

            // 初期サイズ設定
            ImGui.SetNextWindowSize(new Vector2(480, 240), ImGuiCond.FirstUseEver);

            // 最小サイズ設定
            ImGui.SetNextWindowSizeConstraints(new Vector2(480, 240), new Vector2(600, 600));
            ImGui.Separator();

            if (ImGui.Begin(
                $"{this.plugin.Name} Congfig {Assembly.GetExecutingAssembly().GetName().Version}",
                ref visible,
                ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {

                bool _enabled = configuration.Enabled;

                if (ImGui.Checkbox("Enabled", ref _enabled))
                {
                    configuration.Enabled = _enabled;
                    configuration.Save();
                }

                ImGui.Separator();


                ImGui.Text("Target:");

                PlayerCharacter? target = null;
                if (PluginServices.ClientState.LocalPlayer!.TargetObject != null && PluginServices.ClientState.LocalPlayer.TargetObject is PlayerCharacter)
                {
                    ImGui.SameLine();
                    target = (PlayerCharacter)PluginServices.ClientState.LocalPlayer.TargetObject;
                    ImGui.TextColored(ImGuiColors.DalamudOrange, $"{target.ObjectId}:{target.Name}");
                }

                ImGui.Spacing();

                Vector4 _col;

                #region ----- Target 1 -----
                ImGui.Text("Marker 1:");
                ImGui.SameLine();

                _col = this.configuration.Target1Color;
                if (ImGui.ColorEdit4("###Target1_Color", ref _col, ImGuiColorEditFlags.NoInputs))
                {
                    if (_col != this.configuration.Target1Color)
                    {
                        this.configuration.Target1Color = _col;
                        this.configuration.Save();
                    }
                }
                ImGui.SameLine();

                // 登録IDを表示する
                if (configuration.Target1ObjectID != null)
                {
                    var obj = PluginServices.ObjectTable.SearchById((uint)configuration.Target1ObjectID);
                    ImGui.TextColored(
                        obj != null ? ImGuiColors.DalamudViolet : ImGuiColors.DalamudGrey3, 
                        $"{configuration.Target1ObjectID}:{obj?.Name ?? Common.c_Unknown}"
                        );
                    ImGui.SameLine();
                }


                if (target != null)
                {
                    if (ImGui.Button("Target" + "###Upd_Target1"))
                    {
                        configuration.Target1ObjectID = target.ObjectId;
                        configuration.Save();
                    }
                    ImGui.SameLine();
                }
                if (ImGui.Button("Delete" + "###Del_Target1"))
                {
                    configuration.Target1ObjectID = null;
                    configuration.Save();
                }
                #endregion

                ImGui.Spacing();

                #region ----- Target 2 -----
                ImGui.Text("Marker 2:");
                ImGui.SameLine();

                _col = this.configuration.Target2Color;
                if (ImGui.ColorEdit4("###Target2_Color", ref _col, ImGuiColorEditFlags.NoInputs))
                {
                    if (_col != this.configuration.Target2Color)
                    {
                        this.configuration.Target2Color = _col;
                        this.configuration.Save();
                    }
                }
                ImGui.SameLine();

                // 登録IDを表示する
                if (configuration.Target2ObjectID != null)
                {
                    var obj = PluginServices.ObjectTable.SearchById((uint)configuration.Target2ObjectID);
                    ImGui.TextColored(
                        obj != null ? ImGuiColors.DalamudViolet : ImGuiColors.DalamudGrey3,
                        $"{configuration.Target2ObjectID}:{obj?.Name ?? Common.c_Unknown}"
                        );
                    ImGui.SameLine();
                }


                if (target != null)
                {
                    if (ImGui.Button("Target" + "###Upd_Target2"))
                    {
                        configuration.Target2ObjectID = target.ObjectId;
                        configuration.Save();
                    }
                    ImGui.SameLine();
                }
                if (ImGui.Button("Delete" + "###Del_Target2"))
                {
                    configuration.Target2ObjectID = null;
                    configuration.Save();
                }
                #endregion

                ImGui.Spacing();

                #region ----- Target 3 -----
                ImGui.Text("Marker 3:");
                ImGui.SameLine();

                _col = this.configuration.Target3Color;
                if (ImGui.ColorEdit4("###Target3_Color", ref _col, ImGuiColorEditFlags.NoInputs))
                {
                    if (_col != this.configuration.Target3Color)
                    {
                        this.configuration.Target3Color = _col;
                        this.configuration.Save();
                    }
                }
                ImGui.SameLine();

                // 登録IDを表示する
                if (configuration.Target3ObjectID != null)
                {
                    var obj = PluginServices.ObjectTable.SearchById((uint)configuration.Target3ObjectID);
                    ImGui.TextColored(
                        obj != null ? ImGuiColors.DalamudViolet : ImGuiColors.DalamudGrey3,
                        $"{configuration.Target3ObjectID}:{obj?.Name ?? Common.c_Unknown}"
                        );
                    ImGui.SameLine();
                }


                if (target != null)
                {
                    if (ImGui.Button("Target" + "###Upd_Target3"))
                    {
                        configuration.Target3ObjectID = target.ObjectId;
                        configuration.Save();
                    }
                    ImGui.SameLine();
                }
                if (ImGui.Button("Delete" + "###Del_Target3"))
                {
                    configuration.Target3ObjectID = null;
                    configuration.Save();
                }
                #endregion

                ImGui.Spacing();

                float value = this.configuration.scale;
                if (ImGui.DragFloat("Marker Scale###Scale", ref value, 0.1f, 0.1f, 10f)) {
                    this.configuration.scale = value;
                    this.configuration.Save();
                }
            }
        }
    }
}
