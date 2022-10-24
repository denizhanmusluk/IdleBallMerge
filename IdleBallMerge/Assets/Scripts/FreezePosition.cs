using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FreezePosition : MonoBehaviour
{
    event Action onUpdate;
    Vector3 firstLocalPosition;
    Vector3 firstWorldPosition;
    public enum freezeState { local,world};
    public freezeState freeze;
    void Awake()
    {
        switch (freeze)
        {
            case freezeState.local:
                {
                    firstLocalPosition = transform.localPosition;
                    onUpdate += LocalPosUpdate;
                }
                break;

            case freezeState.world:
                {
                    firstWorldPosition = transform.position;
                    onUpdate += WorldPosUpdate;
                }
                break;
        }
        firstLocalPosition = transform.localPosition;
    }

    void WorldPosUpdate()
    {
        transform.position = firstWorldPosition;
    }
    void LocalPosUpdate()
    {
        transform.localPosition = firstLocalPosition;
    }
    private void Update()
    {
        onUpdate?.Invoke();
    }
}
