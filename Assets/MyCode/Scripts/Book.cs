using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public GameObject[] pages;
    
    private int pageIndex;
    private GameObject[] pageTexts;
    private Vector3 mouseLastPos;
    private Vector3 openPos;
    private Vector3 closePos;

    void Start()
    {
        pageTexts = new GameObject[pages.Length];
        for (int i = 0; i < pages.Length; i++)
            pageTexts[i] = pages[i].GetComponentInChildren<Text>().gameObject;

        openPos = new Vector3(Screen.width/2, Screen.height/2, 0);
        closePos = gameObject.transform.position;
    }
    void Update()
    {
        SwapPage();
    }

    void SwapPage()
    {
        if (Input.GetMouseButtonDown(0))
            mouseLastPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (Mathf.Abs(mouseLastPos.x-Input.mousePosition.x) > 50)
            {
                if (mouseLastPos.x < Input.mousePosition.x)
                {
                    if (pageIndex < pages.Length)
                    {
                        if (pages[pageIndex].transform.rotation.y == 0)
                        {
                            iTween.RotateTo(pages[pageIndex], new Vector3(0, -180, 0), 1);
                            pageTexts[pageIndex].SetActive(false);
                            pageIndex++;
                        }
                    }
                }
                else
                {
                    if (pageIndex > 0)
                    {
                        if (pages[pageIndex-1].transform.rotation.y == -1)
                        {
                            iTween.RotateTo(pages[pageIndex-1], new Vector3(0, 0, 0), 1);
                            pageTexts[pageIndex-1].SetActive(true);
                            pageIndex--;
                        }
                    }
                }
            }
        }
    }

    public void OpenBook()
    {
        if (gameObject.transform.position != openPos)
            iTween.MoveTo(gameObject, openPos, 1);
        else
            iTween.MoveTo(gameObject, closePos, 1);
    }
}
