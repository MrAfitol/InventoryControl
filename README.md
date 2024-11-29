# InventoryControl
[![Version](https://img.shields.io/github/v/release/MrAfitol/InventoryControl?sort=semver&style=flat-square&color=blue&label=Version)](https://github.com/MrAfitol/InventoryControl/releases)
[![Downloads](https://img.shields.io/github/downloads/MrAfitol/InventoryControl/total?style=flat-square&color=yellow&label=Downloads)](https://github.com/MrAfitol/InventoryControl/releases)

A plugin that allows you to configure the default role inventory

## How download ?
   - *1. Find the SCP SL server config folder*
   
   *("C:\Users\(user name)\AppData\Roaming\SCP Secret Laboratory\" for windows, "/home/(user name)/.config/SCP Secret Laboratory/" for linux)*
  
   - *2. Find the "PluginAPI" folder there, it contains the "plugins" folder.*
  
   - *3. Select either the port of your server to install the same on that server or the "global" folder to install the plugin for all servers*
  
  ***Or***
  
   - *Run the command in console `p install MrAfitol/InventoryControl`*

## Config

```yml
# Custom inventory list for the role. (Do not add a role to the list if you want to leave the role as a regular inventory)
inventory:
  DefaultCassD:
    role_type_id: ClassD
    keep_items: false
    items:
      Painkillers: 80
      Coin: 100
    ammos: 
  JanitorCassD:
    role_type_id: ClassD
    keep_items: false
    items:
      KeycardJanitor: 35
      Painkillers: 80
      Coin: 100
    ammos: 
  DefaultScientist:
    role_type_id: Scientist
    keep_items: true
    items:
      Flashlight: 100
      Coin: 90
    ammos: 
# Custom inventory list for players with a rank
inventory_rank:
  owner:
    OwnerCassD:
      role_type_id: ClassD
      keep_items: false
      items:
        KeycardScientist: 80
        GunCOM18: 40
        Painkillers: 100
        Coin: 100
      ammos:
        Ammo9x19: 30
    OwnerScientist:
      role_type_id: Scientist
      keep_items: true
      items:
        GunCOM18: 65
        SCP500: 70
        Flashlight: 100
        Coin: 90
      ammos:
        Ammo9x19: 60
```


```
InventoryName:
   role_type_id: RoleType
   keep_items: true / false
   items:
      ItemType: Chance
   ammos:
      AmmoType: Amount
```

```
RankName:
   InventoryName:
      role_type_id: RoleType
      keep_items: true / false
      items:
         ItemType: Chance
      ammos:
         AmmoType: Amount
```

## Types

**RoleType**
```
Scp173,
ClassD,
Spectator,
Scp106,
NtfSpecialist,
Scp049,
Scientist,
Scp079,
ChaosConscript,
Scp096,
Scp0492,
NtfSergeant,
NtfCaptain,
NtfPrivate,
Tutorial,
FacilityGuard,
Scp939,
ChaosRifleman,
ChaosRepressor,
ChaosMarauder
```

**ItemType**
```
KeycardJanitor,
KeycardScientist,
KeycardResearchCoordinator,
KeycardZoneManager,
KeycardGuard,
KeycardNTFOfficer,
KeycardContainmentEngineer,
KeycardNTFLieutenant,
KeycardNTFCommander,
KeycardFacilityManager,
KeycardChaosInsurgency,
KeycardO5,
Radio,
GunCOM15,
Medkit,
Flashlight,
MicroHID,
SCP500,
SCP207,
Ammo12gauge,
GunE11SR,
GunCrossvec,
Ammo556x45,
GunFSP9,
GunLogicer,
GrenadeHE,
GrenadeFlash,
Ammo44cal,
Ammo762x39,
Ammo9x19,
GunCOM18,
SCP018,
SCP268,
Adrenaline,
Painkillers,
Coin,
ArmorLight,
ArmorCombat,
ArmorHeavy,
GunRevolver,
GunAK,
GunShotgun,
SCP330,
SCP2176,
SCP244a,
SCP244b,
SCP1853,
ParticleDisruptor,
GunCom45,
SCP1576,
Jailbird
```

**AmmoType**
```
Ammo12gauge,
Ammo556x45,
Ammo44cal,
Ammo762x39,
Ammo9x19,
```
