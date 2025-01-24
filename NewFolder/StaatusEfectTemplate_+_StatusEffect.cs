using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGFight

{
    public abstract class StatusEffect// status efect template
    {
        public string Name { get; set; }
        public int Duration { get; set; } // Number of turns the effect lasts

        public StatusEffect(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        public virtual void ApplyEffect(Character target)
        {
            // Base logic (can be overridden by child classes)
        }

        public virtual void ReduceDuration(Character target)//build in timer
        {
            Duration--;
        }

        public bool IsExpired()
        {
            return Duration <= 0;
        }
    }
    public class DamageOverTime : StatusEffect
    {
        public int DamagePerTurn { get; set; }

        public DamageOverTime(string name, int duration, int damagePerTurn)
            : base(name, duration)
        {
            DamagePerTurn = damagePerTurn;
        }

        public override void ApplyEffect(Character target)
        {
            target.TakeDamage(DamagePerTurn);//this one deals constatnt damage throught few turns
            Console.WriteLine($"{target.Name} takes {DamagePerTurn} damage from {Name}!");
        }
    }
    class HealOverTime : StatusEffect
    {
        public int DamagePerTurn { get; set; }

        public HealOverTime(string name, int duration, int damagePerTurn)
            : base(name, duration)
        {
            DamagePerTurn = damagePerTurn;
        }

        public override void ApplyEffect(Character target)
        {
            target.Health += 5;
            if (target.MaxHealth < target.Health) //check if it dont go over max health
                target.Health = target.MaxHealth;
            Console.WriteLine($"{target.Name} restores {DamagePerTurn} health from {Name}!");//this one regenerate health through few turns 
        }
    }
    public class AttackPowerNerf : StatusEffect
    {
        public int AttackReduction { get; set; }

        public AttackPowerNerf(string name, int duration, int attackReduction)
            : base(name, duration)
        {
            AttackReduction = attackReduction;
        }

        public override void ApplyEffect(Character target)
        {
            target.AttackPower -= AttackReduction;//Reduce target AtackPowere value
            Console.WriteLine($"{target.Name}'s Attack Power is reduced by {AttackReduction} due to {Name}!");
        }

        public override void ReduceDuration(Character target)
        {
            base.ReduceDuration( target);

            // If the effect expires, restore the attack power
            if (IsExpired())
            {
                target.AttackPower += AttackReduction;
                Console.WriteLine($"{Name} effect has expired. {target.Name}'s Attack Power is restored by {AttackReduction}.");
            }
        }
    }
}
