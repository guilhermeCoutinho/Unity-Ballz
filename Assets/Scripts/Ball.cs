using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	Vector3 direction;
	Rigidbody2D rb;
	public float force;
	ObjectPool pool;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		pool = GetComponentInParent<ObjectPool>();
	}

	public void Fire (Vector2 direction ){
		rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
	}
	
	public void Stop () {
		rb.velocity = Vector3.zero;
	}
}
