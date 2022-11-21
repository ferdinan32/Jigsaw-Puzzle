using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {
	public Transform parentPanel;
	public Transform prefabPanelBahasa;
	public Image spalshImage;
	public Sprite[] splashSprite;
	public Transform panelBahasa;
	int splsahSpriteCount;
	float splashInSecound=60;
	float splashShowTime=0.5f;
	float splashTimer;
	bool isSplashWait;
	bool isLanguageSelect;

#if UNITY_EDITOR
	private void Awake() {		
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("level", 12);
	}
#endif

	// Use this for initialization
	void Start () {
		splashShowTime = splashInSecound * splashShowTime;
		spalshImage.color = new Color(1,1,1,0);
		isLanguageSelect = PlayerPrefs.GetInt("isBahasa")==0 ? false : true;
	}
	
	// Update is called once per frame
	void Update () {
		if(spalshImage.sprite == splashSprite[splashSprite.Length-1] && splashTimer == (splashShowTime*2)-1 && isSplashWait){
			if(!isLanguageSelect){
				panelBahasa.gameObject.SetActive(true);
			}else{
				this.GetComponent<LanguageManager>().LanguageSelected(GameManager.instance.idBahasa);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
			}
			return;
		}

		splashTimer++;

		if(splashTimer >= splashShowTime && splashTimer < (splashShowTime*2)){			
			isSplashWait = true;
			spalshImage.color = new Color (1,1,1,(splashShowTime/splashTimer)-0.5f);			
		}
		else if(splashTimer == (splashShowTime*2)){
			splashTimer = 0;
			splsahSpriteCount+=1;
			isSplashWait = false;
			spalshImage.sprite = splashSprite[splsahSpriteCount];
		}		

		if(splashShowTime > 0 && !isSplashWait){
			spalshImage.color = new Color (1,1,1,splashTimer/splashShowTime);
		}
	}
}
