using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ballLevel;
    public float radius;
    public float mass;
   public TrailRenderer trailRender;
    bool ballFallActive = false;
    public GameObject _coinPoint;
    public int coinValue;
   public bool firstCollisionDetection = false;
    bool delaycollisionActive = false;
    private void Awake()
    {
        firstCollisionDetection = false;
        transform.localScale = new Vector2(radius, radius);
        trailRender = GetComponent<TrailRenderer>();
        //GetComponent<Rigidbody2D>().mass = mass;
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.5f);
        delaycollisionActive = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<HingeJoint2D>() != null && GetComponent<Rigidbody2D>().velocity.magnitude > 1)
        {

            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -1, 0) * 100);
        }
        if (firstCollisionDetection && delaycollisionActive)
        {
            BallCreator.Instance.RopesMassUpdate();
            firstCollisionDetection = false;
            Coin(transform, coinValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "topbound" && !ballFallActive)
        {
            BallManager.Instance.ballList.Remove(this);
            BallCreator.Instance.totalMass -= mass;
            ballFallActive = true;
            Destroy(this.gameObject, 3f);
        }
    }

    void Coin(Transform instPos, int Coin)
    {
        var point = Instantiate(_coinPoint, instPos.position, Quaternion.identity);
        point.GetComponent<Point>().PointText.text = "$" + Coin.ToString();
    }
}
