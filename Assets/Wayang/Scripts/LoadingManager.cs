using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
    private AsyncOperation aoUnload;
    private AsyncOperation aoLoad;

    public Image loadingImage;
    public Text loadingText;
    public Text loadingButtom;
    public Sprite[] loadingSprite;

	private void Start() {
        int loadingRandom;
        loadingRandom = Random.Range(0,loadingSprite.Length);
        loadingButtom.text = GameManager.instance.languageSet[10];
		
        StartCoroutine(LoadSceneAfterFinished());
        
        loadingImage.sprite = loadingSprite[loadingRandom];
        loadingText.text = GameManager.instance.languageLoading[loadingRandom];

        SoundManager.instance.PlayMusic(SoundManager.instance.musicsList, 2);
	}

	private IEnumerator LoadSceneAfterFinished()
    {
        aoUnload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		yield return new WaitForSeconds(5);
        yield return new WaitUntil (() => aoUnload.isDone);
        aoLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+(GameManager.instance.idScene), LoadSceneMode.Additive);
        yield return new WaitUntil(() => aoLoad.isDone);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

}
