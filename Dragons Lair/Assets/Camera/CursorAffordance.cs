using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

[SerializeField] private Texture2D walkCursor = null;
[SerializeField] private Texture2D FightCursor = null;
[SerializeField] private Texture2D invalidCursor = null;

[SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

 private CameraRaycaster Click;
	
	// Use this for initialization
	void Start () {
		Click = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//if(Input.GetMouseButtonDown(0)){
			//print(Click.layerHit.ToString());
			switch (Click.layerHit){

			case Layer.Walkable:
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy:
				Cursor.SetCursor(FightCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.RayEndStop:
				Cursor.SetCursor(invalidCursor,cursorHotspot,CursorMode.Auto);
				break;
			default :
				Debug.Log("Don't know what cursor to show");
				return;
		}
	}
}
