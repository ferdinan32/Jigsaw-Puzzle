using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[Header("Global Menu")]
	public Transform[] panelMenus;
	public GameObject[] lockLevel;
	public GameObject[] lockBook;

	public Text bookText;
	public Image bookImage;
	public Sprite[] bookSprites;
	public Slider musicSlider;
	public Slider sfxSlider;

	[Header("UI Text")]
	public Text uiPressAnyButton,
	uiTitleBook,
	uiTitleLevel,
	uiTitleSettings,
	uiTitleMusic,
	uiTitleLanguage,
	uiTitleExit,
	uiSubMusic,
	uiSubSfx,
	uiSubExitY,
	uiSubExitN,
	uiSubSettingMusic,
	uiSubSettingLanguage;
	

	private void Start() {
		MenuSelected(panelMenus[0]);
		SetMasterLanguage();

		for (int i = 0; i <= PlayerPrefs.GetInt("level"); i++)
		{
			lockLevel[i].SetActive(false);
			lockBook[i].SetActive(false);
		}		

		SoundManager.instance.PlayMusic(SoundManager.instance.musicsList, 1);
	}

	void SetMasterLanguage(){
		uiTitleLanguage.text = uiSubSettingLanguage.text = GameManager.instance.languageSet[0];
		uiPressAnyButton.text = GameManager.instance.languageSet[1];
		uiTitleBook.text = GameManager.instance.languageSet[2];		
		uiTitleSettings.text = GameManager.instance.languageSet[3];
		uiTitleMusic.text = uiSubMusic.text = uiSubSettingMusic.text = GameManager.instance.languageSet[4];
		uiSubSfx.text = GameManager.instance.languageSet[5];
		uiTitleExit.text = GameManager.instance.languageSet[6];
		uiSubExitN.text = GameManager.instance.languageSet[7];
		uiSubExitY.text = GameManager.instance.languageSet[8];
		uiTitleLevel.text = GameManager.instance.languageSet[9];		
	}	

	public void PlayGames(int levelGame){
		if(levelGame > PlayerPrefs.GetInt("level")){
			return;
		}

		GameManager.instance.levelPuzzle = levelGame;

		GameManager.instance.LoadingScene(1);

		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 0);
	}

	public void ExitGames(){
		Application.Quit();
	}

	public void MenuSelected(Transform panelMenu){
		SetMasterLanguage();
		
		for (int i = 0; i < panelMenus.Length; i++){
			panelMenus[i].localPosition = new Vector2 (2000,2000);
		}

		panelMenu.localPosition = new Vector2 (0,0);
		SoundManager.instance.PlayMusic(SoundManager.instance.sfxsList, 0);
	}

	public void MusicController(bool isMute){
		SoundManager.instance.music.SetActive(!isMute);
		SoundManager.instance.PlayMusic(SoundManager.instance.musicsList, 1);
	}
	public void SfxController(bool isMute){
		SoundManager.instance.sfx.SetActive(!isMute);
	}

	public void OpenBook(int levelBook){
		if(levelBook > PlayerPrefs.GetInt("level")){
			return;
		}

		bookImage.sprite = bookSprites[levelBook];
		bookText.text = GameManager.instance.languageBook[levelBook];

		MenuSelected(panelMenus[6]);		
	}

	public void ChangeVolume(bool isMusic){
		if(isMusic){
			for (int i = 0; i < SoundManager.instance.musicsList.Length; i++){
				SoundManager.instance.musicsList[i].volume = musicSlider.value;
			}
		}else{
			for (int i = 0; i < SoundManager.instance.sfxsList.Length; i++){
				SoundManager.instance.sfxsList[i].volume = sfxSlider.value;
			}
		}		
	}
}
