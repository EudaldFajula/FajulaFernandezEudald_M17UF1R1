using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private MoveBehaviour _mb;
    private InputSystem_Actions inputActions;
    private Rigidbody2D _rb;
    private Vector2 moveInput;

    public void OnJump(InputAction.CallbackContext context)
    {
        _mb.JumpCharacter();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Para mover el personaje 
        _mb.MoveCharacter(moveInput);
    }
    private void Awake()
    {
        //Para crear los inputActions
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        //Crear la variable _mb con el script de MoveBehaviour
        _mb = GetComponent<MoveBehaviour>();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
