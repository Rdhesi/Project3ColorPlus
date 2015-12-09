using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public int x, y, status;
	public bool isActive=false;
	// Use this for initialization
	void Start () {
	
	}
	void OnMouseDown()
	{
		GameController.ProcessClickedCube (this,x,y);
	}
	public void changeCubeScale(float scale)
	{
		gameObject.transform.localScale = new Vector3 (scale, scale, scale);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
