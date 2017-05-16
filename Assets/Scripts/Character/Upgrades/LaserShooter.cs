using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour {
	public LayerMask hitLayers;
	private LineRenderer lineRenderer;
	CharStatus charStatus;
	CharEnergy charEnergy;
	InputManager input;

	bool holdShoot, canShoot = true;
	public int damage = 2;

	void Start() {
		//Change player sprite and display tutorial
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		charStatus = transform.parent.GetComponent<CharStatus> ();
		charEnergy = GameObject.Find("Char").GetComponent<CharEnergy> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		if (!input.GetKey("shoot")) {
			holdShoot = false;
		}
		if (input.GetKey("shoot") && !holdShoot && canShoot && !charStatus.isSmall) {
			holdShoot = true;
			if (charEnergy.UseEnergy (2)) {
				canShoot = false;
				StartCoroutine (ActivateLaser ());
			}
		}
	}

	IEnumerator ActivateLaser() {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, Mathf.Infinity, hitLayers);
		if (hit.point != new Vector2(0, 0)) {
			lineRenderer.SetPosition (0, new Vector3 (transform.position.x + 0.5f, transform.position.y, -5));
			lineRenderer.SetPosition (1, new Vector3 (hit.point.x, hit.point.y, -5));
			lineRenderer.enabled = true;
			HitByLaser (hit);
			yield return new WaitForSeconds (0.1f);
			lineRenderer.enabled = false;
			yield return new WaitForSeconds (1f);
			canShoot = true;
		} else
			canShoot = true;
	}

	void HitByLaser(RaycastHit2D victim) {
		Debug.Log ("Player shot: " + victim.collider.gameObject.name + " with tag: " + victim.collider.gameObject.tag);

		//victim.transform returns parent transform, victim.collider returns the hit collider.
		switch(victim.collider.gameObject.tag) {

		//Add more cases as more types of enemies are added to the game
		case "SmallCritter":
			victim.transform.gameObject.GetComponent<SmallCritter> ().TakeDamage (damage);
			break;

		case "JumpingCritter":
			victim.transform.gameObject.GetComponent<JumpingCritter> ().TakeDamage (damage);
			break;

		case "HardEnemy":
			victim.transform.gameObject.GetComponent<HardCritter> ().Rush ();
			break;

		case "BigEyeGuy" :
			//Can't be hurt by laser, play relevant things
			break;

		case "CrawlerCritter":
			CrawlerCritter crawlercritter = victim.transform.gameObject.GetComponent<CrawlerCritter> ();
			if (crawlercritter.deShelled) {
				crawlercritter.TakeDamage (damage);
			} else {
				//Can't be hurt by laser, play relevant things
			}
			break;

		case "ShellMan":
			ShellMan shellMan = victim.transform.gameObject.GetComponent<ShellMan> ();
			if (shellMan.deShelled) {
				shellMan.TakeDamage (damage);
			} else {
				//Can't be hurt by laser, play relevant things
			}
			break;

		case "BirdBossWeakSpot":
			Debug.Log ("Hit Bird in the Mouth!");
			victim.transform.gameObject.GetComponent<BigBadBird> ().TakeDamage ();
			break;

		case "BigEyeGuyWeakSpot":
			Debug.Log ("Hit EyeGuy in the Eye!");
			victim.transform.gameObject.GetComponent<BigEyeGuy> ().TakeDamage (damage);
			break;

		case "FinalBossArmor":
			Debug.Log ("Hit the boss in the armor!");
			victim.transform.gameObject.GetComponent<Phase1> ().Stunned (7f);
			break;

		case "FinalBossArmor2":
			victim.transform.gameObject.GetComponent<Phase2> ().Stunned (7f);
			break;

		case "FinalBossWeakSpot":
			Debug.Log ("Hit the boss in the weak spot!");
			victim.transform.gameObject.GetComponent<Phase1> ().Fall ();
			break;

		case "FinalBossHead":
			Debug.Log ("Hit the boss in the head!");
			victim.transform.gameObject.GetComponent<Phase2> ().LaserShot ();
			break;

		case "FinalBossLastForm":
			Debug.Log ("Shot last boss form");
			//Check so that it goes after the correct child
			victim.transform.gameObject.transform.GetChild (0).GetComponent<BigEyeGuyLaser> ().Shoot ();
			break;
		}
	}

	//Probably wont use this.
/*	IEnumerator ShrinkLaser() {
		Vector3 target = transform.position;
		Invoke ("CanShoot", 1);
		yield return new WaitForSeconds (0.1f);
		if (transform.parent.gameObject.GetComponent<CharStatus> ().isMirrored) {
			while (lineRenderer.enabled && Mathf.Abs (target.x) > Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		else {
			while (lineRenderer.enabled && Mathf.Abs (target.x) < Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		lineRenderer.enabled = false;
	}*/
}