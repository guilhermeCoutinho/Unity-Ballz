using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour {
	const int columns = 7;
	public CustomGradient colorGradient;
	List<Square> squareList  = new List<Square>();
	ObjectPool squarePool ;

	void Awake () {
		squarePool = GetComponent<ObjectPool> ();
	}

	public void SquareDied (Square sq) {
		squareList.Remove(sq);
		squarePool.returnObject (sq.gameObject);
	}

	Square SpawnSquare (int i , int j,int life) {
        Square sq = squarePool.getObject().GetComponent<Square>();
		if (sq.life > 0)
			Debug.Log ("WTF " + sq.name);

        sq.transform.position = new Vector3 (i,j,0);
        squareList.Add(sq);
		sq.life = life;
		return sq;
	}

	public void Spawn (int playerMovesSoFar) {
		int [] spawnPositions = new int [columns];
		int differentColumns = 0;
		
		for (int i = 0; i < columns; i++)
            spawnPositions[i] = 0;
		int log = 
		Mathf.FloorToInt(
			Mathf.Log (playerMovesSoFar,2) 
			);

		int value = Mathf.Max (log,1);

		for (int i = 0;i<value;i++) {
			int randomX = Random.Range(0,columns);
			if (spawnPositions[randomX] == 0)
				differentColumns ++;
			if ( differentColumns > value ||differentColumns>6)
				continue;
			spawnPositions[randomX] += playerMovesSoFar;
		}
		for (int i = 0; i < columns; i++)
        {
			if (spawnPositions[i] != 0) {
				SpawnSquare (i,0,spawnPositions[i]);
			}
        }
	}

	void OnLastBallArrived () {
		Spawn(Player.Instance.numberOfPlayerMoves);
		moveSquaresToFinalPosition () ;
	}

	void moveSquaresToFinalPosition () {
		foreach (Square sq in squareList)
        {
            if (sq.gameObject.activeInHierarchy)
                sq.MoveToPosition(sq.transform.position + Vector3.down);
        }
	}

	void OnGameStarted () {
		OnLastBallArrived ();
	}

    void OnEnable()
    {
        EventBinder.StartListening(EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
		EventBinder.StartListening(EventBinder.ON_GAME_STARTED,
		OnGameStarted);
	}

    void OnDisable()
    {
        EventBinder.StopListening(EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
        EventBinder.StopListening(EventBinder.ON_GAME_STARTED,
        OnGameStarted);
    }

}
