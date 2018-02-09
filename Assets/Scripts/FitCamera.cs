using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCamera : MonoBehaviour {

	public GameObject prefab;
	[Range(0,0.05f)]
	public float spacing ;
	public Vector2 padding;
	public Vector2Int gridSize;
	public Camera cam;

	GameObject [] elements;

	void Start () {
        elements = new GameObject[(int)gridSize.x];
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = Instantiate(prefab, transform);
        }
        PositionElements();
	}

	void Update () {
		PositionElements();
	}

	void PositionElements () {
		Sprite spr = prefab.GetComponent<SpriteRenderer>().sprite;
        float cameraWidth = cam.orthographicSize * cam.aspect * 2;
        float spriteWidth = spr.rect.width / spr.pixelsPerUnit;
		int numberOfSpaces = gridSize.x > 0 ? gridSize.x - 1 : 0;
        float spriteDesiredWidth = (cameraWidth - (padding.x * 2)) / gridSize.x - numberOfSpaces * spacing;

        float scaleFactor = spriteDesiredWidth / spriteWidth;

        for (int i = 0; i < gridSize.x; i++)
        {
            Vector3 position;  
			if (i ==0){
				position = cam.transform.position;
				position.x += -cameraWidth/2 + spriteDesiredWidth / 2 + padding.x + numberOfSpaces * spacing/gridSize.x;
				position.z = 0;
			}else {
				position = elements[i-1].transform.position;
				position.x += spacing*numberOfSpaces + spriteDesiredWidth;
			}
            elements[i].transform.position = position;
			elements[i].transform.localScale = prefab.transform.localScale * scaleFactor;
        }
	}
}
