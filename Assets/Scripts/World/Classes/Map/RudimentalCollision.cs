using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudimentalCollision
{
    public CollisionTag Tag;
    public Movement Movement;

    public RudimentalCollision(CollisionTag tag, Movement movement)
    {
        Tag = tag;
        Movement = movement;
    }
}
