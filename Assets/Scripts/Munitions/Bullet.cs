using UnityEngine;


public class Bullet : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    float counter;
    float actualSpeed;

    BoxCollider bc;



	void Awake()
	{
        counter = lifeTime;
        actualSpeed = speed;

        bc = GetComponent<BoxCollider>();
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

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
        {
            actualSpeed = 0.0f;
            bc.isTrigger = true;
        }
        else if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            ResetState();
        }
	}



    void ResetState()
    {
        counter = lifeTime;
		actualSpeed = speed;
		bc.isTrigger = false;

        gameObject.SetActive(false);
    }
}
