using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float colorPeriod;
    // Start is called before the first frame update
    void Start()
    {
        float colorVal = (Time.time * 6 / colorPeriod) % 6; //(Mathf.Sin(Time.time * Mathf.PI / colorPeriod) + 1) * 3;
        if (colorVal < 1)
        {
            sr.color = new Color(0f, colorVal, 1f);
        }
        else if (colorVal < 2)
        {
            sr.color = new Color(0f, 1f, 1 - colorVal % 1);
        }
        else if (colorVal < 3)
        {
            sr.color = new Color(colorVal % 1, 1f, 0f);
        }
        else if (colorVal < 4)
        {
            sr.color = new Color(1f, 1 - colorVal % 1, 0f);
        }
        else if (colorVal < 5)
        {
            sr.color = new Color(1f, 0f, colorVal % 1);
        }
        else if (colorVal < 6)
        {
            sr.color = new Color(1 - colorVal % 1, 0f, 1f);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
