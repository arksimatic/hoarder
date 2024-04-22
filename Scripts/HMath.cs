using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
    internal class HMath
    {
        public static Vector2 TrimToCircle(Vector2 destination, Vector2 center, Single radius)
        {
            Single distance = center.DistanceTo(destination);
            if (distance > radius)
            {
                Single ration = radius / distance;
                Single dx = destination.X - center.X;
                Single dy = destination.Y - center.Y;
                return new Vector2(center.X + dx * ration, center.Y + dy * ration);
            }
            else
                return destination;
        }

        //public static Vector2 TrimToPlayerCircle(Vector2 mousePosition, Vector2 parentPosition, Single radius)
        //{
        //    Vector2 direction = (mousePosition - parentPosition).Normalized();
        //    Single distance = parentPosition.DistanceTo(mousePosition);
        //    if (distance > radius)
        //        mousePosition = parentPosition + direction * radius;
        //    return mousePosition;
        //}
    }
}
