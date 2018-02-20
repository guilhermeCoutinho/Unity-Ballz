using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPreviewer : MonoBehaviour {	

	public LineRenderer line;
	public float scrollSpeed;
	public GameObject arrow;

	bool show ;
	Vector2 offset = Vector2.zero;
    Vector3[] points = new Vector3[2];

	void Update () {
		if (show && !line.gameObject.activeInHierarchy){
			toggle(true);
		}else if (!show && line.gameObject.activeInHierarchy){
			toggle(false);
		}

		if (show) {
			Vector2 direction = Player.Instance.GetMouseCorrectedPosition ()
			- BallHolder.Instance.GetPosition();
			Debug.Log (direction);
			direction *= 10;
			Debug.Log (direction);
			points[0] = BallHolder.Instance.GetPosition() +
				new Vector3 (direction.x,direction.y,BallHolder.Instance.GetPosition().z);
			points[1] = BallHolder.Instance.GetPosition();
			line.SetPositions (points);
			line.material.SetTextureOffset ("_MainTex",offset);
			offset.x += scrollSpeed * Time.deltaTime;
			arrow.transform.position = BallHolder.Instance.GetPosition ();
			arrow.transform.rotation =
				Quaternion.Euler (
			 	new Vector3 (0,0,90+Vector2.Angle(Vector2.left,points[1]-points[0]))
				 );
		}
	}

	public void Show () {
		offset = Vector2.zero;
		show = true;
	}

	public void Hide () {
		show = false;
	}

	void toggle (bool active) {
    	line.gameObject.SetActive( active );
        arrow.gameObject.SetActive ( active );
	}
}
