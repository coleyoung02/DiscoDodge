using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            Rigidbody2D rb = Instantiate(bullet, transform).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time)) * 2f;
        }
    }
}
