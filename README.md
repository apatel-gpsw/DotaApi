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
   - Item Image URL example: http://cdn.dota2.com/apps/dota2/images/items/blink_lg.png
   <details>
   <summary>Hero JSON Object</summary>
   <p>
   
   ```json
   {
   "result":{
      "items":[
            {
               "id":1,
               "name":"item_blink",
               "cost":2250,
               "secret_shop":0,
               "side_shop":1,
               "recipe":0,
               "localized_name":"Blink Dagger"
            }
         ]
      }
   }
   \```
   
   </p>
   </details>
2. Build Heros List object using [GetHeroes()](https://wiki.teamfortress.com/wiki/WebAPI/GetHeroes). 
   - Hero Image URL example: http://cdn.dota2.com/apps/dota2/images/heroes/antimage_lg.png
   <details>
   <summary>Hero JSON Object</summary>
   <p>
   
   ```json
   {
   "result":{
      "heroes":[
            {
               "name":"npc_dota_hero_antimage",
               "id":1,
               "localized_name":"Anti-Mage"
            }
         ]
      }
   }
   \```
   
   </p>
   </details>
3. Build Abilities List object using `npc_abilities.txt` file. Steam doesn't provide any API to fetch the abilities, not sure why.
   - Ability Image URL example: http://cdn.dota2.com/apps/dota2/images/abilities/antimage_blink_md.png
   <details>
   <summary>Hero JSON Object</summary>
   <p>
   
   ```json
   {
   "DOTAAbilities":{
      "antimage_mana_break"
      {
         "ID"                  "5003"
         "AbilityBehavior"         "DOTA_ABILITY_BEHAVIOR_PASSIVE"
         "AbilityUnitDamageType"      "DAMAGE_TYPE_PHYSICAL"      
         "SpellImmunityType"         "SPELL_IMMUNITY_ENEMIES_NO"
         "AbilitySpecial"
         {
            "01"
            {
               "var_type"         "FIELD_FLOAT"
               "damage_per_burn"   "0.6"
            }
            "02"
            {
               "var_type"         "FIELD_INTEGER"
               "mana_per_hit"      "28 40 52 64"
            }
         }
      }
   }
   \```
   
   </p>
   </details>
4. Get match details using [GetMatchDetails()](https://wiki.teamfortress.com/wiki/WebAPI/GetMatchDetails). For now, the match id is hardcoded.
5. Show data on the console.