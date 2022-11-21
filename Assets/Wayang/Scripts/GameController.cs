using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public DemoJigsawPuzzle jigsawPuzzle;
	public Demo guiController;
	public GameObject effectWin;
	public GameObject cloundWin;
	public GameObject bgMenuIngame;
	public GameObject nextButton;
	public GameObject restartButton;
	public GameObject[] starCount;
	public Transform[] panelMenuIngame;

	public Text winCondition;
	public Text pauseTitle;
	public Text wayangText;
	public Sprite[] winImage;
	public Sprite[] wayangSprite;

	public Image wayangImage;
	public Image timerImage;
	public float timerPerStage;
	float timerUpdate;
	float timeStarCountOne;
	float timeStarCountTwo;

	bool isSolved;
	bool isPause;
	string[] winText = new string[2];

	private void Awake() {
		DefaultState();

		SoundManager.instance.PlayMusic(SoundManager.instance.musicsList, 0);
	}

	private void Update() {
		if(!isSolved){
			if(!isPause){
				timerUpdate--;
			}
			
			if (jigsawPuzzle.solved){
				StartCoroutine(GameComplete(true));
			}

			if(!jigsawPuzzle.solved && timerUpdate!=0){
				timerImage.fillAmount = timerUpdate/(timerPerStage*60);
			}
			else if(!jigsawPuzzle.solved && timerUpdate<=0){
				StartCoroutine(GameComplete(false));
			}

			if(timerUpdate == (4*60)){
				SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 2);
			}
		}		
	}

	void DefaultState(){
		winText[0] = GameManager.instance.languageSet[11];
		winText[1] = GameManager.instance.languageSet[12];
		pauseTitle.text = GameManager.instance.languageSet[13];		

		wayangImage.sprite = wayangSprite[GameManager.instance.levelPuzzle];
		wayangText.text = GameManager.instance.languageBook[GameManager.instance.levelPuzzle];
		
		if (GameManager.instance.levelPuzzle < 4){
			timerPerStage = 10;
		}
		else if (GameManager.instance.levelPuzzle >= 4 && GameManager.instance.levelPuzzle < 9){
			timerPerStage = 15;
		}
		else if (GameManager.instance.levelPuzzle >= 9){
			timerPerStage = 25;
		}

		timerUpdate = timerPerStage*60;
		timeStarCountOne = (timerPerStage*60)/2;
		timeStarCountTwo = (timerPerStage*60)/4;
	}

	public void GoToMenu(){		
		SaveLevelGame();
		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 1);
		GameManager.instance.LoadingScene(-1);
	}

	void OpenMenuIngame(int indexMenu, bool isMenu){
		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 0);
		bgMenuIngame.SetActive(isMenu);

		for (int i = 0; i < panelMenuIngame.Length; i++){
			panelMenuIngame[i].localPosition = new Vector2 (2000,2000);
		}

		if(isMenu){
			panelMenuIngame[indexMenu].localPosition = new Vector2 (0,0);
		}		
	}

	public void RestartGame(){
		SaveLevelGame();

		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 1);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void PauseMenu(bool _isPause){
		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 1);

		OpenMenuIngame(0,_isPause);
		isPause = _isPause;

		for (int i = 0; i < jigsawPuzzle.pieces.Count; i++)
		{
			jigsawPuzzle.pieces[i].GetComponent<BoxCollider>().enabled = !_isPause;
		}
	}

	IEnumerator GameComplete(bool isWin){
		int winVal = isWin ? 0:1 ;
		if(isWin){
			SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 4);
		}
		isSolved = true;
		effectWin.SetActive(isWin);
		yield return new WaitForSeconds(1);
		OpenMenuIngame(1,true);
		for (int i = 0; i < starCount.Length; i++){
			starCount[i].SetActive(true);
		}

		if(timerUpdate <= timeStarCountOne && timerUpdate > timeStarCountTwo){
			starCount[2].SetActive(false);
		}
		else if(timerUpdate <= timeStarCountTwo){
			starCount[1].SetActive(false);
			starCount[2].SetActive(false);
		}
		
		winCondition.transform.parent.GetComponent<Image>().sprite = winImage[winVal];
		winCondition.text = winText[winVal];
		cloundWin.SetActive(isWin);
		nextButton.SetActive(isWin);
		restartButton.SetActive(isWin);
		starCount[0].transform.parent.gameObject.SetActive(isWin);

		if(!isWin){
			SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 3);
		}
	}

	public void NextLevel(bool isReaded){	
		if(isReaded){
			RestartGame();
		}else{
			GameManager.instance.levelPuzzle+=1;
		}
		
		if(GameManager.instance.levelPuzzle<jigsawPuzzle.image.Length){
			//RestartGame();
			OpenMenuIngame(2,true);
		}else{			
			GoToMenu();
		}		
	}

	void SaveLevelGame(){
		if(GameManager.instance.levelPuzzle >= jigsawPuzzle.image.Length){
			GameManager.instance.levelPuzzle = (jigsawPuzzle.image.Length-1);
		}

		PlayerPrefs.SetInt("level", GameManager.instance.levelPuzzle);
	}
}
