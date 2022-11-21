using UnityEngine;
using System.Collections;

// this Demo is the main process class for the WyrmTale Jigsaw Puzzle Pack Demo
// this script was added to the Demo scene main Camera
public class Demo : MonoBehaviour {
	
	// we will select all images and game objects that have to be accessed on the linked script
	// so we dant have to do lookups or put stuff in Resources
    public Texture guiTitle = null;
    public Texture guiMenu= null;
    public Texture guiMenuPuzzle = null;
    public Texture guiMenuPieces = null;
    public Texture guiMenuRestart = null;
    public Texture[] imagePuzzle;
    public GameObject puzzle;

    DemoJigsawPuzzle jigsawPuzzle = null;
    int puzzleImageSelected = 1;
    int sizeMode = 1;

	// Use this for initialization
	void Start () {
        if (puzzle != null)
        {
			// puzzle was set so get linked DemoJigsawPuzzle class
            jigsawPuzzle = puzzle.GetComponent("DemoJigsawPuzzle") as DemoJigsawPuzzle;
			if (jigsawPuzzle==null)
			     jigsawPuzzle = puzzle.GetComponent("DemoJigsawPuzzle_mac_ios") as DemoJigsawPuzzle_mac_ios;
			if (jigsawPuzzle==null)
			     jigsawPuzzle = puzzle.GetComponent("JSDemoJigsawPuzzle") as DemoJigsawPuzzle;
            if (jigsawPuzzle!=null) 
				// if we have a jigsawPuzzle set size related to puzzleImage (1-3) and sizeMode (1-6)
				SetSize();
        }        
	}
	
    // translate Input mouseposition to GUI coordinates using camera viewport
    private Vector2 GuiMousePosition()
    {
        Vector2 mp = Input.mousePosition;
        Vector3 vp = Camera.main.ScreenToViewportPoint(new Vector3(mp.x, mp.y, 0));
        mp = new Vector2(vp.x * Camera.main.pixelWidth, (1 - vp.y) * Camera.main.pixelHeight);
        return mp;
    }
	
	// menu 'puzzle' is clicked so go to next puzzle
    public void Puzzle()
    {		
        puzzle.transform.localScale = new Vector3(6,6, puzzle.transform.localScale.z);
        jigsawPuzzle.image[puzzleImageSelected] = imagePuzzle[puzzleImageSelected];
        SetSize();
    }
	
	// set current puzzle size
    void SetSize()
    {
        if (GameManager.instance.levelPuzzle < 4){
			jigsawPuzzle.size = new Vector2(2,2);
            puzzle.transform.localScale = new Vector3(6,6, puzzle.transform.localScale.z);
		}
		else if (GameManager.instance.levelPuzzle >= 4 && GameManager.instance.levelPuzzle < 9){
			jigsawPuzzle.size = new Vector2(3,3);
            puzzle.transform.localScale = new Vector3(6.5f,6.5f, puzzle.transform.localScale.z);
		}
		else if (GameManager.instance.levelPuzzle >= 9){
			jigsawPuzzle.size = new Vector2(4,4);
            puzzle.transform.localScale = new Vector3(7,7, puzzle.transform.localScale.z);
		}
    }

    void Pieces()
    {
		// loop possible sizes - if on mobile platform we can not have too many polygons so cap size at Mode 5 max
        sizeMode++;
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) && sizeMode == 6) 
			sizeMode = 1;
		else
		if (sizeMode == 7) sizeMode = 1;
        SetSize();
    }

    void Restart()
    {
		// determine random topleft piece
        string topLeft = "" + ((int)(Mathf.Floor(Random.value * 5)) + 1) + ((int)(Mathf.Floor(Random.value * 5)) + 1);
        while (jigsawPuzzle.topLeftPiece == topLeft)
            topLeft = "" + ((int)(Mathf.Floor(Random.value * 5)) + 1) + ((int)(Mathf.Floor(Random.value * 5)) + 1);
		// set puzzle top left piece so restart is forced
        jigsawPuzzle.topLeftPiece = topLeft;
    }
	
	// handle GUI drawing
	//拼图
    // void OnGUI()
    // {
	// 	// we must have a puzzle
    //     if (jigsawPuzzle == null) 
    //     {
    //         Debug.LogError("JigsawPuzzle not found!");
    //         return;
    //     }
	// 	// draw titel image
    //     if (guiTitle!=null)
    //         GUI.DrawTexture(new Rect(5, 5, 472, 113), guiTitle, ScaleMode.ScaleToFit, true, 0f);
	// 	// draw menu
    //     if (guiMenu != null)
    //     {
    //         Vector2 mp = GuiMousePosition();
    //         // check current GUI mouse position 
    //         if (new Rect(8, 92, 107, 50).Contains(mp))
    //         {
    //             GUI.DrawTexture(new Rect(8, 92, 107, 150), guiMenuPuzzle, ScaleMode.ScaleToFit, true, 0f);
	// 			// check if 'puzzle' menu was clicked
    //             if (Input.GetMouseButtonUp(0) && Event.current.type == EventType.MouseUp)
    //                 Puzzle();
    //         }
    //         else
    //             if (new Rect(8, 142, 107, 50).Contains(mp))
    //             {
    //                 GUI.DrawTexture(new Rect(8, 92, 107, 150), guiMenuPieces, ScaleMode.ScaleToFit, true, 0f);
	// 				// check if 'pieces' menu was clicked
    //                 if (Input.GetMouseButtonUp(0) && Event.current.type == EventType.MouseUp)
    //                     Pieces();
    //             }
    //             else
    //                 if (new Rect(8, 192, 107, 50).Contains(mp))
    //                 {
    //                     GUI.DrawTexture(new Rect(8, 92, 107, 150), guiMenuRestart, ScaleMode.ScaleToFit, true, 0f);
	// 					// check if 'restart' menu was clicked
    //                     if (Input.GetMouseButtonUp(0) && Event.current.type == EventType.MouseUp)
    //                         Restart();
    //                 }
    //                 else
    //                     GUI.DrawTexture(new Rect(8, 92, 107, 150), guiMenu, ScaleMode.ScaleToFit, true, 0f);
    //     }
		
	// 	if (jigsawPuzzle.solved)
	// 	{
	// 		// if puzzle is solved display stats
	// 		GUI.skin.box.fontSize = 24;
	// 		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	// 		GUI.Box(new Rect((Screen.width-320),20,300,100), new GUIContent("- SOLVED -\n"+jigsawPuzzle.moves+" moves\n"+DispTime()));
	// 	}
		
    // }
	
	// format time display string
	private string DispTime()
	{
		if (jigsawPuzzle.time<60)
		{
			return string.Format("{0:0} seconds",jigsawPuzzle.time);
		}
		else
		{
			return string.Format("{0:0.0} minutes",jigsawPuzzle.time/60);
		}	
	}
	
}
