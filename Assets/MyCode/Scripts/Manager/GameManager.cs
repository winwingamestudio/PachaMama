using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
  public  bool IAmNotInScrollerSideGame = false;
  public bool comeFromOut;//for breathinpractice to show start in the beginig of practice

  public GameObject audioOnIcon;
  public GameObject audioOffIcon;

  public GameObject roomGame;
  public GameObject sideScrollerGame;
  public GameObject breathingGame;



  public int gameState;
  public bool gameStart;


  public int currentScore;
  public Text scoreText;

  public bool ballonHasAir = true;
  public  bool IAmInGame = false;


  private static GameManager _instance = null;
  public static GameManager Instance{
    get{if(_instance == null){
      _instance = new GameManager();
      }
      return _instance;
    }
  }

  private GameManager(){
    _instance = this;
  }

public void start()
{
  if(!IAmNotInScrollerSideGame)
  {
    GameConstants.GAMESTATE = 0;
    currentScore = 0;
  }

  gameStart = true;

}

private float startTime;
public Text timerText;
public bool timerStart;
string minutes;
string seconds;

public int level;
public Text TxtLevel;

public int highScore;
public Text TxtHighScore;


  // Update is called once per frame
  void Update()
  {
    if(gameStart)
    {
      gameStart = false;
    }

  }

  public void Timer()
  {
    if(!timerStart) startTime = Time.time;
    if(timerStart)
    {
      var t = Time.time - startTime;
      minutes = ((int) t / 60).ToString();
      seconds = ((int) t % 60).ToString();
      timerText.text = minutes + ":" + seconds;
    }
  }


  public void Awake(){
    _instance = this;
    gameState = GameConstants.GAMESTATE;

    SwitchGameState();

  }


  public void GameState(int _gameState)
  {
    timerStart = false;
    gameState = _gameState;
    GameConstants.GAMESTATE = gameState;
    if(gameState == 1) StartCoroutine(FadeOutScene());
    else SwitchGameState();
  }


  public void SwitchGameState()
  {

    switch (gameState)
    {
      case 0://HomeGame
      BreathingPractice.healingDone = false;
      FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 1, 0);// Scene become clear
      roomGame.SetActive(true);
      sideScrollerGame.SetActive(false);
      breathingGame.SetActive(false);
          break;
        case 1://SideScrollerGame
        GameManagerLocal.playerPositionedOnBreathingIsland = false;
        GameManagerLocal.playerPositionedOnHomeIsland = false;
        FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 1, 0);// Scene become clear
        roomGame.SetActive(false);
        sideScrollerGame.SetActive(true);
        breathingGame.SetActive(false);
            break;
        case 2://BreathingGame
        FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 1, 0);// Scene become clear
        roomGame.SetActive(false);
        sideScrollerGame.SetActive(false);
        breathingGame.SetActive(true);
            break;
        default:
            Debug.Log("Error");
            break;
    }
  }

public void GetScore()
{
  currentScore += 1;
  GameConstants.SCORE = currentScore;
  scoreText.text = "" + currentScore;

  Book.SavePages(currentScore);
}

public void LostScore(int i)
{
  currentScore -= i;
  GameConstants.SCORE = currentScore;
  scoreText.text = "" + currentScore;
}

IEnumerator FadeOutScene()
{
  FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 0, 1);//Scene become black
  yield return new WaitUntil(()=>FindObjectOfType<FadeInOutScenes>().FadeImage.color.a==1);
  SwitchGameState();
}

}
