using UnityEngine;


public class PlayerShot : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] float cooldown;

	[Header("Own References")]
	[SerializeField] Transform bulletOriginPoint;
	[SerializeField] Transform cameraTransform;

	float counter;


	void Awake()
	{
		counter = 0.0f;
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (counter > 0.0f)
		{
			counter -= Time.deltaTime;
		}

		if (Input.GetButton("Shot") && counter <= 0.0f)
		{
			counter = cooldown;

			Shoot();
		}
	}


	void Shoot()
	{
		GameObject bullet = ObjectPooling.GetInstance().GetElement();

		bullet.transform.position = bulletOriginPoint.position;
		bullet.transform.rotation = bulletOriginPoint.rotation;

		bullet.SetActive(true);
	}

	RaycastHit GetImpactedElementOnFront()
	{
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)) // Realiza el raycast
		{
			Vector3 clickedPoint = hit.point; // Obtiene la posición 3D del impacto.
		}

		return hit;
	}
}
