using UnityEngine;
using System.Collections;
using Assets.Scripts._base;

public class PlayerOneRacketQuadBehavior : UnityBaseBehaviour
{
    public float MovementSpeed = 100;

    public override void FixedUpdate()
    {
        var axis = Input.GetAxisRaw(PlayerPrefs.GetString("isAI") == "true" ? "Vertical" : "Vertical1");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, axis) * MovementSpeed;
    }
}
