using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LanguageList {
	public DataCallBack data;
}

[System.Serializable]
public class DataCallBack
{
    public string[] language_set;
	public string[] language_loading;
	public string[] language_book;
}
