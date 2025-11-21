using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    public Turret _spawner;
    void Start()
    {
        _rb.linearVelocity = new Vector2( -10, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
            //Destroy(gameObject);
            _spawner.BulletStack.Push(gameObject);
            gameObject.SetActive(false);
        }
    }
}
