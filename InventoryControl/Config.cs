namespace InventoryControl
{
    using PlayerRoles;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Config
    {
        [Description("List of roles, their items, and chance (Do not add a role if you want its inventory to be normal)")]
        public Dictionary<string, RoleInventory> Inventory { get; set; } = new Dictionary<string, RoleInventory>()
        {
            {
                "DefaultCassD", new RoleInventory()
                {
                    RoleTypeId = RoleTypeId.ClassD,
                    KeepItems = false,
                    Items = new Dictionary<ItemType, int>()
                    {
                        { ItemType.Painkillers, 80 },
                        { ItemType.Coin, 100 }
                    }
                }
            },
            {
                "JanitorCassD", new RoleInventory()
                {
                    RoleTypeId = RoleTypeId.ClassD,
                    KeepItems = false,
                    Items = new Dictionary<ItemType, int>()
                    {
                        { ItemType.KeycardJanitor, 35 },
                        { ItemType.Painkillers, 80 },
                        { ItemType.Coin, 100 }
                    }
                }
            },
            {
                "DefaultScientist", new RoleInventory()
                {
                    RoleTypeId = RoleTypeId.Scientist,
                    KeepItems = true,
                    Items = new Dictionary<ItemType, int>()
                    {
                        { ItemType.Flashlight, 100 },
                        { ItemType.Coin, 90 }
                    }
                }
            }
        };

        [Description("List of ranks and their roles items and chances")]
        public Dictionary<string, Dictionary<string, RoleInventory>> InventoryRank { get; set; } = new Dictionary<string, Dictionary<string, RoleInventory>>()
        {
            {
                "owner", new Dictionary<string, RoleInventory>()
                {
                    {
                        "OwnerCassD", new RoleInventory()
                        {
                            RoleTypeId = RoleTypeId.ClassD,
                            KeepItems = false,
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
                        "OwnerScientist", new RoleInventory()
                        {
                            RoleTypeId = RoleTypeId.Scientist,
                            KeepItems = true,
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

    public class RoleInventory
    {
        public RoleTypeId RoleTypeId { get; set; }
        public bool KeepItems { get; set; }
        public Dictionary<ItemType, int> Items { get; set; }
    }
}
