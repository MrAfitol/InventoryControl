using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace InventoryControl
{

    public class Config
    {
        [Description("List of roles, their items, and chance (Do not add a role if you want its inventory to be normal)")]
        public Dictionary<RoleTypeId, Dictionary<ItemType, int>> Inventory { get; set; } = new Dictionary<RoleTypeId, Dictionary<ItemType, int>>()
        {
            { 
                RoleTypeId.ClassD, new Dictionary<ItemType, int>()
                {
                    { ItemType.KeycardJanitor, 35 },
                    { ItemType.Painkillers, 80 },
                    { ItemType.Coin, 100 }
                }
            }
        };
    }
}
