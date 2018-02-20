using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPosition : MonoBehaviour {

	[SerializeField]float speed;
	public bool randomizeSpeed;
	public bool randomizeDelay;
	public bool delayBasedOnYPosition;
	public float multiplicativeFactor;
	Vector3 targetPosition;
	bool shouldMove =false;
	float actualSpeed ;

	public void Move (Vector3 position) {
		actualSpeed = randomizeSpeed ? Random.Range (speed,3*speed) : speed;
		if (randomizeDelay)
			Invoke ("DelayedMove" , Random.Range(0,.2f));
		else if (delayBasedOnYPosition)
            Invoke("DelayedMove", Random.Range(0,.05f) + multiplicativeFactor * transform.position.y);
		else 
			shouldMove = true;
		targetPosition = position;
	}

	void DelayedMove () {
		shouldMove = true;
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
				(transform.position,targetPosition,actualSpeed * Time.deltaTime);
		}
	}
}
