using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour {
    const float SQUARE_SIZE = 1f;
	const float WALL_WIDTH = 1F;

	public Vector2Int gridSize;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject topWall ;
	public GameObject bottomWall;

	void Start () {
		CreateGrid () ;
	}

	public void CreateGrid () {
		setupWall (new Vector2(WALL_WIDTH,gridSize.y),topWall);
        setupWall(new Vector2(WALL_WIDTH, gridSize.y), bottomWall);
        setupWall(new Vector2(WALL_WIDTH, gridSize.x), leftWall);
        setupWall(new Vector2(WALL_WIDTH, gridSize.x), rightWall);

        float totalWidth = gridSize.y * SQUARE_SIZE  +  WALL_WIDTH;
        float totalHeight =gridSize.x * SQUARE_SIZE  +  WALL_WIDTH;
		float offset = SQUARE_SIZE / 2f + WALL_WIDTH / 2f;
		
		topWall.transform.position = new Vector2 ( totalWidth/2f-WALL_WIDTH, offset);
		bottomWall.transform.position = new Vector2 (totalWidth/2f-WALL_WIDTH,offset - totalHeight);
		leftWall.transform.position = new Vector2 (-offset,-totalHeight/2f+WALL_WIDTH);
		rightWall.transform.position = new Vector2(-offset + totalWidth, -totalHeight / 2f+WALL_WIDTH);
	}

	void setupWall (Vector2 dims , GameObject wall ) {
		wall.GetComponent<SpriteRenderer>().size = new Vector2(dims.x, dims.y);
		wall.GetComponent<BoxCollider2D>().size = new Vector2(dims.x, dims.y);
	}

	void SpawnSquare (int i , int j) {
	}

	public void Spawn () {
		
	}

}
