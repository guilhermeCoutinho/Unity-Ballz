using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	int interaction = -1 ;
	Vector3 collisionPoint  ;
	Vector3 offset = Vector3.up * .2f;

	void OnTriggerEnter2D(Collider2D col) {
		BallHolder.Instance.ReturnBall (col.GetComponent<Ball> ());
    }
}
