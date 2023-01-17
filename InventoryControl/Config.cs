namespace InventoryControl
{
    using PlayerRoles;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Config
    {
        [Description("List of roles, their items, and chance (Do not add a role if you want its inventory to be normal)")]
        public Dictionary<RoleTypeId, InventoryItem> Inventory { get; set; } = new Dictionary<RoleTypeId, InventoryItem>()
        {
            {
                RoleTypeId.ClassD, new InventoryItem()
                {
                    keepItems = false,
                    Items = new Dictionary<ItemType, int>()
                    {
                        { ItemType.KeycardJanitor, 35 },
                        { ItemType.Painkillers, 80 },
                        { ItemType.Coin, 100 }
                    }
                }
            },
            {
                RoleTypeId.Scientist, new InventoryItem()
                {
                    keepItems = true,
                    Items = new Dictionary<ItemType, int>()
                    {
                        { ItemType.Flashlight, 100 },
                        { ItemType.Coin, 90 }
                    }
                }
            }
        };

        [Description("List of ranks and their roles items and chances")]
        public Dictionary<string, Dictionary<RoleTypeId, InventoryItem>> InventoryRank { get; set; } = new Dictionary<string, Dictionary<RoleTypeId, InventoryItem>>()
        {
            {
                "owner", new Dictionary<RoleTypeId, InventoryItem>()
                {
                    {
                        RoleTypeId.ClassD, new InventoryItem()
                        {
                            keepItems = false,
                            Items = new Dictionary<ItemType, int>()
                            {
                                { ItemType.KeycardScientist, 80 },
                                { ItemType.GunCOM18, 40 },
                                { ItemType.Painkillers, 100 },
                                { ItemType.Coin, 100 }
                            }
                        }
                    },
                    {
                        RoleTypeId.Scientist, new InventoryItem()
                        {
                            keepItems = true,
                            Items = new Dictionary<ItemType, int>()
                            {
                                { ItemType.GunCOM18, 65 },
                                { ItemType.SCP500, 70 },
                                { ItemType.Flashlight, 100 },
                                { ItemType.Coin, 90 }
                            }
                        }
                    }
                }
            }
        };
    }

    public class InventoryItem
    {
        public bool keepItems { get; set; }
        public Dictionary<ItemType, int> Items { get; set; }
    }
}
