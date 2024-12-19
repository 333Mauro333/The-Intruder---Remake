using UnityEngine;


public class EnemyTurretSearch : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float speed;
    [SerializeField] float distanceToDetect;

    [Header("References")]
    [SerializeField] Transform target;
    [SerializeField] Transform[] rayOriginPoints;


    bool targetWasDetected;



    void Awake()
    {
        targetWasDetected = false;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
		targetWasDetected = SearchPlayer();

		if (targetWasDetected)
        {
            RotateToTarget();
		}
    }



    void RotateToTarget()
    {
		Vector3 direction = target.position - transform.position;
		Quaternion desiredRotation = Quaternion.LookRotation(direction);

		transform.rotation = Quaternion.RotateTowards(transform.rotation,
													  desiredRotation,
													  speed * Time.deltaTime);
	}
	bool SearchPlayer()
	{
		for (int i = 0; i < rayOriginPoints.Length; i++)
		{
			RaycastHit ray;
			Vector3 direction = target.position - rayOriginPoints[i].position;

			if (Physics.Raycast(rayOriginPoints[i].position, direction, out ray, distanceToDetect))
			{
				if (ray.collider.tag == "Player")
				{
					return true;
				}
			}
		}

		return false;
	}
}
