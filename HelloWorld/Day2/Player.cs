using System;
namespace Day2
{
    public class Player : Role, IRole
    {
        private Weapon _weapon;
        public int[] Position { set; get; } = new int[] { 0, 0 };

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;

            Console.WriteLine("获得了{0}", weapon.Name);
        }

        public Player(int maxHP, string Name, int ATK, int DEF) : base(maxHP, Name, ATK, DEF)
        {
            _weapon = null;
        }

        public void UseWeapon(Player otherPlayer)
        {
            if (_weapon == null)
            {
                otherPlayer.TakeDamage(Atk);
                PrintAtkResult("拳头", Atk, otherPlayer);
                return;
            }
            int dmg = _weapon.Attack(out string extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult(_weapon.Name, dmg, otherPlayer);
        }
        public void UseWeapon(Monster otherPlayer)
        {
            if (_weapon == null)
            {
                otherPlayer.TakeDamage(Atk);
                PrintAtkResult("拳头", Atk, otherPlayer);
                return;
            }
            int dmg = _weapon.Attack(out string extraMsg);
            otherPlayer.TakeDamage(dmg);
            PrintAtkResult(_weapon.Name, dmg, otherPlayer);
        }

        public void Heal()
        {
            SetHP = HP + 10 > MaxHP ? MaxHP : HP + 10;
            Console.WriteLine("{0}使用了治疗，恢复了10点HP，{0}还有{1}点HP", Name, HP);
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
            else
            {
                UseWeapon(otherPlayer);
            }
        }

        public void RandomMove(Monster otherPlayer)
        {
            int rNum = random.Next(0, 10);
            if (rNum >= 8)
            {
                Heal();
            }
            else
            {
                UseWeapon(otherPlayer);
            }
        }
    }
}
