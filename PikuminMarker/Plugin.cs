using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Logging;

using System;
using System.IO;
using System.Reflection;

using PikuminMarker.Config;
using PikuminMarker.Service;


namespace PikuminMarker
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "PikuminMarker";
        private const string commandName = "/pkm";

        public Configuration Configuration { get; init; }
        private readonly ConfigurationUI ConfigurationUI;

        private readonly MakerDraw MakerDraw;


        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface
            )
        {
            PluginLog.LogDebug(string.Format("{0} ver {1} : Start. ", Name, Assembly.GetExecutingAssembly().GetName().Version));
            PluginServices.Initialize(pluginInterface);

            PluginServices.CommandManager.AddHandler(commandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Open NamepleteColor ConfigWindow."
            });

            Configuration = PluginServices.DalamudPluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginServices.DalamudPluginInterface);
            ConfigurationUI = new ConfigurationUI(this, Configuration);

            MakerDraw = new MakerDraw(this);

            PluginServices.DalamudPluginInterface.UiBuilder.Draw += DrawUI;
            PluginServices.DalamudPluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            try
            {
                PluginServices.DalamudPluginInterface.UiBuilder.Draw -= DrawUI;
                PluginServices.DalamudPluginInterface.UiBuilder.OpenConfigUi -= DrawConfigUI;
                if (ConfigurationUI != null)
                {
                    ConfigurationUI.Dispose();
                }

                PluginServices.CommandManager.RemoveHandler(commandName);

                PluginLog.LogDebug($"{Name}: Dispose.");
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, $"{Name}: Failed to Dispose plugin.");
            }
        }

        private void OnCommand(string command, string args)
        {
            // in response to the slash command, just display our main ui
            ConfigurationUI.Visible = true;
        }

        private void DrawUI()
        {
            MakerDraw.Draw();
            ConfigurationUI.Draw();
        }

        private void DrawConfigUI()
        {
            ConfigurationUI.Visible = true;
        }
    }
}
