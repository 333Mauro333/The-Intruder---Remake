using UnityEngine;


public class EnemyTurretSearch : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float speed;
    [SerializeField] float detectionRange;
	[SerializeField] float detectionAngle;

    [Header("References")]
    [SerializeField] Transform target;
    [SerializeField] Transform[] rayOriginPoints;
	[SerializeField] GameObject light;


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

			if (TargetIsInRangeDetection())
			{
				light.SetActive(true);
			}
			else
			{
				light.SetActive(false);
			}
		}
	}

	void OnDrawGizmos()
	{
		// Dibujar el rango máximo como un círculo.
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRange);

		// Dibujar las líneas del cono.
		Vector3 frente = transform.forward * detectionRange;

		// Calcular los extremos del cono.
		Quaternion rotacionIzquierda = Quaternion.Euler(0, -detectionAngle / 2, 0);
		Quaternion rotacionDerecha = Quaternion.Euler(0, detectionAngle / 2, 0);

		Vector3 limiteIzquierdo = rotacionIzquierda * frente;
		Vector3 limiteDerecho = rotacionDerecha * frente;

		// Dibujar las líneas del cono.
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + limiteIzquierdo);
		Gizmos.DrawLine(transform.position, transform.position + limiteDerecho);
	}



	bool SearchPlayer()
	{
		for (int i = 0; i < rayOriginPoints.Length; i++)
		{
			RaycastHit ray;
			Vector3 direction = target.position - rayOriginPoints[i].position;

			if (Physics.Raycast(rayOriginPoints[i].position, direction, out ray, detectionRange))
			{
				if (ray.collider.tag == "Player")
				{
					return true;
				}
			}
		}

		return false;
	}
	void RotateToTarget()
    {
		Vector3 direction = target.position - transform.position;
		Quaternion desiredRotation = Quaternion.LookRotation(direction);

		transform.rotation = Quaternion.RotateTowards(transform.rotation,
													  desiredRotation,
													  speed * Time.deltaTime);
	}

	bool TargetIsInRangeDetection()
	{
		Vector3 direction = target.position - transform.position;
		bool playerIsInRange = false;

		if (direction.magnitude <= detectionRange + 0.75f)
		{
			float angle = Vector3.Angle(transform.forward, direction);

			if (angle <= detectionAngle / 2.0f)
			{
				playerIsInRange = true;
			}
		}

		return playerIsInRange;
	}
}
