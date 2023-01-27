using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] Damageable damageableScript;
	[SerializeField] GameObject canvas;
	[SerializeField] GameObject camera;
	[SerializeField] Image emptyHealthBar;
	[SerializeField] Image barHealth;
	[SerializeField] TextMeshProUGUI healthOutput;

	public int maxHealth;
	public int currentHealth;

	// Start is called before the first frame update
	void Awake()
    {
		maxHealth = damageableScript._healthConfigSO.MaxHealth;
		currentHealth = maxHealth;

		canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (canvas.activeInHierarchy)
		{
			currentHealth = damageableScript._currentHealth;

			float healthPercentage = (float)currentHealth / (float)maxHealth;
			barHealth.fillAmount = (healthPercentage);
			healthOutput.text = currentHealth + "/" + maxHealth;

			transform.LookAt(camera.transform);
		}

		if(camera == null)
		{
			camera = GameObject.FindGameObjectWithTag("MainCamera");
			//Debug.Log(camera.name);
		}

    }

	public void OnAttack()
	{
		//Debug.Log("isactive");
		canvas.SetActive(true);
	}

	public IEnumerator OnIdle()
	{
		yield return new WaitForSeconds(7);

		canvas.SetActive(false);
	}

	public void OnPlayerDead()
	{
		canvas.SetActive(false);
	}

}
