using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour {
	
	public float life = 5f;
	public float maxLife = 200;

	public CustomGradient customGradient;
	Text cachedText;
	SpriteRenderer spriteRenderer;
	 
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> () ;
	}

	void Start () {
		cachedText = WorldSpaceCanvas.Instance.CreateText("" + life , transform.position);
		updateLife ();
	}

	void updateLife () {
        WorldSpaceCanvas.Instance.DisplayText("" + life, transform.position, cachedText);
		spriteRenderer.color = customGradient.Evaluate (life/maxLife);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        life--;
		if (life <= 0){
			WorldSpaceCanvas.Instance.destroyText(cachedText);
			Destroy(gameObject);
		}
		else{
			updateLife () ;
		}
    }
}
