using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPGFight

{
    public abstract class Character
    {
        //CHARACTER STATISTICS
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }
        public int BlockDmg { get; set; }
        public int HealthPotion { get; set; }
        public int Gold { get; set; }
        public decimal ArmourValue { get; set; }

        public Weapon Wepon { get; set; }

        public List<StatusEffect> ActiveEffects { get; private set; } = new List<StatusEffect>();


        public int Exp { get; set; }

        public Character(string name, int health, int attackPower, int healthPotion, decimal armourValue, int maxHealth, int blockDmg, int gold,Weapon weapon, int exp)
        {//Iniciate statistics
            Name = name;
            Health = health;
            AttackPower = attackPower;
            HealthPotion = healthPotion;
            ArmourValue = armourValue;
            MaxHealth = maxHealth;
            BlockDmg = blockDmg;
            Gold = gold;
            Wepon = weapon;
            Exp = exp;
        }
        public void LevelUp() { }//Level up function 
        public void ApplyStatusEffect(StatusEffect effect) //add status effect to character
        {
            ActiveEffects.Add(effect);
            Console.WriteLine($"{Name} is affected by {effect.Name} for {effect.Duration} turns!");
        }

        public void ProcessEffects()
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)//Go throught all active efects
            {
                StatusEffect effect = ActiveEffects[i];
                effect.ApplyEffect(this);//Activate effect
                effect.ReduceDuration(this);//Reduce it timer

                if (effect.IsExpired())
                {//Remove effect
                    Console.WriteLine($"{effect.Name} has expired on {Name}.");
                    ActiveEffects.RemoveAt(i);
                }
            }
        }

        public abstract void TakeTurn(Character oponent, Random random);
        public int DealDamage(Random random)
        {
            return Wepon.DealDamage(random,this);//Invokes wepon deamage logic
        }

        public int HealDamage(Random random)
        {
            if (HealthPotion > 0)//Check if player still has potions
            {
                HealthPotion -= 1;
                int Heal = random.Next(20, 40);
                Health += Heal;
                if (Health > MaxHealth) Health = MaxHealth; // Ensure health doesn't go over max walue
                return Heal;
            }
            else { return 0; }
        }

        public int TakeDamage(int damage)
        {
            damage = damage - BlockDmg;//reduce Damage by block 
            BlockDmg = 0;
            if (ArmourValue > 0)
            {//Reduce damage by armor 
                decimal taken = damage * ArmourValue;
                damage = (int)Math.Round(taken, 0);
                Health -= damage;

            }
            else
                Health -= damage;
            if (Health < 0) Health = 0; // Ensure health doesn't go below zero
            return damage;
        }
    }
}
