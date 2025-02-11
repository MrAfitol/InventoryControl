using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using System;

namespace InventoryControl
{
    public class InventoryControl : Plugin<Config>
    {
        public static InventoryControl Instance { get; private set; }

        public override string Name { get; } = "InventoryControl";

        public override string Description { get; } = "A plugin that will allow you to control the inventory of various roles.";

        public override string Author { get; } = "MrAfitol";

        public override Version Version { get; } = new Version(1, 2, 0);

        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

        public EventsHandler EventsHandler { get; private set; }

        public override void Enable()
        {
            Instance = this;
            EventsHandler = new EventsHandler();
            PlayerEvents.ChangedRole += EventsHandler.OnPlayerChangedRole;
        }

        public override void Disable()
        {
            PlayerEvents.ChangedRole -= EventsHandler.OnPlayerChangedRole;
            EventsHandler = null;
            Instance = null;
        }
    }
}
