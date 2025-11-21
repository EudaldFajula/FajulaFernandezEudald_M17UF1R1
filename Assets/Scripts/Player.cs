using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private MoveBehaviour _mb;
    private InputSystem_Actions inputActions;
    private Rigidbody2D _rb;
    private Vector2 moveInput;
    private Animator animator;
    [SerializeField] private int lives = 3;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private TextMeshProUGUI livesText;
    public static event Action UsePauseMenu = delegate { };
    [SerializeField] public AudioSource audioJump;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _mb.IsGrounded())
        {
            _mb.JumpCharacter();
            audioJump.Play();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            TakeDamage(1);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Win"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    public void TakeDamage(int damage)
    {
        lives -= damage;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    private void Respawn()
    {
        if(_rb.gravityScale < 0)
        {
            _mb.FlipGravity();
        }
        transform.position = respawnPoint.position;
        _rb.linearVelocity = Vector2.zero;
        // Puedes a�adir una animaci�n o efecto de respawn si quieres
    }
    private void UpdateLivesUI()
    {
        livesText.text = lives.ToString();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();  
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Para mover el personaje 
        _mb.MoveCharacter(moveInput);
        _mb.FlipSprite(moveInput.x);
        animator.SetBool("isJumping", !_mb.IsGrounded());
        animator.SetFloat("xVelocity", Mathf.Abs(_rb.linearVelocity.x));
        animator.SetFloat("yVelocity", _rb.linearVelocity.y);
        
    }
    private void Awake()
    {
        //Para crear los inputActions
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        //Crear la variable _mb con el script de MoveBehaviour
        _mb = GetComponent<MoveBehaviour>();
    }
    private void ControlInputs(bool enable)
    {
        if (enable)
            inputActions.Disable();
        else inputActions.Enable();
    }
    private void OnEnable()
    {
        inputActions.Enable();
        PauseController.PausePlayer += ControlInputs;
    }
    private void OnDisable()
    {
        inputActions.Disable();
        PauseController.PausePlayer -= ControlInputs;
    }
    private void OnDestroy()
    {
        inputActions.Disable();
        PauseController.PausePlayer -= ControlInputs;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        UsePauseMenu.Invoke();
    }
}
