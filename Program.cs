using System;

namespace Battle
{
    // The main class that starts and controls the game
    public class Game
    {
        public static void Main()
        {
            var controller = new Game();
            controller.RunGame();
        }

        public void RunGame()
        {
            var p = new Player("Joe", new Weapon("sword", 8));
            var k1 = new Kobold("Rikrak");
            Fight(p, k1);
            if (p.IsAlive)
            {
                var k2 = new Kobold("Okluk");
                Fight(p, k2);
            }
            Console.WriteLine("Game over.\n");
        }


        public void Fight(Player p, Character m)
        {
            p.Engage(m);
            m.Engage(p);
            Console.WriteLine($"{p} encounters {m}.\n");
            while (m.IsAlive&& p.IsAlive)
            {
                p.Attack();
                if (m.IsAlive)
                {
                    m.Attack();
                    if (!p.IsAlive)
                        Console.WriteLine($"{p} is dead.\n");
                }
                else
                    Console.WriteLine($"{m} is dead.\n");
            }
        }
    }


     // A character's weapon
    public class Weapon
    {
        private readonly Random _rand;
        private readonly String _name;
        private readonly int _maxDamage;

        // Constructor for objects of class Weapon
        public Weapon(String name, int power)
        {
            _name = name;
            _maxDamage = power;
            _rand = new Random();
        }

        public override String ToString() => _name;

        public int DamageAmount() => _rand.Next(_maxDamage) + 1;
    }


    // Represents a character (player or non-player) in the game.
    public class Character
    {
        private readonly Random _rand;
        private readonly Weapon _weapon;
        private readonly int _toHit;
        private readonly int _armor;
        private Character _adversary;
        private int _health;

        // Constructor for objects of class Character
        public Character(int health, int toHit, Weapon weapon, int armor)
        {
            _health = health;
            _toHit = toHit;
            _weapon = weapon;
            _armor = armor;
            _rand = new Random();
        }

        public override String ToString() => "Character";

        public void Engage(Character c)
        {
            _adversary = c;
        }

        public int Armor => _armor;

        public Boolean CheckForHit() => _rand.Next(20) + 1 >= _toHit + _adversary.Armor;

        public void Attack()
        {
            Console.WriteLine($"{this} attacks {_adversary} with {_weapon}.");
            if (CheckForHit())
            {
                _adversary.Damage(_weapon.DamageAmount());
            }
            else
            {
                Console.WriteLine($"{this} misses.");
            }
            Console.WriteLine();
        }

        public void Damage(int amount)
        {
            Console.WriteLine($"{this} receives damage of {amount}.");
            _health -= amount;
        }

        public Boolean IsAlive => _health > 0;
    }


    // Represents the player of the game
    public class Player : Character
    {
        private readonly String _name;

        public Player(String myName, Weapon weapon) : base(10, 12, weapon, 1)
        {
            _name = myName;
        }

        public override String ToString() => _name;
    }

    // Our scary monster
    public class Kobold : Character
    {
        private readonly String _name;

        public Kobold(String name) : base(4, 12, new Weapon("claws", 4), 3)
        {
            _name = name;
        }

        public override String ToString() => "Kobold " + _name;
    }
}