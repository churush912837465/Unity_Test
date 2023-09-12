using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image[] arr;
    public Text _text;
    public InputField inputText;


    public void OnOffImage(int cnt) 
    {
        if (arr[cnt].gameObject.activeSelf)
        {
            arr[cnt].gameObject.SetActive(false);
        }
        else 
        {
            arr[cnt].gameObject.SetActive(true);
        }
    }

    public void getName() 
    {
        _text.text = inputText.text;
    }
}
