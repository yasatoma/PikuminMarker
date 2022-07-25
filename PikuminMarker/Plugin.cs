using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Logging;

using System;
using System.IO;
using System.Reflection;

using PikuminMarker.Config;
using PikuminMarker.Service;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.Game.Object;

namespace PikuminMarker
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {
        public string Name => "PikuminMarker";
        private const string commandName = "/pkm";
        private const string commandName1 = "/pkm1";
        private const string commandName2 = "/pkm2";
        private const string commandName3 = "/pkm3";

        public Configuration Configuration { get; init; }
        private readonly ConfigurationUI ConfigurationUI;

        private readonly MakerDraw MakerDraw;


        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface
            )
        {
            PluginLog.LogDebug(string.Format("{0} ver {1} : Start. ", Name, Assembly.GetExecutingAssembly().GetName().Version));
            PluginServices.Initialize(pluginInterface);

            PluginServices.CommandManager.AddHandler(
                commandName, new CommandInfo(OnCommand)
                {
                    HelpMessage = "Open NamepleteColor ConfigWindow."
                });

            PluginServices.CommandManager.AddHandler(
                commandName1, new CommandInfo(OnCommand)
                {
                    HelpMessage = "Add Marker1 to <t>. e.g. /pkm1 <t>"
                });
            PluginServices.CommandManager.AddHandler(
                commandName2, new CommandInfo(OnCommand)
                {
                    HelpMessage = "Add Marker2 to <t>. e.g. /pkm2 <mo>"
                });
            PluginServices.CommandManager.AddHandler(
                commandName3, new CommandInfo(OnCommand)
                {
                    HelpMessage = "Add Marker3 to <t>."
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

            if (command == commandName)
            {
                // in response to the slash command, just display our main ui
                ConfigurationUI.Visible = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(args))
                {
                    PluginServices.ChatGui.PrintError($"/pkm* <t>. *=1,2,3.");
                    return;
                }

                var resolve = Framework.Instance()->GetUiModule()->GetPronounModule()->ResolvePlaceholder(args, 0, 0);
                if (resolve == null)
                {
                    foreach (var actor in PluginServices.ObjectTable)
                    {
                        if (actor == null) continue;
                        if (actor.Name.TextValue.Equals(args, StringComparison.InvariantCultureIgnoreCase))
                        {
                            resolve = (GameObject*)actor.Address;
                            break;
                        }
                    }
                }
                if (resolve != null && resolve->ObjectKind == 1 && resolve->SubKind == 4)
                {
                    switch (command)
                    {
                        case commandName1:
                            this.Configuration.Target1ObjectID = resolve->ObjectID;
                            break;
                        case commandName2:
                            this.Configuration.Target2ObjectID = resolve->ObjectID;
                            break;
                        case commandName3:
                            this.Configuration.Target3ObjectID = resolve->ObjectID;
                            break;
                    }
                }
            }
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
