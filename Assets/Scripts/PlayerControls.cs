using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedBoost;
    [SerializeField] private float dodgeTime;
    [SerializeField] private float dodgeCooldown;
    [SerializeField] private int maxHealth;
    private float boostMult = 1f;
    private bool hasBoost = true;
    private bool isImmune = false;
    private bool hitImmune = false;
    private Vector2 boostedDir = Vector2.zero;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector2(1, 0);
        }
        if (hasBoost && Input.GetKeyDown(KeyCode.Space))
        {
            boostedDir = moveDir;
            StartCoroutine(Boost());
        }
        if (isImmune)
        {
            moveDir = boostedDir;
        }
        moveDir = BoundMovement(moveDir);
        
        rb.velocity = moveDir.normalized * moveSpeed * boostMult;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hit");
            Destroy(collision.gameObject);
            if (!(isImmune || hitImmune))
            {
                currentHealth--;
                if (currentHealth <= 0)
                {
                    SceneManager.LoadScene("RIP");
                }
                StartCoroutine(HitImmunity());
            }
        }
        else
        {

            Debug.Log("hit with " + collision.gameObject.tag);
        }
    }

    private IEnumerator Boost()
    {
        hasBoost = false;
        boostMult = speedBoost;
        isImmune = true;
        for (float f = 0; f < dodgeTime; f += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            transform.localRotation = Quaternion.Euler(0, 0, f * 360f * -Mathf.Sign(boostedDir.x) / dodgeTime);
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        boostMult = 1f;
        isImmune = false;
        for (float f = 0; f < dodgeCooldown; f += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            // update dodge ui here
        }
        hasBoost = true;
    }

    private IEnumerator HitImmunity()
    {
        hitImmune = true;
        yield return new WaitForSeconds(.5f);
        hitImmune = false;
    }

    private Vector2 BoundMovement(Vector2 moveDir)
    {
        Vector2 newDir = new Vector2(moveDir.x, moveDir.y);
        if (transform.position.x > 8.5f)
        {
            newDir.x = Mathf.Min(moveDir.x, 0);
        }
        if (transform.position.y > 4.8f)
        {
            newDir.y = Mathf.Min(moveDir.y, 0);
        }
        if (transform.position.x < -8.5f)
        {
            newDir.x = Mathf.Max(moveDir.x, 0);
        }
        if (transform.position.y < -4.8f)
        {
            newDir.y = Mathf.Max(moveDir.y, 0);
        }
        return newDir;
    }


}
