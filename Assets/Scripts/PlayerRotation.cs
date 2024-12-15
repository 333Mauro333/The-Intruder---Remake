using UnityEngine;


public class PlayerRotation : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float sensitivity;

    [Header("References")]
    [SerializeField] Camera camera;



    void Update()
    {
        float mouseXAxis = Input.GetAxis("Mouse X");
		float mouseYAxis = Input.GetAxis("Mouse Y");

        if (mouseXAxis != 0.0f)
        {
            transform.Rotate(0.0f, mouseXAxis * sensitivity, 0.0f);
        }

        if (mouseYAxis != 0.0f)
        {
            Vector3 rotation = camera.transform.localEulerAngles;

            rotation.x = (rotation.x - mouseYAxis * sensitivity + 360) % 360;

            if (rotation.x > 80.0f && rotation.x < 180.0f)
            {
                rotation.x = 80.0f;
            }
            else if (rotation.x < 280.0f && rotation.x > 180.0f)
            {
                rotation.x = 280.0f;
            }

            camera.transform.localEulerAngles = rotation;
        }
	}
}
