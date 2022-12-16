# InventoryControl
[![GitHub release](https://flat.badgen.net/github/release/MrAfitol/InventoryControl)](https://github.com/MrAfitol/InventoryControl/releases/)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/MrAfitol/InventoryControl)

A plugin that will allow you to control the inventory of various roles

## How download ?
  *1. Find the SCP SL server config folder*
  
  *("C:\Users\(user name)\AppData\Roaming\SCP Secret Laboratory\" for windows, "/home/(user name)/.config/SCP Secret Laboratory/" for linux)*
  
  *2. Find the "PluginAPI" folder there, it contains the "plugins" folder.*
  
  *3. Select either the port of your server to install the same on that server or the "global" folder to install the plugin for all servers*

## Config

```yml
# List of roles, their items, and chance (Do not add a role if you want its inventory to be normal)
inventory:
  ClassD:
    KeycardJanitor: 35
    Painkillers: 80
    Coin: 100
```


```
RoleType:
   ItemType: Chance
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
GunCom45
```
