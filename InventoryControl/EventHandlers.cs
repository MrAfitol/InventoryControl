namespace InventoryControl
{
    using InventorySystem;
    using MEC;
    using PlayerRoles;
    using PluginAPI.Core;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Core.Items;
    using PluginAPI.Enums;
    using System.Linq;
    using Random = UnityEngine.Random;

    public class EventHandlers
    {

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnChangeRole(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason reason)
        {
            if (!InventoryControl.Instance.Config.Inventory.ContainsKey(newRole)) return;

            Timing.CallDelayed(0.3f, () =>
            {
                int itemCount = player.ReferenceHub.inventory.UserInventory.Items.Count;
                int i = 0;

                while (i < itemCount)
                {
                    player.ReferenceHub.inventory.ServerRemoveItem(player.ReferenceHub.inventory.UserInventory.Items.ElementAt(0).Key, null);
                    i++;
                }

                foreach (var RoleInventory in InventoryControl.Instance.Config.Inventory)
                {
                    if (RoleInventory.Key == newRole)
                    {
                        foreach (var Item in RoleInventory.Value)
                        {
                            if (Item.Value >= Random.Range(0, 101)) player.ReferenceHub.inventory.ServerAddItem(Item.Key);
                        }
                    }
                }
            });
        }
    }
}
