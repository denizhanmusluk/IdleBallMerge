using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance = null;
    public static CameraManager Instance => _instance;

    [SerializeField] List<Transform> levels = new List<Transform>();
    Transform currentLevelPos;
    Vector3 newCameraPos;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        currentLevelPos = levels[PlayerPrefs.GetInt("level")];
        newCameraPos = new Vector3(currentLevelPos.position.x, currentLevelPos.position.y, transform.position.z);
        transform.position = newCameraPos;
    }

    // Update is called once per frame
    public void CameraPositionSet()
    {
        StartCoroutine(MoveCamera());
    }
    IEnumerator MoveCamera()
    {
        currentLevelPos = levels[PlayerPrefs.GetInt("level")];
        Vector3 oldCameraPos = transform.position;
        newCameraPos = new Vector3(currentLevelPos.position.x, currentLevelPos.position.y, transform.position.z);
        float counter = 0f;
        while(counter < 1f)
        {
            counter += 0.25f * Time.deltaTime;
            transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, counter);
            yield return null;
        }
        transform.position = newCameraPos;
    }
}
