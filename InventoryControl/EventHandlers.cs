namespace InventoryControl
{
    using InventorySystem;
    using InventorySystem.Items;
    using InventorySystem.Items.Firearms;
    using InventorySystem.Items.Firearms.Attachments;
    using MEC;
    using PlayerRoles;
    using PluginAPI.Core;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Random = UnityEngine.Random;

    public class EventHandlers
    {

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnChangeRole(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason reason)
        {
            if (!InventoryControl.Instance.Config.Inventory.ContainsKey(newRole)) return;

            Timing.CallDelayed(0.1f, () =>
            {
                try
                {
                    Dictionary<ItemType, ushort> Ammo = new Dictionary<ItemType, ushort>();

                    foreach (KeyValuePair<ItemType, ushort> item in player.ReferenceHub.inventory.UserInventory.ReserveAmmo)
                        Ammo.Add(item.Key, item.Value);

                    for (int ammo = 0; ammo < player.ReferenceHub.inventory.UserInventory.ReserveAmmo.Count; ammo++)
                        player.ReferenceHub.inventory.ServerSetAmmo(player.ReferenceHub.inventory.UserInventory.ReserveAmmo.ElementAt(ammo).Key, 0);

                    for (int item = 0; player.ReferenceHub.inventory.UserInventory.Items.Count > 0; item++)
                        player.ReferenceHub.inventory.ServerRemoveItem(player.ReferenceHub.inventory.UserInventory.Items.ElementAt(0).Key, null);

                    foreach (KeyValuePair<RoleTypeId, Dictionary<ItemType, int>> RoleInventory in InventoryControl.Instance.Config.Inventory)
                        if (RoleInventory.Key == newRole)
                            foreach (KeyValuePair<ItemType, int> Item in RoleInventory.Value)
                                if (Item.Value >= Random.Range(0, 101))
                                {
                                    ItemBase itemBase = player.ReferenceHub.inventory.ServerAddItem(Item.Key);

                                    if (itemBase is Firearm firearm)
                                    {
                                        if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(player.ReferenceHub, out var value) && value.TryGetValue(itemBase.ItemTypeId, out var value2))
                                            firearm.ApplyAttachmentsCode(value2, reValidate: true);

                                        FirearmStatusFlags firearmStatusFlags = FirearmStatusFlags.MagazineInserted;
                                        if (firearm.HasAdvantageFlag(AttachmentDescriptiveAdvantages.Flashlight))
                                            firearmStatusFlags |= FirearmStatusFlags.FlashlightEnabled;

                                        firearm.Status = new FirearmStatus(firearm.AmmoManagerModule.MaxAmmo, firearmStatusFlags, firearm.GetCurrentAttachmentsCode());
                                    }
                                }

                    for (int ammo = 0; ammo < Ammo.Count; ammo++)
                        if (Ammo.ElementAt(ammo).Value > 0)
                            player.ReferenceHub.inventory.ServerSetAmmo(Ammo.ElementAt(ammo).Key, Ammo.ElementAt(ammo).Value);
                }
                catch (Exception e)
                {
                    Log.Error("[InventoryControl] [Event: OnChangeRole] " + e.ToString());
                }
            });
        }
    }
}