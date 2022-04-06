using System;
namespace PvP
{
    public class Player : Role, IRole
    {
        private MithrilSword _mithrilSword = new MithrilSword();
        private SteelSword _steelSword = new SteelSword();
        private WoodenSword _woodenSword = new WoodenSword();

        public Player(int maxHP, string Name, int ATK, int DEF) : base(maxHP, Name, ATK, DEF)
        {
        }

        public void UseWodenSword(Player otherPlayer)
        {
            int dmg = _woodenSword.Attack(out string extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("木剑", dmg, otherPlayer);
        }
        public void UseWodenSword(Monster otherPlayer)
        {
            int dmg = _woodenSword.Attack(out string extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("木剑", dmg, otherPlayer);
        }

        public void Heal()
        {
            SetHP = HP + 10 > MaxHP ? MaxHP : HP + 10;
            Console.WriteLine("{0}使用了治疗，恢复了10点HP，{0}还有{1}点HP", Name, HP);
        }

        public void UseSteelSword(Player otherPlayer)
        {
            string criticalStr;
            int dmg = _steelSword.Attack(out criticalStr);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("钢剑", dmg, otherPlayer, criticalStr);
        }
        public void UseSteelSword(Monster otherPlayer)
        {
            string criticalStr;
            int dmg = _steelSword.Attack(out criticalStr);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("钢剑", dmg, otherPlayer, criticalStr);
        }

        public void UseMithrilSword(Player otherPlayer)
        {
            string extraMsg;
            int dmg = _mithrilSword.Attack(out extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("秘银剑", dmg, otherPlayer, extraMsg);
        }
        public void UseMithrilSword(Monster otherPlayer)
        {
            string extraMsg;
            int dmg = _mithrilSword.Attack(out extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult("秘银剑", dmg, otherPlayer, extraMsg);
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

        private void PrintAtkResult(string EName, int damage, Player other, string extraMsg = "")
        {
            Console.WriteLine("{0}使用了{4}，{5}对{1}造成了{3}点伤害，{1}还有{2}点HP", Name, other.Name, other.HP, damage-other.Def, EName, extraMsg);
        }
        private void PrintAtkResult(string EName, int damage, Monster other, string extraMsg = "")
        {
            Console.WriteLine("{0}使用了{4}，{5}对{1}造成了{3}点伤害，{1}还有{2}点HP", Name, other.Name, other.HP, damage-other.Def, EName, extraMsg);
        }

        private void Die()
        {
            Console.WriteLine("AWSL");
        }

        public void RandomMove(Player otherPlayer)
        {
            int rNum = random.Next(0, 10);
            if (rNum >= 8)
            {
                Heal();
            }
            else if (rNum >= 7)
            {
                UseMithrilSword(otherPlayer);
            }
            else if (rNum >= 4)
            {
                UseSteelSword(otherPlayer);
            }
            else
            {
                UseWodenSword(otherPlayer);
            }
        }

        public void RandomMove(Monster otherPlayer)
        {
            int rNum = random.Next(0, 10);
            if (rNum >= 8)
            {
                Heal();
            }
            else if (rNum >= 7)
            {
                UseMithrilSword(otherPlayer);
            }
            else if (rNum >= 4)
            {
                UseSteelSword(otherPlayer);
            }
            else
            {
                UseWodenSword(otherPlayer);
            }
        }
    }
}
