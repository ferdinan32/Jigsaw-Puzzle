using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public TextAsset[] languageTxt;
	public int idBahasa;
	public int idScene;
	public int levelPuzzle;
	
	public string[] languageSet;
	[TextArea(15,20)]
	public string[] languageLoading;
	[TextArea(15,20)]
	public string[] languageBook;
	
	void Awake () {
		if (instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		idBahasa = PlayerPrefs.GetInt("bahasa");
	}

	public void LoadingScene(int _idScene){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+(_idScene), LoadSceneMode.Additive);
		idScene = _idScene;
	}

}
