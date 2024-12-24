using UnityEngine;


public class Bullet : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    float counter;
    float actualSpeed;



	void Awake()
	{
        counter = lifeTime;
        actualSpeed = speed;
	}

	void Update()
    {
        transform.position += transform.up * actualSpeed * Time.deltaTime;

        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            ResetState();
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Wall") || other.CompareTag("Floor") || other.CompareTag("PlayerShip"))
        {
			actualSpeed = 0.0f;
		}
        else if (other.CompareTag("MeleeEnemyCollider"))
        {
            ResetState();
        }
	}



    void ResetState()
    {
        counter = lifeTime;
		actualSpeed = speed;

        gameObject.SetActive(false);
    }
}
