using System;
namespace PvP
{
    public interface IPlayer
    {
        public void RandomMove(Player otherPlayer, Random random);
        public void WodenSword(Player otherPlayer);
        public void SteelSword(Player otherPlayer);
        public void MithrilSword(Player otherPlayer);
        public void TakeDamage(int damage);
        public void Heal();
    }
}
