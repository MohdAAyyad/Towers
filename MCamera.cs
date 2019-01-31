using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCamera : MonoBehaviour {

	protected GameObject C_FindPlayer;


	// Use this for initialization
	void Start () {

		Cursor.visible = false;

		if(C_FindPlayer=GameObject.Find("Player"))
		{
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {


		if (C_FindPlayer )
		{
			transform.position = new Vector3(C_FindPlayer.transform.position.x, C_FindPlayer.transform.position.y, -10.0f);			

		}

	}

	public void C_ZoomOut()
	{
		while(Camera.main.orthographicSize<10.0f)
		{
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 100, 0.1f);
		}
	}

	public void C_ZoomIn()
	{
		while (Camera.main.orthographicSize > 5.0f)
		{
			Camera.main.orthographicSize -= 0.001f;
		}
	}
}
