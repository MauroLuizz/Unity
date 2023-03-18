public class movimentacao : MonoBehaviour
{
    public Rigidbody2D rb;
    public int moveSpeed;
    public float jumpForce;
    private float direction;


    private Vector3 facingRight;
    private Vector3 facingLeft;

    public Animator animator; 

    public bool inGround;
    public Transform groundDetect;
    public LayerMask isGround;
    public int doubleJump;

    void Start()
    {
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    
    void Update()
    {   

        inGround = Physics2D.OverlapCircle(groundDetect.position, 0.2f, isGround);

        direction = Input.GetAxis("Horizontal");

        if(direction > 0)
        {
            //Olhando direita
            transform.localScale = facingRight;
        }
        else if (direction < 0)
        {
            transform.localScale = facingLeft;
        }

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if(Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if(Input.GetButtonDown("Jump") && inGround == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if(Input.GetButtonDown("Jump") && inGround == false && doubleJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            doubleJump--;
        }
        if(inGround == true)
        {
            doubleJump = 1;
        }
    }
}