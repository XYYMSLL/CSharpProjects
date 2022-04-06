using System;
namespace PvP
{
	public class Weapon : IWeapon
	{
		private WeaponEnum _type;
		private string _name;
		private int _atk;
		private int _critRate;

		public string Name { get => _name; }
		public int Atk { get => _atk; }
		public int CritRate { get => _critRate; }

		public Weapon(WeaponEnum type, string Name, int Atk, int critRate)
		{
			_type = type;
			_name = Name;
			_atk = Atk;
			_critRate = critRate;
		}

        public virtual int Attack(out string extraMsg)
        {
			extraMsg = "";
			return 10;
        }
    }
}

