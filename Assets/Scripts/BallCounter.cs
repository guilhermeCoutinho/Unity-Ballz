using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCounter : MonoBehaviour {
	public Text text;
	
	int ballsInPlay;
	float startXPosition ;
	Vector3 endPosition ;
	Vector3 offset ;

	void OnEnable () {
		EventBinder.StartListening (
			EventBinder.ON_LAST_BALL_ARRIVED,onLastBallArrived);
	}

	void OnDisable () {
        EventBinder.StopListening(EventBinder.ON_LAST_BALL_ARRIVED, onLastBallArrived);
	}

	void Start () {
		offset = transform.position - BallHolder.Instance.GetPosition();
		startXPosition = BallHolder.Instance.GetPosition().x;
		onLastBallArrived ();
	}

    void onLastBallArrived()
    {
		if (BallHolder.Instance.GetPosition().x < startXPosition)
			endPosition = BallHolder.Instance.GetPosition () 
			+ new Vector3( -offset.x, offset.y,offset.z );
		else
            endPosition = BallHolder.Instance.GetPosition() + offset;
		WorldSpaceCanvas.Instance.DisplayText (
			"x" + BallHolder.Instance.getNumberOfBallRemaining().ToString(),
			endPosition,text);
    }

	int ballsRemaining ;
	int ballsArrived ;
	void Update() {
		ballsRemaining = BallHolder.Instance.getNumberOfBallRemaining();
		text.text = "x" + ballsRemaining;
		text.gameObject.SetActive(
				ballsRemaining != 0
		);
	}
}
