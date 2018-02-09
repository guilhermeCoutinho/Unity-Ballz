using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour {
	const int columns = 7;
	List<Square> squareList  = new List<Square>();
	ObjectPool squarePool ;

	void Awake () {
		squarePool = GetComponent<ObjectPool> ();
	}

	Square SpawnSquare (int i , int j,int life) {
        Square sq = squarePool.getObject().GetComponent<Square>();
        sq.transform.position = new Vector3 (i,j,0);
        squareList.Add(sq);
		sq.life = life;
		return sq;
	}

	public void Spawn (int playerMovesSoFar) {
		int [] spawnPositions = new int [columns];
		for (int i = 0; i < columns; i++)
            spawnPositions[i] = 0;
		int sqrt = Mathf.FloorToInt(Mathf.Sqrt(playerMovesSoFar));
		for (int i = 0;i<playerMovesSoFar;i++) {
			int randomX = Random.Range(0,columns);
			spawnPositions[randomX] += sqrt;
		}
		for (int i = 0; i < columns; i++)
        {
			if (spawnPositions[i] != 0) {
				SpawnSquare (i,0,spawnPositions[i]);
			}
        }
	}

	int moves = 0;
	void OnLastBallArrived () {
		foreach (Square sq in squareList)
		{
			if (sq.gameObject.activeInHierarchy)
				sq.MoveToPosition (sq.transform.position + Vector3.down);
		}
		Spawn (moves++);
	}

    void OnEnable()
    {
        EventBinder.StartListening(EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
    }

    void OnDisable()
    {
        EventBinder.StopListening(EventBinder.ON_LAST_BALL_ARRIVED
        , OnLastBallArrived);
    }

}
