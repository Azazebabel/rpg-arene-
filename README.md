Text based Rpg combat system / training of class based coding and basic interactionn with datasbase

Written in c# + sql lite 
Core Gameplay

Combat System:

Fight against various enemies like Goblins, Zombies, Warriors, and Champions.

Enemies have unique abilities and stats.

Player Progression:

Earn experience points (EXP) and gold.

Level up to improve attack power or max health.

Database Integration

Player Saves:

Automatically save the player's stats, inventory, and progress to an SQLite database.

Supports loading saved players or starting a new game.

Dynamic Enemy Management:

Separate databases for each enemy type (e.g., Goblins.db, Zombies.db).

Randomized enemy selection from the database during combat.

In order to test it yourself dowland project and open file then Bin ->Debug->net8.0 ant then open combattest1

Code is written in modular manner 

Character class is base from wich other classes heritage base statistic and logic
Player class heritage from character it functions and overite level up and take turn functions
Goblin/Zombie/Warrior/Chamion classes also heritage basic functionality form Character while overiting Take turn function to include simple logic 

Wepon class in itself dont have nothing outside declaration of functions used by it children clases
Sword/Fist/Spear/Fail/MagicSword classes overite empty fuctions insaide wepon class 

StatusEffect class has build in functions for counting down function and declaration of fuction for it chidren classes 
HealOverTime and DamageOverTime  overate only empty function while AttackPowerNerf overite in adition function already included in status efect class in order to reverse it efect after it turn timer end










Here is screan form isaide of internal dataabase  ![Untitled](https://github.com/user-attachments/assets/610ad4fa-f73a-41ed-a849-b783f85d4f44)


