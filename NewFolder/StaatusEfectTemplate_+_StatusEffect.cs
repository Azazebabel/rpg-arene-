using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGFight

{
    class StatusEffect
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

        public void ReduceDuration()
        {
            Duration--;
        }

        public bool IsExpired()
        {
            return Duration <= 0;
        }
    }
    class DamageOverTime : StatusEffect
    {
        public int DamagePerTurn { get; set; }

        public DamageOverTime(string name, int duration, int damagePerTurn)
            : base(name, duration)
        {
            DamagePerTurn = damagePerTurn;
        }

        public override void ApplyEffect(Character target)
        {
            target.TakeDamage(DamagePerTurn);
            Console.WriteLine($"{target.Name} takes {DamagePerTurn} damage from {Name}!");
        }
    }


}
