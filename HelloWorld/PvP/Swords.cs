using System;
namespace PvP
{
	public class WoodenSword : Weapon
	{
		public WoodenSword() : base(WeaponEnum.WoodenSword, "WoodenSword", 10, 0)
		{
		}

        public override int Attack(out string extraMsg)
        {
			extraMsg = "";
            return 10;
        }
    }

	public class SteelSword : Weapon
	{
		Random random = new Random();
		public SteelSword() : base(WeaponEnum.SteelSword, "SteelSword", 8, 15)
		{ }

        public override int Attack(out string extraMsg)
        {
			int dmg = random.Next(8, 16);
			extraMsg = "";
			if (random.Next(0, 100) < 15)
			{
				extraMsg = "暴击，";
				dmg *= 2;
			}
            return dmg;
        }
    }

	public class MithrilSword : Weapon
	{
		Random random = new Random();
		public MithrilSword() : base(WeaponEnum.MithrilSword, "MithrilSword", 8, 15)
		{ }

        public override int Attack(out string extraMsg)
        {
			int dmg = 20;
			extraMsg = "";
			if (random.Next(0, 10) > 5)
			{
				extraMsg = "造成了额外伤害，";
				dmg = 30;
			}
            return dmg;
        }
    }
}

