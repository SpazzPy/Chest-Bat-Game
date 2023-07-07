using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float FlightSpeed = 0.1f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public float gravityOnDeath = 10f;
    public TMP_Text deathText;

    private enum MovementState { idle }

    private float dirX;
    private float dirY;

    private bool death = false;

    public AudioSource deathSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        deathText.text = "";
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
        WalkingLogic();
        
    }

    private void WalkingLogic()
    {
        if (!death){
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");
            float xHorizontal = FlightSpeed;
            float yVertical = FlightSpeed;

            if (dirX != 0f) {
                
                if (dirX < 0f) {
                    xHorizontal *= -1;
                    sprite.flipX = true;
                } else {
                    sprite.flipX = false;
                }
                transform.position = transform.position + new Vector3(xHorizontal, 0f, 0f); 
            }

            if (dirY != 0f) {
                if (dirY < 0f) {
                    yVertical *= -1;
                }
                transform.position = transform.position + new Vector3(0f, yVertical, 0f); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!death){
            if (collision.gameObject.CompareTag("Traps"))
            {
                anim.SetTrigger("death");
                rb.gravityScale = gravityOnDeath;
                death = true;
                deathSound.Play();
                deathText.text = "Moriste !";
            }
        }
    }

    private IEnumerator RestartLevel () {
        if (death){
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(1);
        }
    }
        
}
