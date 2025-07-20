using UnityEngine;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] public int jumpCount;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float size;
    [SerializeField] private float speedMultiplier;
    [SerializeField] public  bool receiveInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform pos;
    [SerializeField] private float posSize; 
 

    [Header("Script reference")]
    private TileSpawnner tileSpawnner;
    private PlayerFollower follower;
    private InGameUI gameUI;
  

    [Header("Particle Effect")]
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private ParticleSystem dustTrail; 

    [Header("Audio")]
    [SerializeField] private AudioSource coinSound;
    [SerializeField] public AudioSource explosionSound;

    public float tempSpeed;
    [SerializeField] public GameObject explosionEffect;
    void Start()
    {

        receiveInput = true;
        tempSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        follower = FindAnyObjectByType<PlayerFollower>();
        gameUI = FindFirstObjectByType<InGameUI>();
        tileSpawnner = FindFirstObjectByType<TileSpawnner>();
      

    }

    void Update()
    {
   
        if (receiveInput)
        {
            jump();
          
        }
        movement();
        HandleDustTrail();
    }
    private void HandleDustTrail()
    {
        if (dustTrail == null) return;

        if (speed > 0f && receiveInput)
        {
            if (!dustTrail.isPlaying)
                dustTrail.Play();
        }
        else
        {
            if (dustTrail.isPlaying)
                dustTrail.Stop();
        }
    }
    private void jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount < 2)
        {
            JumpOnce();
        }
    }
    public void JumpOnce()
    {
        if (jumpCount == 0)
        {
           
            rb.linearVelocity += Vector2.up * jumpForce;       // Apply fixed jump
            jumpCount++;
        }
        else if (jumpCount == 1)
        {
            anim.SetTrigger("Jump");
            DoubleJump();
        }
    }

    public void DoubleJump()
    {
        if (jumpCount == 1)
        {
       
            rb.linearVelocity += Vector2.up * jumpForce;       // Apply fixed jump
            jumpCount++;
        }
    }


    private void movement()
    {
        if (gameUI.playerCollidedWithEnemy == true || receiveInput == false)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            anim.SetBool("RunShoot", false);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            speed = 0;
            return;
        }
        else
        {
            speed += speedMultiplier * Time.deltaTime;
            
                anim.SetBool("Run", true);
                anim.SetBool("Idle", false);
                anim.SetBool("RunShoot", false);
         
        }
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
       
    }
 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GroundParent"))
        {

            tileSpawnner.spawnTile();
        }
        if (collision.gameObject.CompareTag("StartRun"))
        {
            follower.followPlayer = true; 
        }
        if (collision.gameObject.CompareTag("coins"))
        {   GameObject effect = Instantiate(collectEffect.gameObject, collision.gameObject.transform.position, Quaternion.identity);
            effect.GetComponentInParent<ParticleSystem>().Play();
            Destroy(effect, 0.4f);
            CoinManager.Instance.increment(1);
            Destroy(collision.gameObject);
            if(Settings.Instance.isPlayingSound) coinSound.PlayOneShot(coinSound.clip);
           

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GroundParent"))
        {
            Destroy(collision.gameObject, 5f);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f) // Normal points up, so player is landing
            {
                jumpCount = 0;
                break;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject effect = Instantiate(explosionEffect, collision.gameObject.transform.position, Quaternion.identity);
            gameUI.playerCollidedWithEnemy = true;
            receiveInput = false;
            if (Settings.Instance.isPlayingSound) explosionSound.PlayOneShot(explosionSound.clip);
            Destroy(collision.gameObject);
            Destroy(effect, 0.4f);
            follower.TriggerCameraShake();
           

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            gameUI.playerCollidedWithEnemy = true;
            receiveInput = false;
            if (Settings.Instance.isPlayingSound) explosionSound.PlayOneShot(explosionSound.clip);
            follower.TriggerCameraShake();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position , posSize);
    }
}