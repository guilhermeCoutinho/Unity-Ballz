using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

	public BallHolder ballHolder;
    public int numberOfPlayerMoves = 1;
    public Animator ballupText;

    public void StartGame () {
            BallHolder.Instance.AddBall ();
            EventBinder.TriggerEvent (EventBinder.ON_GAME_STARTED);
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartGame ();
        }
		if (ballHolder.getNumberOfBallsInPlay() == 0) {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 direction =
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                 - ballHolder.GetPosition();
                ballHolder.OnNewPlayerMove();
                numberOfPlayerMoves++;
                BallHolder.Instance.FireBalls(direction);
            }
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
        ballupText.SetTrigger("ball_up");
        BallHolder.Instance.AddBall ();
    }
}
