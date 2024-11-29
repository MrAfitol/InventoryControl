namespace InventoryControl
{
    using PlayerRoles;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Config
    {
        [Description("Custom inventory list for the role. (Do not add a role to the list if you want to leave the role as a regular inventory)")]
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

        [Description("Custom inventory list for players with a rank")]
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
                            },
                            Ammos = new Dictionary<ItemType, int>()
                            {
                                { ItemType.Ammo9x19, 30 }
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
                            },
                            Ammos = new Dictionary<ItemType, int>()
                            {
                                { ItemType.Ammo9x19, 60 }
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
        public Dictionary<ItemType, int> Ammos { get; set; }
    }
}
