using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ballLevel;
    public float radius;
    public float mass;
   public TrailRenderer trailRender;
    private void Awake()
    {
        transform.localScale = new Vector2(radius, radius);
        trailRender = GetComponent<TrailRenderer>();
        //GetComponent<Rigidbody2D>().mass = mass;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<HingeJoint2D>() != null && GetComponent<Rigidbody2D>().velocity.magnitude > 1)
        {

            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -1, 0) * 100);
        }
    }
}
