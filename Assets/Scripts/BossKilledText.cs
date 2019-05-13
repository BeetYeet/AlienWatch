using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilledText : MonoBehaviour
{
	public TMPro.TextMeshProUGUI text;
	public string VitalityBoss;
	public string StrengthBoss;
	public string SpeedBoss;

	public float waitToRem;
	public float time1;
	public float time2;
	public float time3;

	public static BossKilledText curr;

	void Start()
	{
		curr = this;
		text = GetComponent<TMPro.TextMeshProUGUI>();
		text.text = "";
	}

	// Update is called once per frame
	void Update()
	{
		if (BossDie2.redBossDead)
		{
			if (time1 != 0f)
				if (time1 > 0f)
				{
					time1 -= Time.deltaTime;
				}
				else
				{
					time1 = 0f;
					text.text = "";
				}
		}
		if (BossDie3.yellowBossDead)
		{
			if (time2 != 0f)
				if (time2 > 0f)
				{
					time2 -= Time.deltaTime;
				}
				else
				{
					time2 = 0f;
					text.text = "";
				}
		}
		if (BossDie.GreenBossDead)
		{
			if (time3 != 0f)
				if (time3 > 0f)
				{
					time3 -= Time.deltaTime;
				}
				else
				{
					time3 = 0f;
					text.text = "";
				}
		}
	}
}
