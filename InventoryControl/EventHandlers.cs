namespace InventoryControl
{
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
            try
            {
                if (player == null || !Round.IsRoundStarted) return;

                if (InventoryControl.Instance.Config.InventoryRank?.Count > 0)
                    if (InventoryControl.Instance.Config.InventoryRank.ContainsKey(GetPlayerGroupName(player)))
                        { SetRankRoleItem(player, newRole, GetPlayerGroupName(player)); return; }

                if (InventoryControl.Instance.Config.Inventory?.Count > 0)
                    if (InventoryControl.Instance.Config.Inventory.ContainsKey(newRole)) SetRoleItem(player, newRole);
            }
            catch (Exception e)
            {
                Log.Error("[InventoryControl] [Event: OnChangeRole] " + e.ToString());
            }
        }

        private void SetRoleItem(Player player, RoleTypeId newRole)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                try
                {
                    Dictionary<ItemType, ushort> Ammos = new Dictionary<ItemType, ushort>();

                    Ammos = player.AmmoBag;
                    player.ClearInventory(true, false);

                    KeyValuePair<RoleTypeId, InventoryItem> RoleInventory = InventoryControl.Instance.Config.Inventory.First(x => x.Key == newRole);

                    if (!RoleInventory.Value.keepItems)
                        player.ClearInventory(false);

                    foreach (KeyValuePair<ItemType, int> Item in RoleInventory.Value.Items)
                        if (Item.Value >= Random.Range(0, 101))
                        {
                            ItemBase itemBase = player.AddItem(Item.Key);

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

                    for (int ammo = 0; ammo < Ammos.Count; ammo++)
                        if (Ammos.ElementAt(ammo).Value > 0)
                            player.SetAmmo(Ammos.ElementAt(ammo).Key, Ammos.ElementAt(ammo).Value);
                }
                catch (Exception e)
                {
                    Log.Error("[InventoryControl] [Event: SetRoleItem] " + e.ToString());
                }
            });
        }

        private void SetRankRoleItem(Player player, RoleTypeId newRole, string groupName)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                try
                {
                    if (InventoryControl.Instance.Config.InventoryRank.ContainsKey(ServerStatic.PermissionsHandler._members[player.UserId]))
                    {
                        if (!InventoryControl.Instance.Config.InventoryRank[groupName].ContainsKey(player.Role)) return;

                        Dictionary<ItemType, ushort> Ammos = new Dictionary<ItemType, ushort>();

                        Ammos = player.AmmoBag;
                        player.ClearInventory(true, false);

                        KeyValuePair<RoleTypeId, InventoryItem> RoleInventory = InventoryControl.Instance.Config.InventoryRank[ServerStatic.PermissionsHandler._members[player.UserId]].First(x => x.Key == newRole);

                        if (!RoleInventory.Value.keepItems)
                            player.ClearInventory(false);

                        foreach (KeyValuePair<ItemType, int> Item in RoleInventory.Value.Items)
                            if (Item.Value >= Random.Range(0, 101))
                            {
                                ItemBase itemBase = player.AddItem(Item.Key);

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

                        for (int ammo = 0; ammo < Ammos.Count; ammo++)
                            if (Ammos.ElementAt(ammo).Value > 0)
                                player.SetAmmo(Ammos.ElementAt(ammo).Key, Ammos.ElementAt(ammo).Value);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("[InventoryControl] [Event: SetRankRoleItem] " + e.ToString());
                }
            });
        }

        private string GetPlayerGroupName(Player player)
        {
            try
            {
                if (player.UserId == null) return string.Empty;

                if (ServerStatic.PermissionsHandler._members.ContainsKey(player.UserId))
                {
                    return ServerStatic.PermissionsHandler._members[player.UserId];
                }
                else
                {
                    return player.ReferenceHub.serverRoles.Group != null ? ServerStatic.GetPermissionsHandler()._groups.FirstOrDefault(g => EqualsTo(g.Value, player.ReferenceHub.serverRoles.Group)).Key : string.Empty;
                }
            }
            catch (Exception e)
            {
                Log.Error("[InventoryControl] [Event: GetPlayerGroupName] " + e.ToString());
                return string.Empty;
            }
        }

        private bool EqualsTo(UserGroup check, UserGroup player)
            => check.BadgeColor == player.BadgeColor
               && check.BadgeText == player.BadgeText
               && check.Permissions == player.Permissions
               && check.Cover == player.Cover
               && check.HiddenByDefault == player.HiddenByDefault
               && check.Shared == player.Shared
               && check.KickPower == player.KickPower
               && check.RequiredKickPower == player.RequiredKickPower;
    }
}
