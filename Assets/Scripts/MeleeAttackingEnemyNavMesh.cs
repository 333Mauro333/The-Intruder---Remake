using UnityEngine;
using UnityEngine.AI;


public class MeleeAttackingEnemyNavMesh : MonoBehaviour
{
	[Header("References")]
    [SerializeField] Transform objective;

    NavMeshAgent nva;
	MeleeAttackingEnemyStates actualState;



	void Awake()
	{
		nva = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		objective = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
    {
		switch (actualState)
		{
			case MeleeAttackingEnemyStates.Idle:
				if (nva.destination != transform.position)
				{
					nva.destination = transform.position;
				}
				break;

			case MeleeAttackingEnemyStates.FollowingPlayer:
				nva.destination = objective.position;
				break;
		}
    }


	public void SetState(MeleeAttackingEnemyStates newState)
	{
		actualState = newState;
	}
}
