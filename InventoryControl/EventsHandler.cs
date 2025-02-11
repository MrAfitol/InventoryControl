﻿using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Modules;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace InventoryControl
{
    public class EventsHandler
    {
        public void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            try
            {
                if (ev.Player == null || !Round.IsRoundStarted) return;

                Timing.CallDelayed(0.01f, () =>
                {
                    if (InventoryControl.Instance.Config.InventoryRank?.Count > 0)
                        if (InventoryControl.Instance.Config.InventoryRank.ContainsKey(GetPlayerGroupName(ev.Player)))
                            if (InventoryControl.Instance.Config.InventoryRank[GetPlayerGroupName(ev.Player)].Count(x => x.Value.RoleTypeId == ev.NewRole) > 0)
                            { SetRankRoleItem(ev.Player, ev.NewRole, GetPlayerGroupName(ev.Player)); return; }

                    if (InventoryControl.Instance.Config.Inventory?.Count > 0)
                        if (InventoryControl.Instance.Config.Inventory.Count(x => x.Value.RoleTypeId == ev.NewRole) > 0) SetRoleItem(ev.Player, ev.NewRole);
                });
            }
            catch (Exception e)
            {
                Logger.Error("[InventoryControl] [Event: OnPlayerChangedRole] " + e.ToString());
            }
        }

        private void SetRoleItem(Player player, RoleTypeId newRole)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                try
                {
                    Dictionary<ItemType, ushort> Ammos = new Dictionary<ItemType, ushort>();

                    foreach (KeyValuePair<ItemType, ushort> item in player.ReferenceHub.inventory.UserInventory.ReserveAmmo)
                        Ammos.Add(item.Key, item.Value);

                    for (int ammo = 0; ammo < player.ReferenceHub.inventory.UserInventory.ReserveAmmo.Count; ammo++)
                        player.SetAmmo(player.ReferenceHub.inventory.UserInventory.ReserveAmmo.ElementAt(ammo).Key, 0);

                    KeyValuePair<string, RoleInventory> RoleInventory = InventoryControl.Instance.Config.Inventory.Where(x => x.Value.RoleTypeId == newRole).ToList().RandomItem();

                    if (!RoleInventory.Value.KeepItems && player.Inventory.UserInventory.Items.Count > 0)
                        player.ClearInventory(false);

                    foreach (KeyValuePair<ItemType, int> Item in RoleInventory.Value.Items)
                        if (Item.Value >= Random.Range(0, 101))
                        {
                            ItemBase itemBase = player.AddItem(Item.Key).Base;

                            if (itemBase is Firearm firearm)
                            {
                                if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(player.ReferenceHub, out var value) && value.TryGetValue(itemBase.ItemTypeId, out var value2))
                                    firearm.ApplyAttachmentsCode(value2, reValidate: true);

                                if (firearm.Modules.First(x => x is MagazineModule) is MagazineModule magazineModule)
                                {
                                    magazineModule.ServerInsertEmptyMagazine();
                                    magazineModule.AmmoStored = magazineModule.AmmoMax;
                                    magazineModule.ServerResyncData();
                                }
                            }
                        }

                    if (RoleInventory.Value?.Ammos?.Count > 0)
                        foreach (KeyValuePair<ItemType, int> Ammo in RoleInventory.Value.Ammos)
                            if (IsAmmo(Ammo.Key))
                                player.AddAmmo(Ammo.Key, (ushort)Ammo.Value);

                    for (int ammo = 0; ammo < Ammos.Count; ammo++)
                        if (Ammos.ElementAt(ammo).Value > 0)
                            player.SetAmmo(Ammos.ElementAt(ammo).Key, Ammos.ElementAt(ammo).Value);
                }
                catch (Exception e)
                {
                    Logger.Error("[InventoryControl] [Event: SetRoleItem] " + e.ToString());
                }
            });
        }

        private void SetRankRoleItem(Player player, RoleTypeId newRole, string groupName)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                try
                {
                    if (InventoryControl.Instance.Config.InventoryRank.ContainsKey(ServerStatic.PermissionsHandler.Members[player.UserId]))
                    {
                        if (InventoryControl.Instance.Config.InventoryRank[groupName].Count(x => x.Value.RoleTypeId == newRole) <= 0) return;

                        Dictionary<ItemType, ushort> Ammos = new Dictionary<ItemType, ushort>();

                        foreach (KeyValuePair<ItemType, ushort> item in player.ReferenceHub.inventory.UserInventory.ReserveAmmo)
                            Ammos.Add(item.Key, item.Value);

                        for (int ammo = 0; ammo < player.ReferenceHub.inventory.UserInventory.ReserveAmmo.Count; ammo++)
                            player.SetAmmo(player.ReferenceHub.inventory.UserInventory.ReserveAmmo.ElementAt(ammo).Key, 0);

                        KeyValuePair<string, RoleInventory> RoleInventory = InventoryControl.Instance.Config.InventoryRank[ServerStatic.PermissionsHandler.Members[player.UserId]].Where(x => x.Value.RoleTypeId == newRole).ToList().RandomItem();

                        if (!RoleInventory.Value.KeepItems && player.Inventory.UserInventory.Items.Count > 0)
                            player.ClearInventory(false);

                        foreach (KeyValuePair<ItemType, int> Item in RoleInventory.Value.Items)
                            if (Item.Value >= Random.Range(0, 101))
                            {
                                ItemBase itemBase = player.AddItem(Item.Key).Base;

                                if (itemBase is Firearm firearm)
                                {
                                    if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(player.ReferenceHub, out var value) && value.TryGetValue(itemBase.ItemTypeId, out var value2))
                                        firearm.ApplyAttachmentsCode(value2, reValidate: true);

                                    if (firearm.Modules.First(x => x is MagazineModule) is MagazineModule magazineModule)
                                    {
                                        magazineModule.ServerInsertEmptyMagazine();
                                        magazineModule.AmmoStored = magazineModule.AmmoMax;
                                        magazineModule.ServerResyncData();
                                    }
                                }
                            }

                        if (RoleInventory.Value?.Ammos?.Count > 0)
                            foreach (KeyValuePair<ItemType, int> Ammo in RoleInventory.Value.Ammos)
                                if (IsAmmo(Ammo.Key))
                                    player.AddAmmo(Ammo.Key, (ushort)Ammo.Value);

                        for (int ammo = 0; ammo < Ammos.Count; ammo++)
                            if (Ammos.ElementAt(ammo).Value > 0)
                                player.SetAmmo(Ammos.ElementAt(ammo).Key, Ammos.ElementAt(ammo).Value);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("[InventoryControl] [Event: SetRankRoleItem] " + e.ToString());
                }
            });
        }

        private string GetPlayerGroupName(Player player)
        {
            try
            {
                if (player.UserId == null) return string.Empty;

                if (ServerStatic.PermissionsHandler.Members.ContainsKey(player.UserId))
                {
                    return ServerStatic.PermissionsHandler.Members[player.UserId];
                }
                else
                {
                    return player.ReferenceHub.serverRoles.Group != null ? ServerStatic.PermissionsHandler.Groups.FirstOrDefault(g => EqualsTo(g.Value, player.ReferenceHub.serverRoles.Group)).Key : string.Empty;
                }
            }
            catch (Exception e)
            {
                Logger.Error("[InventoryControl] [Event: GetPlayerGroupName] " + e.ToString());
                return string.Empty;
            }
        }

        public static bool IsAmmo(ItemType type)
        {
            if (type == ItemType.Ammo9x19 || type == ItemType.Ammo762x39 || type == ItemType.Ammo556x45 || type == ItemType.Ammo44cal || type == ItemType.Ammo12gauge)
                return true;
            return false;
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
