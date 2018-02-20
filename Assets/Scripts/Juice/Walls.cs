using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour {

	Animator animator;
	void Awake () {
		animator = GetComponentInChildren<Animator> ();
	}

	void OnCollisionEnter2D () 
	{
		animator.SetTrigger ("pop");
	}
}
