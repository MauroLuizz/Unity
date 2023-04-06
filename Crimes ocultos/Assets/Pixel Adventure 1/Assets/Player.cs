using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); //rigidboy vai receber o componente q estiver anexado com esse script
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame 
    // Esses componetes em Update serao checados todo momento
    void Update()
    {
        Move();
        Jump();

    }

    void Move() //Movimentacao do personagem
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        
        if(Input.GetAxis("Horizontal") > 0f)
        {
             anim.SetBool("walk", true); //animaçao indo para direita entao true
             transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)//animaçao indo para esquerda entao true
        {
             anim.SetBool("walk", true);
             transform.eulerAngles = new Vector3(0f,180f,0f);//faz a animação do personagem rotacionar para a esquerda
        }
        
        if(Input.GetAxis("Horizontal") == 0f) //animaçao esta parada entao walk é false
        {
             anim.SetBool("walk", false);
        }
       
    }

    void Jump() //script para pular
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!isJumping)// a exclamação inverte para false a condiçao se for um valor boleano
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);//força do pulo   
                doubleJump = true;// se o double jump for verdadeiro eu pulo novamente
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce ), ForceMode2D.Impulse);//força do pulo
                    doubleJump = false;//se o double jump for falso eu nao pulo novamente
                }   
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)//esse metodo serve para detectar quando o personagem tocar em alguma coisa
    {
        if(collision.gameObject.layer == 9)//o numero 9 corresponde a layer 9 do (Blocos)
        {
            isJumping = false; //quando meu personagem estiver tocando o chao ele ta pulando? nao = entao isJumping é falso
            anim.SetBool("jump", false); //essa linha checa se o personagem toca o chao para executar a animação 
        }
    }

    void OnCollisionExit2D(Collision2D collision)//esse metodo serve para detectar quando o personagem para de tocar em alguma coisa
    {
         if(collision.gameObject.layer == 9)//o numero 9 corresponde a layer 9 do (Blocos)
        {
            isJumping = true; //quando meu personagem estiver tocando o chao ele ta pulando? sim = entao isJumping é verdadeiro
        }
    }
}
