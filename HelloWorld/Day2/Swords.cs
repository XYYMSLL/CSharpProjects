using System;
namespace Day2
{
	public class WoodenSword : Weapon
	{
		public WoodenSword() : base(WeaponEnum.WoodenSword, "木剑", 10, 0)
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
        private Random random = new Random();
		public SteelSword() : base(WeaponEnum.SteelSword, "钢剑", 8, 15)
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
		private Random random = new Random();
		public MithrilSword() : base(WeaponEnum.MithrilSword, "秘银剑", 8, 15)
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

