using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooting
{
    public override void CustomStart()
    {
        
    }

    public override void SubscribeToController()
    {
        gameController.ShootUp.onClick.AddListener(ShootUp);
        gameController.ShootDown.onClick.AddListener(ShootDown);
        gameController.ShootLeft.onClick.AddListener(ShootLeft);
        gameController.ShootRight.onClick.AddListener(ShootRight);
    }

    private void Shoot(Vector2Int dir)
    {
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        GameObject spawn = Spawn();
        BulletMovement bulletMovement = spawn.GetComponent<BulletMovement>();

        bulletMovement.SetDirection(dir);
        bulletMovement.CustomStart();
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
}
