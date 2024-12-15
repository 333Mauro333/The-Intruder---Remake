using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float normalSpeed;
    [Space(8)]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchHeight;
    [Space(8)]
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    [Header("References")]
    [SerializeField] Camera camera;

	float normalHeight;
	float fallVelocity;
    float currentSpeed;

	CharacterController cc;



	void Awake()
	{
		fallVelocity = 0.0f;
        currentSpeed = normalSpeed;

        cc = GetComponent<CharacterController>();

		normalHeight = cc.height;
	}

    void Update()
    {
        ApplyGravitySystem();

        ControlJumpInput();
		ControlCrouchInput();
		ControlMovementInput();
	}


    void ApplyGravitySystem()
    {
        // Limita la velocidad de caída cuando el jugador está sobre el piso.
		if (cc.isGrounded && fallVelocity < 0.0f)
		{
			fallVelocity = -1.0f;
		}

		// Aplica gravedad al jugador.
		fallVelocity += gravity * Time.deltaTime;
		cc.Move(transform.up * fallVelocity * Time.deltaTime);
	}

    void ControlJumpInput()
    {
		if (Input.GetButtonDown("Jump") && cc.isGrounded)
		{
			fallVelocity = Mathf.Sqrt(jumpForce * -1f * gravity);
		}
	}
    void ControlCrouchInput()
    {
		if (Input.GetButtonDown("Crouch"))
		{
			cc.height = crouchHeight;
			currentSpeed = crouchSpeed;
		}

		if (Input.GetButtonUp("Crouch"))
		{
			cc.height = normalHeight;
			currentSpeed = normalSpeed;
		}
	}
	void ControlMovementInput()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		cc.Move(transform.right * horizontalInput * currentSpeed * Time.deltaTime +
		transform.forward * verticalInput * currentSpeed * Time.deltaTime);
	}
}
