using UnityEngine;
using UnityEngine.AI;


namespace TheIntruder_Remake
{
	public class MeleeAttackingEnemyNavMesh : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] Transform target;

		NavMeshAgent nva;
		MeleeAttackingEnemyStates actualState;



		void Awake()
		{
			nva = GetComponent<NavMeshAgent>();
		}

		void Start()
		{
			FindTarget();
		}

		void Update()
		{
			UpdateEnemyBehavior();
		}



		public void SetState(MeleeAttackingEnemyStates newState)
		{
			actualState = newState;
		}

		void FindTarget()
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}
		void UpdateEnemyBehavior()
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
					nva.destination = target.position;
					break;
			}
		}
	}
}
