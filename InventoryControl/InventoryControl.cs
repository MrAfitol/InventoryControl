namespace InventoryControl
{
    using PluginAPI.Core.Attributes;
    using PluginAPI.Enums;
    using PluginAPI.Events;

    public class InventoryControl
    {
        public static InventoryControl Instance { get; private set; }

        [PluginConfig("configs/inventory-control.yml")]
        public static Config Config;

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("InventoryControl", "1.1.4", "A plugin that will allow you to control the inventory of various roles.", "MrAfitol")]
        void LoadPlugin()
        {
            Instance = this;
            EventManager.RegisterEvents<EventHandlers>(this);
        }
    }
}
