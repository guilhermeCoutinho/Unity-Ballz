using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {
    [Range(0,1)]
    public float minimumYDistance = 0.1f;
    public TrajectoryPreviewer trajectoryPreviewer;
	public BallHolder ballHolder;
    public int numberOfPlayerMoves = 1;
    public Animator ballupText;
    Vector3 mouseCorrectedPosition ;

    public void StartGame () {
        Game.gameState = Game.State.RUNNING;
        BallHolder.Instance.AddBall ();
        EventBinder.TriggerEvent (EventBinder.ON_GAME_STARTED);
        trajectoryPreviewer.Show () ;
    }

    public Vector3 GetMouseCorrectedPosition () {
        return mouseCorrectedPosition;
    }

    void calculateCorrectedMousePosition () {
        mouseCorrectedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouseCorrectedPosition.y - ballHolder.GetPosition().y < minimumYDistance)
        {
            mouseCorrectedPosition.y = ballHolder.GetPosition().y + minimumYDistance;
        }
    }

	void Update () {

        if ( Game.gameState == Game.State.STOPPED) {
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame ();
        }
        if (Game.gameState == Game.State.RUNNING){
            gameRunning ();
        }
	}

    void gameRunning () {
        calculateCorrectedMousePosition();
        if (ballHolder.getNumberOfBallsInPlay() == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                trajectoryPreviewer.Hide();
                Vector2 direction =
                mouseCorrectedPosition
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
        trajectoryPreviewer.Show ();
        BallHolder.Instance.AddBall ();
    }
}
