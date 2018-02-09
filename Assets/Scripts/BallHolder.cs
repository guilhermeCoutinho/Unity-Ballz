using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : Singleton<BallHolder>  {

	public GameObject ballPrefab;
	[Range (0,1)]
	public float fireRate;
	List<Ball> ballList = new List<Ball> ();
	List<Ball> ballsInPlay = new List<Ball> () ;

	bool waitingForFirstBallToArrive = true;
	Vector3 waitingPosition ;

	void Start () {
		waitingPosition = transform.position;
	}
	
    public Vector3 GetPosition()
    {
        return waitingPosition;
    }

	public void OnNewPlayerMove () {
		waitingForFirstBallToArrive = true;
	}

	public void AddBall () {
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

	IEnumerator fireBallCoroutine (Vector2 direction) {
		ballsInPlay.Clear () ;
		for (int i = 0; i < ballList.Count; i++)
		{
			ballsInPlay.Add (ballList[i]);
			ballList[i].Fire (direction);
			yield return new WaitForSeconds(fireRate);
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
		}
		b.transform.position = waitingPosition;
	}
}
