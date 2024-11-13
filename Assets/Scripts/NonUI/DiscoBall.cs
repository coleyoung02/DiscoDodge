using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PickRandom()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        if (rand == 0)
        {
            StartCoroutine(Shoot1());
        }
        else if (rand == 1)
        {
            StartCoroutine(Shoot2());
        }
        else if (rand == 2)
        {
            StartCoroutine(Shoot3());
        }
    }

    private IEnumerator Shoot1()
    {
        float degShift = 6f;
        int cycles = 2;
        int counter = 0;
        Rigidbody2D rb;
        float velocity = 2f;
        for (int i = 0; i < 360 / degShift * cycles; i += 1)
        {
            yield return new WaitForSeconds(.125f);
            rb = Instantiate(bullet, transform).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos(i * degShift * Mathf.Deg2Rad), Mathf.Sin(i * degShift * Mathf.Deg2Rad)) * velocity * 1.5f;
            if (counter % 6 == 0)
            {
                int r = Random.Range(0, 360 / (int)degShift);
                for (int j = -3; j <= 3; j += 1)
                {
                    rb = Instantiate(bullet, transform).GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(-Mathf.Cos((i + r + j / 2f) * degShift * Mathf.Deg2Rad), -Mathf.Sin((i + r + j / 2f) * degShift * Mathf.Deg2Rad)) * velocity * .9f;
                }
            }
            counter++;
        }
        PickRandom();
    }

    private IEnumerator Shoot2()
    {
        float degShift = 4f;
        int cycles = 2;
        Rigidbody2D rb;
        for (int i = 0; i < 360 / degShift * cycles; i += 1)
        {
            yield return new WaitForSeconds(.06f);
            rb = Instantiate(bullet, transform).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos(i * degShift * Mathf.Deg2Rad) * ((i % 2) * 2 - 1), Mathf.Sin(i * degShift * Mathf.Deg2Rad) * ((i % 2) * 2 - 1)) * 2f;
        }
        PickRandom();
    }

    private IEnumerator Shoot3()
    {
        int perCycle = 24;
        int bursts = 12;
        float degShift = 360f / perCycle / 2f;
        float velocity = 3f;
        Rigidbody2D rb;
        for (int i = 0; i < bursts; i += 1)
        {
            yield return new WaitForSeconds(.5f);
            for (float j = 0; j < 360; j += 360f / perCycle)
            {
                yield return new WaitForSeconds(.025f);
                rb = Instantiate(bullet, transform).GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(Mathf.Cos((i * degShift + j) * Mathf.Deg2Rad) * ((i % 2) * 2 - 1), Mathf.Sin((i * degShift + j) * Mathf.Deg2Rad) * ((i % 2) * 2 - 1)) * velocity;
            }
        }
        PickRandom();
    }
}
