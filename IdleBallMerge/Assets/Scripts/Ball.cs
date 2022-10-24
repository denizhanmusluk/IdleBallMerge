using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ballLevel;
    public float radius;
    public float mass;
    private void Awake()
    {
        transform.localScale = new Vector2(radius, radius);
        //GetComponent<Rigidbody2D>().mass = mass;
    }
}
