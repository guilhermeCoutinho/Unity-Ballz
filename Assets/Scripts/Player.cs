using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

	public BallHolder ballHolder; 

	void Update () {
		if (ballHolder.getNumberOfBallsInPlay() == 0) {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 direction =
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                 - ballHolder.GetPosition();
                ballHolder.OnNewPlayerMove();
                BallHolder.Instance.FireBalls(direction);
            }
		}
		
		if (Input.GetMouseButtonDown(1)) {
			BallHolder.Instance.AddBall ();
		}
	}

	void OnEnable () {
        EventBinder.StartListening (EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
    }

    void OnDisable () {
        EventBinder.StopListening(EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
    }

    void OnLastBallArrived () {
        
    }
}
