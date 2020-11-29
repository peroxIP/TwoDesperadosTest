using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Spawner
{
    public int VisionRange;
    public float fireDelay;

    private float SeekDelay = 0.1f;
    private RudimentalCollision bulletCollision;

    public override void CustomStart()
    {
        bulletCollision = new RudimentalCollision(CollisionTag.EnemyBullet, null, new List<CollisionTag> { CollisionTag.Player, CollisionTag.PlayerBullet });

        StartCoroutine(Seek(SeekDelay));
    }


    IEnumerator Seek(float time)
    {
        yield return new WaitForSeconds(time);
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        Direction d = world.ViewSuroundingTiles(Position, VisionRange, bulletCollision);
        

        switch (d)
        {
            case Direction.NORTH:
                Debug.Log("SHOOTING IN DIR" + d);
                ShootUp();
                break;
            case Direction.EAST:
                ShootRight();
                break;
            case Direction.SOUTH:
                ShootDown();
                break;
            case Direction.WEST:
                ShootLeft();
                break;
            default:
                StartCoroutine(Seek(SeekDelay));
                break;
        }
    }
    
    private void Shoot(Vector2Int dir)
    {
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        GameObject spawn = Spawn(Start: false);

        SpriteRenderer spr = spawn.GetComponent<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#44B7BFFF", out Color color);
        spr.color = color;

        BulletMovement bulletMovement = spawn.GetComponent<BulletMovement>();

        bulletMovement.movementDelay = 0.3f;

        bulletMovement.SetDirection(dir);
        bulletMovement.SetCollisionTag(bulletCollision);
        bulletMovement.CustomStart();
        StartCoroutine(Seek(fireDelay));
    }

    public void ShootUp()
    {
        Shoot(Vector2Int.up);
    }

    public void ShootDown()
    {
        Shoot(Vector2Int.down);
    }

    public void ShootLeft()
    {
        Shoot(Vector2Int.left);
    }

    public void ShootRight()
    {
        Shoot(Vector2Int.right);
    }

    public override void SubscribeToController()
    {
        
    }

}
