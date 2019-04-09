using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
	TextMeshPro textMesh;


	public static DamagePopup Create(Vector3 Position, int damageAmount, Color color)
	{
		Transform damagePopupTransform = Instantiate(GameAssets.curr.pfDamagePopup, Position, Quaternion.identity);
		DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
		damagePopup.Setup(damageAmount, color);

		return damagePopup;
	}

	const float Disappear_max_timer = 0.4f;
	float disappearTimer;
	public float moveYSpeed = 2f;
	double textcolorAlpha;


	public float decrease = 7f;
	private Vector4 col;

	private void Awake()
	{
		textMesh = transform.GetComponent<TextMeshPro>();
		textcolorAlpha = textMesh.color.a;
	}

	public void Setup(int damageAmount, Color color)
	{
		textMesh.SetText(damageAmount.ToString());
		textMesh.color = color;
		disappearTimer = Disappear_max_timer;
	}

	private void Update()
	{
		disappearTimer -= Time.deltaTime;
		transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
		if (disappearTimer <= 0)
		{
			textcolorAlpha -= (double)decrease * (double)Time.deltaTime;
			textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, (float)textcolorAlpha);
			if (textcolorAlpha <= 0d)
				Destroy(gameObject);

			moveYSpeed *= .9f;
		}
	}
}
