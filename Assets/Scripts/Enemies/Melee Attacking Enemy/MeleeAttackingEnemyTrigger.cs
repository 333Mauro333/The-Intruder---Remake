using System.Collections.Generic;

using UnityEngine;


namespace TheIntruder_Remake
{
	public class MeleeAttackingEnemyTrigger : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] List<MeleeAttackingEnemyNavMesh> meleeAttackingEnemies;



		void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("PlayerCollider"))
			{
				AlertTheEnemies();
				Autodestroy();
			}
		}



		void AlertTheEnemies()
		{
			for (int i = 0; i < meleeAttackingEnemies.Count; i++)
			{
				meleeAttackingEnemies[i].SetState(MeleeAttackingEnemyStates.FollowingPlayer);
			}
		}
		void Autodestroy()
		{
			Destroy(gameObject);
		}
	}
}
