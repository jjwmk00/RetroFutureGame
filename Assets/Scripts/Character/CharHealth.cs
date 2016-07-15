using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharHealth : MonoBehaviour {
	public int currentHealth, maxHealth;

	void Update() {
		if (currentHealth <= 0)
			Die ();
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
		SetHealthSlider ();
	}
		
	public void IncreaseCurrentHealth(int amount) {
		currentHealth += amount;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		SetHealthSlider ();
	}

	public void IncreaseMaxHealth() {
		maxHealth ++;
		currentHealth = maxHealth;
		SetHealthSlider ();
	}

	void SetHealthSlider() {
		GameObject.Find ("healthSlider").GetComponent<Slider>().value = (float) currentHealth / maxHealth;
		GameObject.Find ("healthSlider").GetComponent<RectTransform> ().sizeDelta = new Vector2 (8 + 32 *  (float) maxHealth, 32);
	}

	void Die() {
		Debug.Log ("YOU DIED\n");
	}
}