using System;
namespace Day2
{
    public interface IRole
    {
        public void RandomMove(Player otherPlayer);
        public void RandomMove(Monster otherPlayer);
        public void TakeDamage(int damage);
    }
}
