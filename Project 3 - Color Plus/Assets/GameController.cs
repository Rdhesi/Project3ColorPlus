using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	float turnLength = 2.0f;
	float timeToAct;
	public static CubeBehavior ClickedCube;
	public static bool isCubePlaced=false;
	public static bool isWin=false;
	// Use this for initialization
	void Start () {

		timeToAct = turnLength;
	}
	
	// Update is called once per frame
	void CheckKeyInput()
	{
		for (int i = 0; i < Field.gridHeight; i++) 
		{
			if (Input.GetKeyDown ((Field.gridHeight - i).ToString())) 
			{
				
				Field.inputRow = i;
			}
		}
	}
	public static void ProcessClickedCube(CubeBehavior clickedCube, int x, int y)
	{
		if (clickedCube.status == 1) {
			clickedCube.isActive = true;
			clickedCube.changeCubeScale (1.5f);

			if (ClickedCube != null) {
				ClickedCube.changeCubeScale (1.0f);
				ClickedCube.isActive = false;
			}
			ClickedCube = clickedCube;

		}
		else if (clickedCube.status == 1&&clickedCube.isActive==true) {
			clickedCube.isActive = false;
			clickedCube.changeCubeScale (1.0f);
		}
		else
			if (((ClickedCube.y==y&&(ClickedCube.x-1==x||ClickedCube.x+1==x))||(ClickedCube.x==x&&(ClickedCube.y-1==y||ClickedCube.y+1==y)))&&clickedCube.status==0)
		 {

				clickedCube.isActive = true;
				clickedCube.changeCubeScale (1.5f);
				clickedCube.status=1;
				clickedCube.GetComponent<Renderer> ().material.color=ClickedCube.GetComponent<Renderer> ().material.color;
				ClickedCube.GetComponent<Renderer> ().material.color=Color.white;
				ClickedCube.changeCubeScale (1.0f);
				ClickedCube.isActive = false;
				ClickedCube.status=0;
				ClickedCube = clickedCube;
		 }



	}

	void Update () {
		CheckKeyInput ();
		if (Field.inputRow != -1 && isCubePlaced==false) {
			Field.PlaceCube ();
			isCubePlaced = true;
		}
		if (Time.time > timeToAct&&Time.time<120.0f&&Field.gameOver==false) {
			Field.CubeGenerator ();

			if(Field.inputRow==-1){
				Field.PlaceRandomBlackCube ();
				Field.score--;
		}
			timeToAct += turnLength;
			Field.inputRow = -1;
			isCubePlaced = false;
			if (Time.time >= 120.0f) {
				Field.gameOver = true;
				isWin = true;
			}
		}
	
	}
}
