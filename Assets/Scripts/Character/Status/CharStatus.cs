﻿using UnityEngine;
using System.Collections;

public class CharStatus : MonoBehaviour {
	public bool onGround, onLeftWall, onRightWall, onPlatform, isMirrored, isFloating, onSurface, invulnerable;
	public bool megaPunch, chargedMegaPunch, isSmall;
	public float velocityX, velocityY;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;

	void Update() {
		velocityX = GetComponent<Rigidbody2D> ().velocity.x;
		velocityY = GetComponent<Rigidbody2D>().velocity.y;
	}

	void FixedUpdate() {
		grounded = GetComponent<Rigidbody2D>().velocity.y < 1f ? Physics2D.OverlapBox (groundCheck.position, new Vector2 (0.6f, 0.1f), 0.2f, whatIsGround) : false;
	}

	/**
	 * Returns true if the player is in the air.
	 */
	public bool InAir() {
		return (!onGround && !onLeftWall && !onRightWall && !onPlatform && !onSurface);
	}

	public bool IsMegaPunching() {
		return (megaPunch || chargedMegaPunch);
	}

	public void NoLongerMegaPunching() {
		megaPunch = false;
		chargedMegaPunch = false;
	}

	public void Invulnerable(float time) {
		invulnerable = true;
		Invoke ("SetVulnerable", time);
	}

	void SetVulnerable() {
		invulnerable = false;
	}

	public bool Invulnerable() {
		return invulnerable;
	}

	/*public void changeSize() {
		isSmall = !isSmall;
	}
	*/
}
