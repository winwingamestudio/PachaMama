using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;

public class Book : MonoBehaviour
{
    public GameObject[] pages;
    
    private int pageIndex;
    private Text[] pageTexts;
    private string[] pageStrings;
    private Vector3 mouseLastPos;
    private Vector3 openPos;
    private Vector3 closePos;

    void Start()
    {
        pageTexts = new Text[pages.Length];
        pageStrings = new string[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            pageTexts[i] = pages[i].GetComponentInChildren<Text>();
            pageStrings[i] = pageTexts[i].text;
            pageTexts[i].text = "Page is not available";
        }

        openPos = new Vector3(Screen.width/2, Screen.height/2, 0);
        closePos = gameObject.transform.position;

        //ObscuredPrefs.DeleteKey("AvailablePages");
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
                            pageTexts[pageIndex].gameObject.SetActive(false);
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
                            pageTexts[pageIndex-1].gameObject.SetActive(true);
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
        {
            int n = LoadPages();
            for (int i = 0; i < n; i++)
                pageTexts[i].text = pageStrings[i];
            
            iTween.MoveTo(gameObject, openPos, 1);
        }
        else
            iTween.MoveTo(gameObject, closePos, 1);
    }

    public static void SavePages(int val)
    {
        ObscuredPrefs.SetInt("AvailablePages", val);
    }

    public static int LoadPages()
    {
        if (ObscuredPrefs.HasKey("AvailablePages"))
            return ObscuredPrefs.GetInt("AvailablePages");
        else
            return 0;
    }
}
