using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float speed;

    CharacterController cc;


	void Awake()
	{
        cc = GetComponent<CharacterController>();
	}

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        cc.Move(transform.right * horizontalInput * speed * Time.deltaTime);
        cc.Move(transform.forward * verticalInput * speed * Time.deltaTime);
    }
}
