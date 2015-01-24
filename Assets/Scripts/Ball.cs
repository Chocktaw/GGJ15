﻿using UnityEngine;
using System.Collections;

/**
 * The ball may or may not be attached to a player at any given time. 
 * If it is attached, we want the ball to spin to the position that the player is heading in.
 */
public class Ball : MonoBehaviour 
{
	public float Cooldown = 1.0f;
	private float cooldownTimer = 0.0f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (this.transform.parent != null)
		{
			// get it down to a smaller number
			Vector3 parentVelocity = this.transform.parent.rigidbody2D.velocity.normalized;
			// we don't want the ball to return to the centre when the player is not moving
			if (parentVelocity.magnitude != 0)
				transform.localPosition = new Vector3(parentVelocity.x/4, parentVelocity.y/4, 0);
		}
		cooldownTimer += Time.deltaTime;
	}

	public void Steal(Transform newParent)
	{
		// start the cooldown timer and change who the parent is, also reset the localposition
		this.transform.parent = newParent;
		this.transform.localPosition = new Vector3(0, 0.25f, 0);
		this.cooldownTimer = 0;
		this.rigidbody2D.velocity = Vector2.zero;
	}

	public bool IsOffCooldown()
	{
		return cooldownTimer > Cooldown;
	}
}