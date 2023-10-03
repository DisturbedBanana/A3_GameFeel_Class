using UnityEngine;

namespace IIMEngine.Movements2D
{
    public interface IMove2DOrientWriter
    {
        Vector2 OrientDir { get; set; }
        
        float OrientX { get; set; }
        
        float OrientY { get; set; }
    }
}