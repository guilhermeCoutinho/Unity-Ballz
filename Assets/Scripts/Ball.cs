using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {
	Vector3 direction;
	Rigidbody2D rb;
	public float force;
	ObjectPool pool;
	MoveToPosition mover;

	void Awake () {
		mover = GetComponent<MoveToPosition>() ;
		rb = GetComponent<Rigidbody2D>();
		pool = GetComponentInParent<ObjectPool>();
	}

	public void Fire (Vector2 direction ){
		rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
	}
	
	public void Stop () {
		mover.Stop ();
		rb.velocity = Vector3.zero;
	}

	public void Stop (Vector3 position) {
		Stop ();
		transform.position = position;
	}

	public void MoveTo (Vector3 moveTo) {
		mover.Move (moveTo);
	}
}
