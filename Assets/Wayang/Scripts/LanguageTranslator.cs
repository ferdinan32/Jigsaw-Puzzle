using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageTranslator : MonoBehaviour {
	public static LanguageTranslator instance;
	public List<LanguageList> languageList;

	void Awake () {
        instance = this;
	}
}
