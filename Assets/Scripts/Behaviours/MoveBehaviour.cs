using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float jumpforce;
    private bool isGravityFlipped = false;
    private Animator animator;
    private bool isFacingRight = true;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void MoveCharacter(Vector2 direction)
    {
        //Se le añade un nuevo Vector para poder cambiar la 'x' y la 'y' de maneras diferentes para no cambiar las dos a la vez
        _rb.linearVelocity = new Vector2(direction.normalized.x * speed, _rb.linearVelocityY);
    }
    public void JumpCharacter()
    {
        if (IsOnFloor())
        {
            FlipGravity();
        }
        
    }
    public bool IsGrounded()
    {
        return IsOnFloor();
    }
    private bool IsOnFloor()
    {
        //Si la gravedad esta invertida, el 'Floor' esta arriba
        Vector2 direction = isGravityFlipped ? Vector2.up : Vector2.down;
        return Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Floor"));
    }
    public void FlipGravity()
    {
        //Invertir el signo de la gravedad del Rigidbody
        _rb.gravityScale *= -1;
        isGravityFlipped = !isGravityFlipped;

        //Girar visualmente el personaje
        Vector3 scale = transform.localScale;
        //Darle la vuelta verticalmente
        scale.y *= -1; 
        transform.localScale = scale;

        //Añadir un impulso en la direccion del nuevo "arriba"
        //Reseteamos velocidad para evitar errores
        _rb.linearVelocity = Vector2.zero; 
        _rb.AddForce(Vector2.up * jumpforce * Mathf.Sign(_rb.gravityScale), ForceMode2D.Impulse);
    }

    public void FlipSprite(float horizontalInput)
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
