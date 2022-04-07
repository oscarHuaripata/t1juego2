using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
     private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;

   // private Collision2D other;
    
    public float velocityX = 3;
    public float jumpForce = 30;
    
    private const int Run = 0;

    private const int Jump = 1;

    private const int Slide = 2;
    
    private bool estaSaltando = false;//aquiiiiiiii
    
    private const int LAYER_ENEMY = 10;

    private int contador = 0;

     bool active;
     
     private const string TAG_ENEMY = "enemigo";
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //other = GetComponent<Collision2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        sr.flipX = false;
        CambiarAnimacion(Run);
        
        if (Input.GetKeyUp(KeyCode.Space)  && !estaSaltando )
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            CambiarAnimacion(Jump);
            estaSaltando = true;
        }

        if (Input.GetKey(KeyCode.X))
        {
            CambiarAnimacion(Slide);
        }
       
        
        
    }

               
    
    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Piso")
            estaSaltando = false;
        
        
        if (other.gameObject.name == "Dino" && Input.GetKey(KeyCode.X ))
        {
            rb.velocity = new Vector2(velocityX + 1.5f , rb.velocity.y);
           Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "Dino")
        {
           // Application.Quit();
           active = !active;
           Time.timeScale = (active) ? 0 : 1f;
           // Debug.Log("Salio del juego");
        }

        
        if (other.gameObject.CompareTag(TAG_ENEMY))
        {
            contador++;
            Debug.Log("Contador esta en: " + contador);
        }

        else if (contador == 10)
        {
            active = !active;
            Time.timeScale = (active) ? 0 : 1f;
        }
        
        

    }

    
   
}
