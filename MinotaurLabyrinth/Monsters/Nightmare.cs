namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a custom monster in the game.
    /// </summary>
    public class Nightmare : Monster
    {
        private int Health { get; set; } = 100;


        public override void Activate(Hero hero, Map map)
        {
            const int heroAttackDamageWithSword = 20;
            const int heroAttackDamageWithoutSword = 10;
            const int monsterRegularAttackDamage = 10;
            const int monsterSpecialAttackDamage = 30;

            bool heroHasSword = hero.HasSword;
            int turnCount = 1;

            while (hero.Health > 0 && Health > 0)
            {
                // Monster attacks the hero
                if (hero.Health <= 0)
                {
                    break; // Hero defeated
                }

                if (turnCount % 3 == 0)
                {
                    hero.Health -= monsterSpecialAttackDamage;
                    Console.WriteLine($"Nightmare unleashes a powerful attack on the hero! The hero loses {monsterSpecialAttackDamage} health.");
                }
                else
                {
                    hero.Health -= monsterRegularAttackDamage;
                    Console.WriteLine($"Nightmare attacks the hero! The hero loses {monsterRegularAttackDamage} health.");
                }

                if (hero.Health <= 0)
                {
                    break; // Hero defeated
                }

                // Hero attacks the monster
                if (heroHasSword)
                {
                    Health -= heroAttackDamageWithSword;
                    Console.WriteLine($"The hero strikes Nightmare with a mighty blow! Nightmare loses {heroAttackDamageWithSword} health.");
                }
                else
                {
                    Health -= heroAttackDamageWithoutSword;
                    Console.WriteLine($"The hero attacks Nightmare! Nightmare loses {heroAttackDamageWithoutSword} health.");
                }

                if (Health <= 0)
                {
                    break; // Monster defeated
                }

                // Increment the turn count
                turnCount++;
            }

            if (hero.Health <= 0)
            {
                hero.Kill("Hero defeated"); // Hero defeated
            }
            else
            {
                // Monster defeated
                // Restore hero's health to 100
                hero.Health = 100;

                // Give the hero a sword if they don't have one
                if (!heroHasSword)
                {
                    hero.HasSword = true;
                }


            }
        }



        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 1)
            {
                ConsoleHelper.WriteLine("It is getting darker, seems like a Nightmare ahead! Be Careful!", ConsoleColor.Red);
                return true;
            }
            return false;
        }

        public override DisplayDetails Display()
        {
            return new DisplayDetails("[N]", ConsoleColor.Red);
        }
    }
}
