using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPopup : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		DamagePopup _ = DamagePopup.Create(Vector3.zero, 400, Color.white);
		_.decrease = 2;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
