using System.Collections.Generic;

using UnityEngine;


public class MeleeAttackingEnemyTrigger : MonoBehaviour
{
    [SerializeField] List<MeleeAttackingEnemyNavMesh> meleeAttackingEnemies;



	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerCollider"))
		{
			for (int i = 0; i < meleeAttackingEnemies.Count; i++)
			{
				meleeAttackingEnemies[i].SetState(MeleeAttackingEnemyStates.FollowingPlayer);
			}

			Destroy(gameObject);
		}
	}
}
