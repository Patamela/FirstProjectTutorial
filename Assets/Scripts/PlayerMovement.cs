using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private MeshRenderer playerRenderer;

    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;

    public Camera camera;
    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        // FixedUpdateNetwork is only executed on the StateAuthority

        if (_controller.isGrounded)
        {
            //Mantiene el jugador pegado al suelo, haciendo que cuando este subiendo rampas tengsa como referencia una misma alutra dependiendo de donde este
            _velocity = new Vector3(0, -1, 0);
        }

        //Vector3 move = new Vector3(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), 0, Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) * Runner.DeltaTime * PlayerSpeed;
        Quaternion cameraRotationY = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), 0, Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) * Runner.DeltaTime * PlayerSpeed;

        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }

        _controller.Move(move + _velocity * Runner.DeltaTime);
        
        gameObject.transform.forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z).normalized;
        /*if (move != Vector3.zero)
        {
           gameObject.transform.forward = move;
        }*/

        _jumpPressed = false;
    }

    /// <summary>
    /// Lo que hace es asociar la camara al jugador cuando ya has spawneado
    /// </summary>
    public override void Spawned()
    {
        if(HasStateAuthority == false) return;
        
        playerRenderer.material.color = new Color(0f,1f,0f,1f);
        camera = Camera.main;
        camera.GetComponent<FirstPersonCamera>().Target = transform;
    }
}