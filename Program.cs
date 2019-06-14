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
            var finished = false;

            while (!finished)
            {
                var p = new Player("Joe", new Weapon("sword", 8));
                var enemies = new Character[] {
                new Kobold("Rikrak"),
                new Kobold("Okluk"),
                new Wolf(),
                new Orc("Morg"),
                new Orc("Grog"),
                new Drow("Morden", new Weapon("Lightning Bolt", 10))
            };
                foreach (var enemy in enemies)
                {
                    Fight(p, enemy);
                    if (!p.IsAlive)
                    {
                        break;
                    }
                }
                Console.WriteLine("Game over.\n");

                Console.Write("Do you want to play again? ");
                var answer = Console.ReadLine();
                if (answer.ToLower() == "no")
                {
                    finished = true;
                }
            }
        }


        public void Fight(Player p, Character m)
        {
            p.Engage(m);
            m.Engage(p);
            Console.WriteLine($"{p} encounters {m}.\n");
            while (m.IsAlive && p.IsAlive)
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

        public Player(String myName, Weapon weapon) : base(50, 13, weapon, 4)
        {
            _name = myName;
        }

        public override String ToString() => _name;
    }

    // Our scary monsters
    public class Kobold : Character
    {
        private readonly String _name;

        public Kobold(String name) : base(4, 12, new Weapon("claws", 4), 3)
        {
            _name = name;
        }

        public override String ToString() => "Kobold " + _name;
    }

    public class Orc : Character
    {
        private readonly String _name;

        public Orc(String name) : base(7, 10, new Weapon("scimitar", 7), 3)
        {
            _name = name;
        }

        public override String ToString() => "Orc " + _name;
    }


    public class Wolf : Character
    {
        public Wolf() : base(3, 12, new Weapon("teeth", 4), 0)
        { }

        public override string ToString() => "a wolf ";
    }


    public class Drow : Character
    {
        private readonly String _name;

        public Drow(String name, Weapon w) : base(15, 12, w, 5)
        {
            _name = name;
        }

        public override String ToString() => "Drow " + _name;
    }
}