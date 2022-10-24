using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBoundary : MonoBehaviour
{
    public int topSideBallCount = 0;
    int bounding = 10;
    void Awake()
    {
        foreach (Boundary bndry in GetComponentsInChildren<Boundary>())
        {
            bndry.topboundary = this;
        }
    }

    // Update is called once per frame
    public void BallEnter()
    {
        topSideBallCount++;
        if(topSideBallCount >= bounding)
        {
            StopSpawn();
        }
    }
    public void BallExit()
    {
        topSideBallCount--;
        if (topSideBallCount < bounding)
        {
            StartSpawn();
        }
    }
    void StopSpawn()
    {
        BallCreator.Instance.withoutBallSpawn = true;
    }
    void StartSpawn()
    {
        BallCreator.Instance.withoutBallSpawn = false;
    }
}
