using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float jumpforce;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void MoveCharacter(Vector2 direction)
    {
        //Se le añade un nuevo Vector para poder cambiar la 'x' y la 'y' de maneras diferentes para no cambiar las dos a la vez
        _rb.linearVelocity = new Vector2(direction.normalized.x * speed, _rb.linearVelocity.y);
    }
    public void JumpCharacter()
    {
        if (InFloor())
        {
            //Se multiplica el Vector para arriba por la jumpforce, la otra cosa es para que se impulse para arriba y despues baje
            //(para que no este todo el rato para arriba)
            _rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }
    }
    private bool InFloor()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Floor"));
    }
}
