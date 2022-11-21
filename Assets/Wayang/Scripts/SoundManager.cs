using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance;
	public GameObject music;
	public GameObject sfx;

	public AudioSource[] musicsList;
	public AudioSource[] sfxsList;

	// Use this for initialization
	void Start () {
		if (instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }
	}

	public void PlayMusic(AudioSource[] soundList, int soundId){
		for (int i = 0; i < soundList.Length; i++){
			soundList[i].Stop();
		}

		soundList[soundId].Play();
	}
}
