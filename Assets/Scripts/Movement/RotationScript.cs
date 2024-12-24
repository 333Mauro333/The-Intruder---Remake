using UnityEngine;


namespace TheIntruder_Remake
{
    public class RotationScript : MonoBehaviour
    {
        [SerializeField] Vector3 rotationSpeed;



        void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
