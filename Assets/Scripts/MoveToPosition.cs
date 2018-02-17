using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPosition : MonoBehaviour {

	[SerializeField]float speed;
	Vector3 targetPosition;
	bool shouldMove =false;

	public void Move (Vector3 position) {
		shouldMove = true;
		targetPosition = position;
	}

	public void Stop () {
		shouldMove = false;
	}

	void Update( ) {
		if (!shouldMove)
			return;
		if (Vector3.Distance (transform.position,targetPosition) < .01f){
			transform.position = targetPosition;
			shouldMove = false;
		}else 
		{
			transform.position = Vector3.Lerp
				(transform.position,targetPosition,speed * Time.deltaTime);
		}
	}
}
