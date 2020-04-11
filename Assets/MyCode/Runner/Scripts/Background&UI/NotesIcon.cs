using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesIcon : MonoBehaviour
{
  [Tooltip("Reference to a UI image for fading in")]
  [SerializeField] private Image[] FadeImages;
  [SerializeField] private int ImagesIndex;

  [Tooltip("Color to fade to")][SerializeField]
private Color EndColor = Color.white;

[Tooltip("Color to fade from")][SerializeField]
private Color StartColor = Color.clear;

public void Start()
{
  for(int i = 0; i < FadeImages.Length; i++)
  FadeImages[i].color = Color.white;

}

public void Update()
{
  // FadeInOrOut(i, j);// i = index of item, j = true FadeIn, j = false FadeOut
  FadeInOrOut(0, false);
}

public void FadeInOrOut(int i, bool In)
{
  ImagesIndex = i;
  if (In) Fade("Note", 1, 0);
  else Fade("Note", 0, 1);
}
void Fade(string FadingItem, float from, float to)
{
    iTween.ValueTo(this.gameObject, iTween.Hash("from", from, "to", to, "time", 1, "onupdate", FadingItem));
}

void Note(float val)
{
    Color c = FadeImages[ImagesIndex].color;
    c.a = val;
    FadeImages[ImagesIndex].color = c;
}

}
