using System;
namespace Day2
{
    public class Role
    {
        private int _hp;
        private int _maxHP;
        private string _name;
        private int _atk;
        private int _def;

        public int HP { get => _hp; }
        protected int SetHP { set => _hp = value; }
        public int MaxHP { get => _maxHP; }
        public int Atk { get => _atk; }
        public int Def { get => _def; }
        public string Name { get => _name; }

        protected Random random;


        public Role(int maxHP, string Name, int ATK, int DEF)
        {
            _name = Name;
            _maxHP = maxHP;
            _hp = _maxHP;
            _atk = ATK;
            _def = DEF;

            random = new Random();
        }
    }
}


