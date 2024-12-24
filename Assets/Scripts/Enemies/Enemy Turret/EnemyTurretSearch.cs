using UnityEngine;


namespace TheIntruder_Remake
{
	public class EnemyTurretSearch : MonoBehaviour
	{
		[Header("Values")]
		[SerializeField] float speed;
		[SerializeField] float detectionRange;
		[SerializeField] float detectionAngle;
		[SerializeField] float timeBetweenShoots;
		[SerializeField] Vector3 offset;

		[Header("Own References")]
		[SerializeField] GameObject model;
		[SerializeField] GameObject ownBase;
		[SerializeField] Transform[] rayOriginPoints;
		[SerializeField] Transform bulletOriginPoint;
		[SerializeField] ParticleSystem blueParticles;
		[SerializeField] ParticleSystem smokeParticles;
		[SerializeField] Material materialBurned;

		[Header("References")]
		[SerializeField] Transform target;

		Renderer renderer;
		bool isAlive;
		bool targetWasDetected;
		float counter;



		void Awake()
		{
			renderer = model.GetComponent<Renderer>();
			isAlive = true;
			targetWasDetected = false;
			counter = 0.0f;
		}

		void Start()
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		void Update()
		{
			targetWasDetected = SearchPlayer();

			if (isAlive && targetWasDetected)
			{
				RotateToTarget();

				if (TargetIsInRangeDetection())
				{
					if (counter <= 0.0f)
					{
						Shoot();
						ResetTimeBetweenShoots();
					}
					else
					{
						counter -= Time.deltaTime;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				Autodestroy();
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
			Vector3 direction = target.position - transform.position + offset;
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
		void Shoot()
		{
			GameObject shotBullet = ObjectPooling.GetInstance().GetElement();

			shotBullet.transform.position = bulletOriginPoint.position;
			shotBullet.transform.rotation = bulletOriginPoint.rotation;
			shotBullet.SetActive(true);
		}
		void ResetTimeBetweenShoots()
		{
			counter = timeBetweenShoots;
		}

		void Autodestroy()
		{
			isAlive = false;
			blueParticles.Stop();
			smokeParticles.Play();
			renderer.material = materialBurned;
		}
	}
}
