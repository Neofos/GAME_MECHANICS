using System;

namespace CharacterMovement
{
    abstract class Being // Класс для врагов и главного героя.
    {
        protected string beingIcon;
        protected ConsoleColor beingColor;
        protected int beingPosX, beingPosY;
        protected char[] impassableObjects;

        public abstract void BeingMove();
    }
}
