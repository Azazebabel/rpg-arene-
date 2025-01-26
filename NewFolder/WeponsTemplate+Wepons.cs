using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGFight
{
       public abstract class Weapon//wepon template
        {
            public string Name { get; }
            public int AditionalDamage { get; }

            public Weapon(string name, int aditionalDamage)
            {
                Name = name;
                AditionalDamage = aditionalDamage;
            }

            public abstract int DealDamage(Random random, Character user);
            public abstract void ApplySpecialEffect(Character target, Random random);//unused left for future uses 
        }
    public class Spear : Weapon
    {
        public Spear() : base("Spear", 15) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next(user.AttackPower, AditionalDamage + user.AttackPower );//damage logic
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {

        }
    }
    public class Fail : Weapon
    {
        public Fail() : base("Fail", 0) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next( 0, AditionalDamage + user.AttackPower + 40);//damage logic
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {

        }
    }
    public class Sword : Weapon
    {
        public Sword() : base("Sword", 10) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next(AditionalDamage + user.AttackPower - 5, AditionalDamage+ user.AttackPower + 3);//Damage logic
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {
           
        }
    }
  public   class Fist : Weapon
    {
        public Fist() : base("Fist", 0) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return random.Next( user.AttackPower - 3, user.AttackPower);//Damage logic
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {
            
        }
    }

    public class MagicSword : Weapon
    {
        public MagicSword() : base("Magic sword", 50) { }

        public override int DealDamage(Random random, Character user)
        {
            // Slight variation in damage
            return (user.AttackPower + AditionalDamage);//Damage logic
        }

        public override void ApplySpecialEffect(Character target, Random random)
        {

        }
    }

}
