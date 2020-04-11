using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutScenes : MonoBehaviour
{
  [Tooltip("Reference to a UI image for fading in")]
  [SerializeField] public Image FadeImage;

  [Tooltip("Color to fade to")][SerializeField]
private Color EndColor = Color.white;

[Tooltip("Color to fade from")]
[SerializeField] private Color StartColor = Color.clear;


public void Start()
{
}

public void Update()
{
  //if(fadeIn) Fade("BlackImage", 1, 0); //Scene become clear
  //else Fade("BlackImage", 0, 1); // Scene become black
}

public void Fade(string FadingItem, float from, float to)
{
  Debug.Log("come to fade");
  iTween.ValueTo(this.gameObject, iTween.Hash("from", from, "to", to, "time", 1, "onupdate", FadingItem));
}

void BlackImage(float val)
{
    Color c = FadeImage.color;
    c.a = val;
    FadeImage.color = c;
}

}
