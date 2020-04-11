using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Complete Script for Breathing
public class BreathingPractice : MonoBehaviour
{
  //public GameObject breathingPracticeAnimation;
  public GameObject ballon;
  public GameObject ballonParent;
  public GameObject ballonPrefab;

  public GameObject micVolume;
  public GameObject circleBreathing;
  public GameObject cloudParent;
  public GameObject cloudsNew;
  public GameObject cloudsPrefabNew;


  public GameObject gameCanvas;
  public GameObject gameOverCanvas;
  [SerializeField] private Text GameOverText;


  public static bool healingDone = false;

  public GameObject inhaleTxt;
  public GameObject exhaleTxt;
  public GameObject inhaleSound;
  public GameObject exhaleSound;

  public GameObject breathingPractice;

  public GameObject replayBtn;



  public GameObject chooseBreathingOptionPanel;
  private bool _useMicrophone;



  void Start()
  {
    FindObjectOfType<GameManager>().comeFromOut = true;//for breathing practice to show start of game in the begin
    GameStart();

  }


    // Start is called before the first frame update
    public void OnEnable()
    {
      if(FindObjectOfType<GameManager>().comeFromOut)
      {
        //GameOverText.text = " Breathing into your microphone!";
        GameStart();
      }
      else
      {

      //GameOverText.text = " Breathing into your microphone!";
      Destroy(cloudsNew);
      //Destroy(ballon);
      healingDone = false;

      //breathingPracticeAnimation.SetActive(true);
      gameCanvas.SetActive(true);
      gameOverCanvas.SetActive(false);

      var cc = Instantiate(cloudsPrefabNew);
      this.cloudsNew = cc;
      this.cloudsNew.transform.SetParent(cloudParent.transform.parent, false);

      Destroy(ballon);
      var bb = Instantiate(ballonPrefab);
      if(FindObjectOfType<GameManager>().gameState == 9)
      bb.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);

      this.ballon = bb;
      this.ballon.transform.SetParent(ballonParent.transform.parent, false);

     cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = false;

      inhaleTxt.SetActive(false);
      exhaleTxt.SetActive(false);
    }

    }



    // Update is called once per frame

    void Update()
    {
      SingleJoystickPlayerController.playerXMovementEnable = false;
      if(circleBreathing.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BreathingAnim"))
      {
        Destroy(cloudsNew);
        var cc = Instantiate(cloudsPrefabNew);
        this.cloudsNew = cc;
        this.cloudsNew.transform.SetParent(cloudParent.transform.parent, false);
        cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = false;

        inhaleTxt.SetActive(true);
        exhaleTxt.SetActive(false);
        inhaleSound.SetActive(true);
        exhaleSound.SetActive(false);

      }

      if (circleBreathing.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BreathingPauseAnim"))
      {
        if(cloudsNew.transform.gameObject.GetComponent<Image>().color.a<0.1f && !healingDone)
        {
          //Blowing Ballon
          ballon.GetComponent<Animator>().enabled = true;
          healingDone = true;
        }
        if(healingDone == true)
        {
          StartCoroutine(GameOver());

        }
      }

      if(circleBreathing.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BreathOutAnim"))
      {
        inhaleTxt.SetActive(false);
        exhaleTxt.SetActive(true);
        inhaleSound.SetActive(false);
        exhaleSound.SetActive(true);

        if (_useMicrophone == true)
        {
          if( micVolume.GetComponent<MicrophoneInput>().loudness > 10)
          cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = true;

          if( micVolume.GetComponent<MicrophoneInput>().loudness < 2 )
          cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = false;
        }
        else
        {
          if (Input.GetMouseButton(0))
          {
              cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = true;
          }
          else
          {
              cloudsNew.transform.gameObject.GetComponent<Animator>().enabled = false;
          }
        }
      }

    }
    IEnumerator WaitFunctionForGoToScrollerGame()
    {
      yield return new WaitForSeconds(3f);
      FindObjectOfType<GameManager>().GameState(1);
    }

    IEnumerator GameOver()
    {


      if(healingDone)
      {
        //GameOverText.text = "Congratulation :)";
        FindObjectOfType<GameManager>().IAmInGame = false;
        replayBtn.SetActive(false);
        FindObjectOfType<GameManager>().ballonHasAir = true;
        Debug.Log("ballonHasAir" + FindObjectOfType<GameManager>().ballonHasAir);
        StartCoroutine(WaitFunctionForGoToScrollerGame());
        circleBreathing.GetComponent<Animator>().enabled = false;
        inhaleSound.SetActive(false);
        exhaleSound.SetActive(false);
      }
      yield return new WaitForSeconds(3f);
      breathingPractice.SetActive(false);

      Debug.Log("GameOveriiiing");
      breathingPractice.SetActive(true);

      Destroy(cloudsNew);
      //Destroy(ballon);
      gameCanvas.SetActive(false);
      //replayBtn.SetActive(true);
      gameOverCanvas.SetActive(true);
      FindObjectOfType<GameManager>().comeFromOut = false;
  }

    public void GameStart()
    {
      breathingPractice.SetActive(true);

      Destroy(cloudsNew);
      //Destroy(ballon);
      gameCanvas.SetActive(false);
      gameOverCanvas.SetActive(true);
      FindObjectOfType<GameManager>().comeFromOut = false;
    }

    public void UseMicrophone()
    {
        _useMicrophone = true;
        chooseBreathingOptionPanel.SetActive(false);
        inhaleTxt.GetComponent<Text>().text = "Inhale";
        exhaleTxt.GetComponent<Text>().text = "Exhale";
        replayBtn.SetActive(false);
        StartCoroutine(WaitFunctionForGameStart());
    }

    public void UseTouch()
    {
        _useMicrophone = false;
        chooseBreathingOptionPanel.SetActive(false);
        inhaleTxt.GetComponent<Text>().text = "Release";
        exhaleTxt.GetComponent<Text>().text = "Hold";
        replayBtn.SetActive(false);
        StartCoroutine(WaitFunctionForGameStart());
    }

    IEnumerator WaitFunctionForGameStart()
    {
      yield return new WaitForSeconds(2f);
      OnEnable();
    }

}
