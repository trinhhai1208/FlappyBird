using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float flapForce = 6f;
    private Rigidbody2D rb;
    private bool isDead = false;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (isDead) return; //If dead will be end the game

        //Space or left-click to flap the wings
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        float angle = Mathf.Clamp(rb.velocity.y * 5f, -90f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Flap()
    {
        rb.velocity = Vector2.up * flapForce;
        anim.SetTrigger("Flap");
        AudioManager.instance.PlayFlap();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 2f;
        AudioManager.instance.PlayHit(); 
        AudioManager.instance.PlayDie();
        GameManager.instance.GameOver();
    }
}
