using UnityEngine;


public class EnemyTurretSearch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform target;



    void Update()
    {
        transform.LookAt(target);
    }
}
