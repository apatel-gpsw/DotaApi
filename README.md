# DotaApi

Using the Steam APIs building match details on the windows tool.

**Better Steam API documentation** (Dota Game ID: 570)
https://steamwebapi.azurewebsites.net/

**Subset of useful APIs**
https://dev.dota2.com/showthread.php?t=58317

**Get Images**
https://dev.dota2.com/showthread.php?t=138016


## Flow:
1. Build Items List object using [GetGameItems()](https://wiki.teamfortress.com/wiki/WebAPI/GetGameItems).
2. Build Heros List object using [GetHeroes()](https://wiki.teamfortress.com/wiki/WebAPI/GetHeroes).
3. Build Abilities List object using `npc_abilities.txt` file. Steam doesn't provide any API to fetch the abilities, not sure why.
4. Get match details using [GetMatchDetails()](https://wiki.teamfortress.com/wiki/WebAPI/GetMatchDetails). For now, the match id is hardcoded.
5. Show data on the console.