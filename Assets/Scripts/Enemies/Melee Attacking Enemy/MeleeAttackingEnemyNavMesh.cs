using UnityEngine;
using UnityEngine.AI;


namespace TheIntruder_Remake
{
	public class MeleeAttackingEnemyNavMesh : MonoBehaviour
	{
		[Header("Values")]
		[SerializeField] float minDistanceToAttack;
		[SerializeField] float speed;

		[Header("References")]
		[SerializeField] GameObject hitCollider1;
		[SerializeField] GameObject hitCollider2;
		[SerializeField] Transform target;
		[SerializeField] GameObject objetoParaCrear;

		BoxCollider bc;
		NavMeshAgent nva;
		MeleeAttackingEnemyStates actualState;

		Animator a;
		bool hitted;
		bool attacking;



		void Awake()
		{
			bc = GetComponent<BoxCollider>();
			nva = GetComponent<NavMeshAgent>();
			actualState = MeleeAttackingEnemyStates.Idle;

			a = GetComponent<Animator>();
			hitted = false;
			attacking = false;
		}

		void Start()
		{
			FindTarget();
		}

		void Update()
		{
			UpdateEnemyBehavior();

			if (Input.GetKeyDown(KeyCode.Z))
			{
				SetState(MeleeAttackingEnemyStates.Death);
			}
		}



		public void SetState(MeleeAttackingEnemyStates newState)
		{
			actualState = newState;

			if (actualState == MeleeAttackingEnemyStates.FollowingPlayer)
			{
				a.SetBool("PlayerWasDetected", true);
			}
			else if (actualState == MeleeAttackingEnemyStates.Idle)
			{
				a.SetBool("PlayerWasDetected", false);
			}
			else if (actualState == MeleeAttackingEnemyStates.Death)
			{
				nva.destination = transform.position;
				a.SetBool("PlayerWasDetected", false);
				a.SetBool("IsDead", true);

				DestroyComponents();
			}
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
					nva.destination = transform.position;
					break;

				case MeleeAttackingEnemyStates.FollowingPlayer:

					AnimatorStateInfo stateInfo = a.GetCurrentAnimatorStateInfo(0);


					if (stateInfo.IsName("Running"))
					{
						nva.speed = speed;
						nva.destination = target.position;

						if (Vector3.Distance(transform.position, target.position) < minDistanceToAttack)
						{
							System.Random generator = new System.Random();

							hitted = false;

							if (!attacking)
							{
								attacking = true;

								if (generator.Next(0, 2) == 0)
								{
									a.SetBool("Attack1", true);
								}
								else
								{
									a.SetBool("Attack2", true);
								}
							}
						}
					}
					else if (stateInfo.IsName("Attacking 1"))
					{
						float actualAnimationTime = stateInfo.normalizedTime;

						nva.speed = 0.0f;

						if (!hitted && actualAnimationTime >= 0.25f)
						{
							hitCollider1.SetActive(true);

							BoxCollider hitBoxCollider = hitCollider1.GetComponent<BoxCollider>();
							Collider[] collidersIntoHitCollider = Physics.OverlapBox(hitBoxCollider.bounds.center, hitBoxCollider.bounds.extents);

							foreach (Collider collider in collidersIntoHitCollider)
							{
								if (collider.CompareTag("PlayerCollider"))
								{
									Instantiate(objetoParaCrear);
									hitted = true;
									hitCollider1.SetActive(false);
								}
							}
						}

						if (actualAnimationTime >= 0.4f)
						{
							hitCollider1.SetActive(false);
						}

						if (actualAnimationTime >= 0.9f)
						{
							Vector3 direction = target.position - transform.position;
							Quaternion rotation;

							direction.y = 0;
							rotation = Quaternion.LookRotation(direction);

							transform.rotation = rotation;

							attacking = false;
							a.SetBool("Attack1", false);
						}
					}
					else if (stateInfo.IsName("Attacking 2"))
					{
						float actualAnimationTime = stateInfo.normalizedTime;

						nva.speed = 0.0f;

						if (!hitted && actualAnimationTime >= 0.3f)
						{
							hitCollider2.SetActive(true);

							BoxCollider hitBoxCollider = hitCollider1.GetComponent<BoxCollider>();
							Collider[] collidersIntoHitCollider = Physics.OverlapBox(hitBoxCollider.bounds.center, hitBoxCollider.bounds.extents);

							foreach (Collider collider in collidersIntoHitCollider)
							{
								if (collider.CompareTag("PlayerCollider"))
								{
									Instantiate(objetoParaCrear);
									hitted = true;
									hitCollider2.SetActive(false);
								}
							}
						}

						if (actualAnimationTime >= 0.5f)
						{
							hitCollider2.SetActive(false);
						}

						if (actualAnimationTime >= 0.9f)
						{
							Vector3 direction = target.position - transform.position;
							Quaternion rotation;

							direction.y = 0;
							rotation = Quaternion.LookRotation(direction);

							transform.rotation = rotation;

							attacking = false;
							a.SetBool("Attack2", false);
						}
					}

					break;
			}
		}
		void DestroyComponents()
		{
			Destroy(nva);
			Destroy(bc);
			Destroy(hitCollider1);
			Destroy(hitCollider2);
		}
	}
}
