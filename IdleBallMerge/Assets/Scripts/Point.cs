﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Point : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] int alpha;
    [Range(0, 400)] [SerializeField] float UpwardSpeed;
    [Range(0, 4)] [SerializeField] float SimulationSpeed;
    float speedFactor;
    float velocityFactor;
    public TextMeshProUGUI PointText;
    float xSpeed;
    private void Start()
    {
        speedFactor = Random.Range(0.5f, 1f);
        velocityFactor = Random.Range(1f, 4f);
        xSpeed = Random.Range(-4f, 4f);
        //foreach (var txt in GetComponentsInChildren<TextMeshProUGUI>())
        //{
        //    PointText = txt;
        //}
        StartCoroutine(pointUp());
        StartCoroutine(colorSet(alpha));
    }
    //public void TextInit(int)
    //{

    //}
    IEnumerator pointUp()
    {

        float counter = 0;
        float counter2 = 0;
        float spd = 0;
        float x = 0;
        while (counter < Mathf.PI / 2)
        {
            counter += speedFactor * SimulationSpeed * Time.deltaTime;

            if (counter2 < Mathf.PI / 2)
            {
                counter2 += 2 * speedFactor * SimulationSpeed * Time.deltaTime;
                x = Mathf.Sin(counter2);

            }
            spd = Mathf.Cos(counter);
            spd *= UpwardSpeed * velocityFactor;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(xSpeed * (1 - x), velocityFactor, 0), Time.deltaTime * spd);

            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator colorSet(float _alpha)
    {
        float counter = 0;
        while (counter < Mathf.PI / 2)
        {
            counter += speedFactor * SimulationSpeed * Time.deltaTime;
            float currentAlpha = (counter / (Mathf.PI / 2));
            PointText.color = new Color(PointText.color.r, PointText.color.g, PointText.color.b, Mathf.Abs(_alpha - currentAlpha));
            yield return null;
        }
        Destroy(gameObject);
    }
}
