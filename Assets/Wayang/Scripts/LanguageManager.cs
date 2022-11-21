using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour {

	public bool isSplashScreen;
	public GameObject backButton;

	private void Start() {
		backButton.SetActive(!isSplashScreen);		
	}

	public void LanguageSelected(int idLanguage){		
		PlayerPrefs.SetInt("isBahasa",1);
		PlayerPrefs.SetInt("bahasa",idLanguage);
				
		GameManager.instance.idBahasa = PlayerPrefs.GetInt("bahasa");		

		LanguageList callBacLanguage = new LanguageList ();
		callBacLanguage = JsonUtility.FromJson<LanguageList> (GameManager.instance.languageTxt[GameManager.instance.idBahasa].ToString());
		GameManager.instance.languageSet = callBacLanguage.data.language_set;
		GameManager.instance.languageLoading = callBacLanguage.data.language_loading;
		GameManager.instance.languageBook = callBacLanguage.data.language_book;
	}

	public void NextSceneLanguage(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void BackToMenu(){
		this.transform.localPosition = new Vector2(2000,2000);
	}
}
