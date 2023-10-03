using UnityEngine;

namespace IIMEngine.Movements2D
{
    public interface IMove2DDirReader
    {
        Vector2 MoveDir { get; }
    }
}