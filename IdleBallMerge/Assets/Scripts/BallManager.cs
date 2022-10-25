using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance = null;
    public static BallManager Instance => _instance;
    public List<Ball> ballList = new List<Ball>();
    public List<Ball> destroyBallList = new List<Ball>();
    private void Awake()
    {
        _instance = this;
    }
    public void DestroyListClear()
    {
        StartCoroutine(DelayDestroy());
    }
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < destroyBallList.Count; i++)
        {
            Ball bll = destroyBallList[0];
            destroyBallList.Remove(bll);

            BallCreator.Instance.totalMass -= bll.GetComponent<Ball>().mass;

            Destroy(bll.gameObject);
        }
    }
}
