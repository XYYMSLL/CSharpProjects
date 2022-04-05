using System;
namespace PvP
{
    public class Player : IPlayer
    {
        private int _hp;
        private string _name;

        public int HP { get => _hp; }
        public string Name { get => _name; }

        public Player(string name)
        {
            _name = name;
            _hp = 100;
        }

        public void WodenSword(Player otherPlayer)
        {
            otherPlayer.TakeDamage(10);
            Console.WriteLine("{0}使用了木剑，对{1}造成了10点伤害，{1}还有{2}点HP", _name, otherPlayer.Name, otherPlayer.HP);
        }

        public void TakeDamage(int damage)
        {
            _hp = _hp - damage < 0 ? 0 : _hp - damage;
        }

        public void Heal()
        {
            _hp = _hp + 10 > 100 ? 100 : _hp + 10;
            Console.WriteLine("{0}使用了治疗，恢复了10点HP，{0}还有{1}点HP", _name, _hp);
        }

        public void SteelSword(Player otherPlayer)
        {
            otherPlayer.TakeDamage(15);
            Console.WriteLine("{0}使用了钢剑，对{1}造成了15点伤害，{1}还有{2}点HP", _name, otherPlayer.Name, otherPlayer.HP);
        }

        public void MithrilSword(Player otherPlayer)
        {
            otherPlayer.TakeDamage(30);
            Console.WriteLine("{0}使用了秘银剑，对{1}造成了30点伤害，{1}还有{2}点HP", _name, otherPlayer.Name, otherPlayer.HP);
        }

        public void RandomMove(Player otherPlayer, Random random)
        {
            int rNum = random.Next(0, 10);
            if (rNum >= 8)
            {
                Heal();
            }
            else if (rNum >= 7)
            {
                MithrilSword(otherPlayer);
            }
            else if (rNum >= 4)
            {
                SteelSword(otherPlayer);
            }
            else
            {
                WodenSword(otherPlayer);
            }
        }
    }
}
