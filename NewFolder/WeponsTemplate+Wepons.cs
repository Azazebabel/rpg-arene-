using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGFight
{
       public abstract class Weapon
        {
            public string Name { get; }
            public int AditionalDamage { get; }

            public Weapon(string name, int aditionalDamage)
            {
                Name = name;
                AditionalDamage = aditionalDamage;
            }

            public abstract int DealDamage(Random random, Character user);
            public abstract void ApplySpecialEffect(Character target, Random random);
        }

   public class Sword : Weapon
    {
        public Sword() : base("Sword", 15) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next(AditionalDamage + user.AttackPower - 3, AditionalDamage+ user.AttackPower + 3);
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {
            // No special effect for a basic sword
            Console.WriteLine($"{Name} has no special effect.");
        }
    }
  public   class Fist : Weapon
    {
        public Fist() : base("Fist", 0) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next( user.AttackPower - 3, AditionalDamage);
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {
            // No special effect for a basic sword
            Console.WriteLine($"{Name} has no special effect.");
        }
    }


}
