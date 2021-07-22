using UnityEngine;
using System.Collections;
using Assets.Scripts._base;

public class PlayerTwoRacketQuadBehavior : UnityBaseBehaviour
{

    public float MovementSpeed = 100;

    private GameObject ball { get { return GameObject.FindGameObjectWithTag("Ball"); } }
    
    public override void FixedUpdate()
    {
        if (!PlayerPrefs.HasKey("isAI") || PlayerPrefs.GetString("isAI") == "false")            
        {
            float axis = Input.GetAxisRaw("Vertical2");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, axis)*MovementSpeed;
        }
        else
        {
            Vector3 v3 = (ball.transform.position - transform.position).normalized;            
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, v3.y) * (MovementSpeed);

            //if (Mathf.Abs(transform.position.y - ball.transform.position.y) > 50)
            //{
            //    GetComponent<Rigidbody2D>().velocity = transform.position.y < ball.transform.position.y
            //        ? Vector2.up*MovementSpeed
            //        : Vector2.down * MovementSpeed;
            //}
            //else
            //{
            //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //}
        }
    }
}
