using UnityEngine;
using System.Collections;

public class BirdBossWingAttack : MonoBehaviour {

	void Start() {
		//Here start the wing-slapping animation
		//Also play relevant soundFX
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!col.collider.isTrigger && col.gameObject.tag.Equals("char")) {
			col.gameObject.GetComponent<CharHealth> ().TakeDamage (2);
			col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, 3f);
			//Play fitting soundFX
		}
	}
}