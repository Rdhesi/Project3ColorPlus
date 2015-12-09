using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Field : MonoBehaviour {
	public GameObject cubePrefab;
	public static int gridWidth = 8;
	public static int gridHeight = 5;
	public static GameObject[,] allCubes=new GameObject[gridWidth, gridHeight];
	public static int x, y, status;//staus is 0=white,2=color 3= black
	public static Color[] colors = {Color.yellow, Color.blue, Color.green, Color.red, Color.magenta};
	public static GameObject randomCube;
	public static Color currentColor;
	public static int inputRow=-1;
	public static int[] freeCubes=new int [gridWidth];
	public static int score = 0;
	public static GameObject[] cubesForScoring = new  GameObject[5];
	public static GameObject[] whiteCubes = new  GameObject[gridWidth*gridHeight];
	public static bool gameOver=false;
	public static int samePoints= 10;
	public static int differentPoints=5;
	public Text scoreUI;
	public Text timeLeft;


	// Use this for initialization
	void Start () {
		randomCube = (GameObject) Instantiate(cubePrefab, new Vector3(-7,2, 10),	Quaternion.identity);
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*2-7, y*2- 8, 10),	Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
				allCubes[x,y].GetComponent<CubeBehavior>().status = 0;
			}
		}
	}

	public static void PlaceCube()
	{
		int j = 0;

		for(int i=0; i<gridWidth;i++ )
		{
			if(allCubes[i,inputRow].GetComponent<CubeBehavior>().status==0)
			{
					freeCubes[j]=i;
					j++;
			}
		
		}
		if (j > 0) {
			int currentX = Random.Range (0, j);
			allCubes [freeCubes [currentX], inputRow].GetComponent<CubeBehavior> ().status = 1;
			allCubes [freeCubes [currentX], inputRow].GetComponent<Renderer> ().material.color = currentColor;
			inputRow = -1;	

		} else {
			gameOver = true;
		}
	}
	public static void CubeGenerator()
	{
		currentColor = colors [Random.Range (0, colors.Length)];
		randomCube.GetComponent<Renderer> ().material.color = currentColor;
		
	}
	public static void PlusFinder()//finds a potential plus for scoring
	{
		//this method will go through the grid and try to find a plus of cubes that are all colored then it'll put 
		//them in the array cubes for scoring so colors can be checked
		for(int x=1;x<gridWidth-1;x++)
		{
			for (int y = 1; y < gridHeight-1; y++)
			{
			if(allCubes[x,y].GetComponent<CubeBehavior>().status==1&&allCubes[x+1,y].GetComponent<CubeBehavior>().status==1&&allCubes[x-1,y].GetComponent<CubeBehavior>().status==1&&allCubes[x,y+1].GetComponent<CubeBehavior>().status==1&&allCubes[x,y-1].GetComponent<CubeBehavior>().status==1)
			{
				cubesForScoring[0]=allCubes[x,y];
				cubesForScoring[1]=allCubes[x+1,y];
				cubesForScoring[2]=allCubes[x-1,y];
				cubesForScoring[3]=allCubes[x,y+1];
				cubesForScoring[4]=allCubes[x,y-1];
				SingleColorScoreChecker();
				MultiColorScoreChecker();
				
			}

			}
		}
	}
	public static void SingleColorScoreChecker()//checks if the plus is all 1 color
	{
		// sets it to true by default
		bool isSingleColor = true;
		//loops through cubes for scoring and checks if the next cube in the list fails to match the previous
		//since they should all be the same
		for(int i=0;i<cubesForScoring.Length-1;i++)
		{
			if (cubesForScoring [i].GetComponent<Renderer> ().material.color != cubesForScoring [i+1].GetComponent<Renderer> ().material.color ) 
			{
				isSingleColor = false;
			}
		}
		if(isSingleColor==true)//sets cubes to black and changes state
		{
			for (int j = 0; j< cubesForScoring.Length ; j++) 
			{
				allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<Renderer> ().material.color = Color.black;
				allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<CubeBehavior>().status = 2;
			}
			ScoreUpdater (samePoints);
		}
		
	}
	public static void MultiColorScoreChecker()//checks if plus has one of each
	{
		
		
		{
			bool isMultiColor =false;
			bool isMagenta = false;
			bool isBlue = false;
			bool isYellow = false;
			bool isGreen = false;
			bool isRed = false;
			

				for(int i=0;i<cubesForScoring.Length;i++)
				{
					if (cubesForScoring[i].GetComponent<Renderer> ().material.color==Color.blue) 
					{
						isBlue=true;
					}
					else if (cubesForScoring[i].GetComponent<Renderer> ().material.color==Color.magenta)
					{
						isMagenta=true;
					}
					else if (cubesForScoring[i].GetComponent<Renderer> ().material.color==Color.yellow)
					{
						isYellow=true;
					}
					else if (cubesForScoring[i].GetComponent<Renderer> ().material.color==Color.green)
					{
						isGreen=true;
					}
					else if (cubesForScoring[i].GetComponent<Renderer> ().material.color==Color.red)
					{
						isRed=true;
					}
				}		
					if(isRed==true&&isMagenta==true&&isBlue==true&&isYellow==true&&isGreen==true)
					{
						isMultiColor=true;
					}
				
					
			if(isMultiColor==true)
			{
				for (int j = 0; j< cubesForScoring.Length ; j++) 
						{
							allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<Renderer> ().material.color = Color.black;
							allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<CubeBehavior>().status = 2;
					allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<CubeBehavior>().changeCubeScale (1.0f);
					allCubes [cubesForScoring [j].GetComponent<CubeBehavior>().x, cubesForScoring [j].GetComponent<CubeBehavior>().y].GetComponent<CubeBehavior>().isActive = false;
						}
				ScoreUpdater (differentPoints);
			}
		}
	}

	public static void ScoreUpdater(int newPoints)
	{
		score += newPoints;	
	}

	public static void PlaceRandomBlackCube()
	{
		int numCubes = 0;
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				if(allCubes[x,y].GetComponent<CubeBehavior>().status==0)
				{
					whiteCubes [numCubes] = allCubes [x, y];
					numCubes++;
				}
			}
		}
		if (numCubes != 0) {
			int chosenCube = Random.Range (0, numCubes);
			whiteCubes [chosenCube].GetComponent<Renderer> ().material.color = Color.black;
			whiteCubes [chosenCube].GetComponent<CubeBehavior> ().status = 2;
		} else {
			gameOver = true;
		
		}
	}

	// Update is called once per frame
	void Update () {
		PlusFinder ();
		if ((120.0f - Time.time)> 10.0f) {
			timeLeft.color = Color.green;
		} else {
			timeLeft.color= Color.red;
		}

		scoreUI.text = "Score: " + score;
		timeLeft.text = "Time Left :" + (120.0f - Time.time);
		//application.loadlevel(int or string)
		if(gameOver==true){
			Application.LoadLevel (1);
		}
	}
}
