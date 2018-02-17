using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : Singleton<BallHolder>  {

	public GameObject ballPrefab;
	[Range (0,1)]
	public float fireRate;
	List<Ball> ballList = new List<Ball> ();
	List<Ball> ballsInPlay = new List<Ball> () ;

	bool waitingForFirstBallToArrive = false;
	bool waitingForLastBallToArrive = false;
	int ballsRemaining ;
	Vector3 waitingPosition ;

	void Awake () {
		waitingPosition = transform.position;
	}
	
    public Vector3 GetPosition()
    {
        return waitingPosition;
    }

	public void OnNewPlayerMove () {
		waitingForFirstBallToArrive = true;
		waitingForLastBallToArrive = true;
	}

	public void AddBall () {
		ballsRemaining ++ ;
		GameObject GO = Instantiate 
			( ballPrefab,waitingPosition,Quaternion.identity,transform);
		ballList.Add (GO.GetComponent<Ball> () );
	}

	public void FireBalls (Vector2 direction )  {
		StartCoroutine ( fireBallCoroutine (direction) );
	}

	public int getNumberOfBallsInPlay () {
		return ballsInPlay.Count;
	}

	public int getNumberOfBallRemaining () {
		return ballsRemaining;
	}

	IEnumerator fireBallCoroutine (Vector2 direction) {
		ballsInPlay.Clear () ;
		Vector3 originalFiringPosition = waitingPosition;
		ballsRemaining = ballList.Count;
		for (int i = 0; i < ballList.Count; i++ )
		{
			ballsInPlay.Add (ballList[i]);
			ballList[i].Stop (originalFiringPosition);
			ballList[i].Fire (direction);
			yield return new WaitForSeconds(fireRate);
			ballsRemaining --;
		}
	}

	public void ReturnBall (Ball b) {
		b.Stop ();
		ballsInPlay.Remove (b);
        if (waitingForFirstBallToArrive){
			waitingForFirstBallToArrive = false;
            float xPosition = b.transform.position.x;
			waitingPosition = 
				new Vector3 (xPosition,transform.position.y,transform.position.z);
            EventBinder.TriggerEvent(EventBinder.ON_FIRST_BALL_ARRIVED);
		}
		if (waitingForLastBallToArrive && getNumberOfBallsInPlay() == 0){
			waitingForLastBallToArrive = false;
            ballsRemaining = ballList.Count;
			EventBinder.TriggerEvent(EventBinder.ON_LAST_BALL_ARRIVED);
		}
		b.transform.position = new Vector3 (b.transform.position.x,
			waitingPosition.y,b.transform.position.z);
		b.MoveTo ( waitingPosition );
	}
}
