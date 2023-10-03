using UnityEngine;

namespace IIMEngine.Movements2D
{
    public interface IMove2DOrientReader
    {
        Vector2 OrientDir { get; }
        
        float OrientX { get; }
        
        float OrientY { get; }
    }
}