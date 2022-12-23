namespace InventoryControl
{
    using InventorySystem;
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

                    foreach (var item in player.ReferenceHub.inventory.UserInventory.ReserveAmmo)
                    {
                        Ammo.Add(item.Key, item.Value);
                    }

                    int ammoCount = player.ReferenceHub.inventory.UserInventory.ReserveAmmo.Count;
                    int whileAmmo = 0;

                    while (whileAmmo < ammoCount)
                    {
                        player.ReferenceHub.inventory.ServerSetAmmo(player.ReferenceHub.inventory.UserInventory.ReserveAmmo.ElementAt(whileAmmo).Key, 0);
                        whileAmmo++;
                    }

                    int itemCount = player.ReferenceHub.inventory.UserInventory.Items.Count;
                    int whileItem = 0;

                    while (whileItem < itemCount)
                    {
                        player.ReferenceHub.inventory.ServerRemoveItem(player.ReferenceHub.inventory.UserInventory.Items.ElementAt(0).Key, null);
                        whileItem++;
                    }

                    foreach (var RoleInventory in InventoryControl.Instance.Config.Inventory)
                    {
                        if (RoleInventory.Key == newRole)
                        {
                            foreach (var Item in RoleInventory.Value)
                            {
                                if (Item.Value >= Random.Range(0, 101))
                                {
                                    var itemBase = player.ReferenceHub.inventory.ServerAddItem(Item.Key);

                                    if (itemBase is Firearm firearm)
                                    {
                                        if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(player.ReferenceHub, out var value) && value.TryGetValue(itemBase.ItemTypeId, out var value2))
                                        {
                                            firearm.ApplyAttachmentsCode(value2, reValidate: true);
                                        }
                                        FirearmStatusFlags firearmStatusFlags = FirearmStatusFlags.MagazineInserted;
                                        if (firearm.HasAdvantageFlag(AttachmentDescriptiveAdvantages.Flashlight))
                                        {
                                            firearmStatusFlags |= FirearmStatusFlags.FlashlightEnabled;
                                        }
                                        firearm.Status = new FirearmStatus(firearm.AmmoManagerModule.MaxAmmo, firearmStatusFlags, firearm.GetCurrentAttachmentsCode());
                                    }
                                }
                            }
                        }
                    }

                    ammoCount = Ammo.Count;
                    whileAmmo = 0;

                    while (whileAmmo < ammoCount)
                    {
                        if (Ammo.ElementAt(whileAmmo).Value > 0)
                            player.ReferenceHub.inventory.ServerSetAmmo(Ammo.ElementAt(whileAmmo).Key, Ammo.ElementAt(whileAmmo).Value);
                        whileAmmo++;
                    }
                }
                catch (Exception e)
                {
                    Log.Error("[InventoryControl] [Event: OnChangeRole] " + e.ToString());
                }
            });
        }
    }
}
