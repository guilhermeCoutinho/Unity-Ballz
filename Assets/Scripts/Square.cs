using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour {
	
	public float life = 5f;
	public float maxLife = 200;

	private SquareSpawner squareSpawner;

	CustomGradient _customGradient ;
	CustomGradient customGradient {
		get {
			if (_customGradient == null)
				_customGradient = GetComponentInParent<SquareSpawner>().colorGradient;
			return _customGradient;
		}
	}
	Text cachedText;
	RectTransform cachedTextRectTransform ;
	SpriteRenderer spriteRenderer;
    Vector3 previousPosition = Vector3.zero;
	MoveToPosition _moveTo;
	MoveToPosition MoveTo {
		get {
			if (_moveTo == null)
				_moveTo = GetComponent<MoveToPosition>();
			return _moveTo;
		}
	}
	 
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> () ;
	}

	void OnEnable () {
		cachedText = WorldSpaceCanvas.Instance.CreateText("" + life , transform.position);
		previousPosition = transform.position;
		cachedTextRectTransform = cachedText.GetComponent<RectTransform> ();
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        life--;
		if (life <= 0){
			if (cachedText != null)
				WorldSpaceCanvas.Instance.destroyText(cachedText);
			cachedText = null;
			MoveTo.Stop ();
            GetComponentInParent<SquareSpawner>().SquareDied(this);
		}
    }

	public void MoveToPosition (Vector3 targetPos) {
		MoveTo.Move (targetPos);
	}


	void Update () {
		WorldSpaceCanvas.Instance.UpdatePosition (cachedTextRectTransform
			,transform.position);
			previousPosition = transform.position;
		cachedText.text = life.ToString();
		spriteRenderer.color = customGradient.Evaluate(life/maxLife);
	}
}
