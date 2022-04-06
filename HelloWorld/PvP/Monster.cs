using System;
namespace PvP
{
	public class Monster : Role, IRole
	{
        public Monster(int maxHP, string Name, int ATK, int DEF) : base(maxHP, Name, ATK, DEF)
		{
		}

        public void RandomMove(Player otherPlayer)
        {
            int rNum = random.Next(0, 100);
            if (rNum > 60)
            {
                Attack(otherPlayer);
            }
            else
            {
                Wait(otherPlayer.Name);
            }
        }

        public void RandomMove(Monster otherPlayer)
        {
            int rNum = random.Next(0, 100);
            if (rNum > 60)
            {
                Attack(otherPlayer);
            }
            else
            {
                Wait(otherPlayer.Name);
            }
        }

        private void Attack(Player otherPlayer)
        {
            otherPlayer.TakeDamage(Atk);
            Console.WriteLine("{0}攻击了{1}，造成了{2}点伤害，{1}还剩{3}点HP", Name, otherPlayer.Name, Atk - otherPlayer.Def, otherPlayer.HP);
        }

        private void Attack(Monster otherPlayer)
        {
            otherPlayer.TakeDamage(Atk);
            Console.WriteLine("{0}攻击了{1}，造成了{2}点伤害，{1}还剩{3}点HP", Name, otherPlayer.Name, Atk - otherPlayer.Def, otherPlayer.HP);
        }

        private void Wait(string otherName)
        {
            Console.WriteLine("{0}看着{1}", Name, otherName);
        }

        public void TakeDamage(int damage)
        {
            int actualDmg = (damage - Def) <= 0 ? 1 : (damage - Def);
            SetHP = (HP - actualDmg) < 0 ? 0 : (HP - actualDmg);

            if (HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Console.WriteLine("Mrgllll glrrm gl!");
        }
    }
}

