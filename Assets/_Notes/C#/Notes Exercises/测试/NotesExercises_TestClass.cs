using System;

namespace Yang.CSharp.Notes.Exercises
{
    internal class MyCustomAttribute : Attribute
    {
    }

    internal struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class Player
    {
        public int atk;
        public int def;

        public int hp;

        [MyCustom] public string name;

        public Position pos;
    }
}