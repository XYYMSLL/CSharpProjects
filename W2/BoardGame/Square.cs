using System;
namespace BoardGame
{
    public class Square
    {
        private bool _player = false;
        private bool _item = false;
        private Square _next;

        public Square(bool item)
        {
            Item = item;
        }

        public bool Player { get => _player; set => _player = value; }
        public bool Item { get => _item; set => _item = value; }
        public Square Next { get => _next; set => _next = value; }
    }
}
