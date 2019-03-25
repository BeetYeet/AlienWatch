using UnityEngine;

public class BloodParent: MonoBehaviour
{
	void Awake()
	{
		EnemyBlood.bloodParent = transform;
	}
}
