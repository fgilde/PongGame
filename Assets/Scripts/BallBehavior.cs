using UnityEngine;
using System.Collections;
using Assets.Scripts._base;

public class BallBehavior : UnityBaseBehaviour
{

    public GameFlowController Game;
    public float StartSpeed = 30;
    public int MaxHitCount = 12;
    public float SpeedAccumulator = 20;
    public SoundController Sounds;

    private int hitCounter = 0;
    private bool needToCalculateCollision { get { return GetComponent<BoxCollider2D>().sharedMaterial.friction <= 0; } }
    private float Speed { get { return (StartSpeed + hitCounter * SpeedAccumulator); } }

    public void ResetBall(bool isPlayer1)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        transform.position = new Vector3(isPlayer1 ? -50 : 50, 0, 0);
        hitCounter = 0;
    }

    public void StartBallMovement(bool isPlayer1)
    {
        GetComponent<Rigidbody2D>().velocity = isPlayer1 ? Vector2.right * Speed : Vector2.left * Speed;
    }

    public override void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Player1RacketQuad")
            OnCollisionPlayerWithRacket(c, true);
        else if (c.gameObject.name == "Player2RacketQuad")
            OnCollisionPlayerWithRacket(c, false);
        else if (c.gameObject.name == "Player1WallQuad")
            OnCollisionWithPlayer1Wall(c);
        else if (c.gameObject.name == "Player2WallQuad")
            OnCollisionWithPlayer2Wall(c);
        else
            OnCollisionTopOrBottomWall();
    }

    private void OnCollisionTopOrBottomWall()
    {
        Sounds.WallSound.Play();
    }

    private void OnCollisionWithPlayer1Wall(Collision2D c)
    {
        StartCoroutine(Game.GoalPlayer2());
    }

    private void OnCollisionWithPlayer2Wall(Collision2D c)
    {
        StartCoroutine(Game.GoalPlayer1());
    }

    private float yOnHit(Vector2 ballPosition, Vector2 racketPosition, float racketHight)
    {
        return (ballPosition.y - racketPosition.y) / racketHight;
    }

    private void OnCollisionPlayerWithRacket(Collision2D c, bool isPlayer1)
    {
        float y = yOnHit(transform.position, c.transform.position, c.collider.bounds.size.y);
        Vector2 v = new Vector2(isPlayer1 ? 1 : -1, y).normalized;
        GetComponent<Rigidbody2D>().velocity = v * Speed;
        if (hitCounter < MaxHitCount)
            hitCounter++;

        Sounds.RacketSound.Play();
    }
}
// Hallo