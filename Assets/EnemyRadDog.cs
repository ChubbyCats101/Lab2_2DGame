using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyRedDog : Enemy
{
    public float speed = 1.0f;
    private int direction = -1;

    public Transform GroundChecker;
    public Transform WallChecker;
    public LayerMask layerToCheck;

    private bool detectedGround;
    private bool detectedWall;
    public float radius;

    PlayerMoveControls playerMoveControls;
    void Start() { }
    void FixedUpdate()
    {
        flip();
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private void flip()
    {
        detectedGround = Physics2D.OverlapCircle(
            GroundChecker.position, radius, layerToCheck);
        detectedWall = Physics2D.OverlapCircle(
            WallChecker.position, radius, layerToCheck);

        if (detectedWall || !detectedGround)
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundChecker.position, radius);
        Gizmos.DrawWireSphere(WallChecker.position, radius);
    }
}
