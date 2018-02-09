using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceCanvas : Singleton<WorldSpaceCanvas> {
	public Camera cam;
	public ObjectPool textPool ;
	RectTransform CanvasRect;
	void Awake () {
		CanvasRect = GetComponent<RectTransform> ();
	}
	
	public Text CreateText (string s,Vector3 worldPosition) {
		GameObject GO = textPool.getObject();
		Text textComponnent = GO.GetComponent<Text>();
		DisplayText(s,worldPosition, textComponnent);
		return textComponnent;
	}

	public void DisplayText(string s, Vector3 worldPosition , Text text)
    {
        text.GetComponent<RectTransform>().anchoredPosition = getScreenPosition(worldPosition);
        text.text = s;
    }

    public Vector2 getScreenPosition(Vector3 worldPosition)
    {
        Vector2 ViewportPosition = cam.WorldToViewportPoint(worldPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
        return WorldObject_ScreenPosition;
    }

	public void destroyText (Text text) {
		textPool.returnObject (text.gameObject);
	}
}
