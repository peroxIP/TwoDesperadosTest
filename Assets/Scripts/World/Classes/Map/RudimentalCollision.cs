using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudimentalCollision
{
    public CollisionTag Tag;
    public Movement Movement;
    public List<CollisionTag> CollidesWith;

    public RudimentalCollision(CollisionTag tag, Movement movement, List<CollisionTag> collidesWith)
    {
        Tag = tag;
        Movement = movement;
        CollidesWith = collidesWith;
    }
}
