using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerLocal : MonoBehaviour {
	[SerializeField] public GameObject pausePanel;
	[SerializeField] private GameObject StartGameCanvas;
	[SerializeField] private GameObject GamePlayCanvas;

	[SerializeField] public GameObject groundPlayer;
	[SerializeField] private GameObject objectParent;
	[SerializeField] private GameObject groundPlayerPrefab;

	[SerializeField] private GameObject ObstacleShredderSky;


	[SerializeField] private Button restartGameButton;
	[SerializeField] private Button jumpGameButton;


	[SerializeField] public Text scoreText, pauseText;
	public List <GameObject> textMessage;
	public GameObject breathingIsland;
	public GameObject homeIsland;



	public int score = 0;

	private int messageNum = 0;


	public GameObject obstaclesObj;
	public GameObject gameAudio;

	public SingleJoystickPlayerController singleJoystickPlayerController;
	public LevelGenerator levelGenerator;

	private float startTime;
	public Text timerText;
	public bool timerStart;
	string minutes;
	string seconds;
	private int minutesInt;

	public static bool playerPositionedOnBreathingIsland;
	public static bool playerPositionedOnHomeIsland;

	public float playerPositionX;
	public float playerPositionY;
	public GameObject _player;

	public bool gameStart;
	public GameObject FadingObject;


	// Use this for initialization
	void Start () {
				gameAudio.SetActive(false);
				int messageNum = 0;
				//Time.timeScale = 0f;
				StartGameCanvas.SetActive(true);
				GamePlayCanvas.SetActive(false);
				FindObjectOfType<BGLooper>().scrollingEnable = false;
				playerPositionedOnBreathingIsland = false;
				BreathingPractice.healingDone = false;
				playerPositionedOnHomeIsland = false;
				OnEnable();
				gameStart = true;

	}

	void Update()
	{
		Debug.Log("SS");
		Timer();
		if(gameStart)
		{
			FindObjectOfType<BGLooper>().scrollingEnable = false;
		}

		// Add restart game function
		ObstacleShredderSky.SetActive(true);
		score = GameConstants.SCORE;
		if(score == 5)
		{
			Debug.Log("messageNum"+messageNum);
			textMessage[messageNum].SetActive (true);
		}
		if(score == 10)
		{
			textMessage[messageNum+1].SetActive (true);
		}
//************** Breathing Island Comming
		if(((int) minutesInt % 2) == 0   && seconds == "0" && !BreathingPractice.healingDone )
		{//&& minutes != "0"
			breathingIsland.SetActive (true);
			playerPositionX = _player.transform.position.x;

		}

		if(((int) minutesInt % 2) == 0  && seconds == "11" && !BreathingPractice.healingDone )
		{//&& minutes != "0"
			playerPositionY = _player.transform.position.y;
			SingleJoystickPlayerController.playerXMovementEnable = true;
			SingleJoystickPlayerController.playerYMovementEnable = true;

			timerStart = false;
			FindObjectOfType<BGLooper>().scrollingEnable = false;
		}
		if(playerPositionedOnBreathingIsland && !BreathingPractice.healingDone )
		StartCoroutine(FadingOutSideScrollerToBreathing());
//**************

//************** Home Island Comming
		if(((int) minutesInt % 3) == 0  && seconds == "30")
		{// && minutes != "0"
			homeIsland.SetActive (true);
			playerPositionX = _player.transform.position.x;

		}

		if(((int) minutesInt % 3) == 0  && seconds == "41" )
		{//&& minutes != "0"
			playerPositionY = _player.transform.position.y;
			SingleJoystickPlayerController.playerXMovementEnable = true;
			SingleJoystickPlayerController.playerYMovementEnable = true;

			timerStart = false;
			FindObjectOfType<BGLooper>().scrollingEnable = false;
		}

		if(playerPositionedOnHomeIsland )
		StartCoroutine(FadingOutSideScrollerToHome());
//**************

		if(score == 20)
		{
			textMessage[messageNum+2].SetActive (true);
			GameConstants.SCORE = 0;

		}

	}


	public void OnEnable ()
	{
		gameAudio.SetActive(false);
		//FindObjectOfType<GameManager>().comeFromOut = true;//for breathing practice to show start of game in the begin

		foreach (Transform child in obstaclesObj.transform)
{
		child.gameObject.SetActive(false);
}


		Destroy(groundPlayer);

		var cc = Instantiate(groundPlayerPrefab);
		this.groundPlayer = cc;
		this.groundPlayer.transform.SetParent(objectParent.transform.parent, false);
		singleJoystickPlayerController.myRotationObject = this.groundPlayer.transform;
		levelGenerator.player = this.groundPlayer;

		StartGameCanvas.SetActive(true);
		pausePanel.SetActive(false);
		pauseText.text = "";

	}

	// Restart function
	public void RestartGame ()
	{
		gameStart = false;
		//fadingAnim.SetTrigger("Appear");
		SingleJoystickPlayerController.playerXMovementEnable = false;
		SingleJoystickPlayerController.playerYMovementEnable = true;

		playerPositionedOnBreathingIsland = false;
		timerStart = true;
		OnEnable();
		gameAudio.SetActive(true);
		FindObjectOfType<BGLooper>().scrollingEnable = true;
		// Reload level
		StartGameCanvas.SetActive(false);
		GamePlayCanvas.SetActive(true);
		pausePanel.SetActive(false);
	}


	public void PauseButton ()
	{
		// freeze game
		Time.timeScale = 0f;

		// activate and display the pause panel
		pausePanel.SetActive(true);

	}

	public void ActivateJoystick()
	{
		singleJoystickPlayerController.singleJoystick.isActive = true;
	}
	public void DeactivateJoystick()
	{
		singleJoystickPlayerController.singleJoystick.isActive = false;
	}

	IEnumerator GameOverBreathing()
	{
		yield return new WaitForSeconds(3f);
		//FindObjectOfType<GameManager>().HomeState(4);
	}

	public void ResumeGame()
	{
		// freeze game
		Time.timeScale = 1f;

		// activate and display the pause panel
		pausePanel.SetActive(false);
	}


	public void GoToStart()
	{
		StartGameCanvas.SetActive(true);
		GamePlayCanvas.SetActive(false);
		pausePanel.SetActive(false);
		SceneManager.LoadScene("RunnerScene"); //Load scene called Game
	}

	public void Timer()
  {
    if(!timerStart) startTime = Time.time;
    if(timerStart)
    {
      var t = Time.time - startTime;
      minutes = ((int) t / 60).ToString();
			minutesInt =((int) t / 60);
      seconds = ((int) t % 60).ToString();
      timerText.text = minutes + ":" + seconds;
    }
  }


	IEnumerator FadingOutSideScrollerToBreathing()
	{
		//FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 0, 1);//Scene become black
		SingleJoystickPlayerController.playerXMovementEnable = false;
		SingleJoystickPlayerController.playerYMovementEnable = false;
		if(FindObjectOfType<FadeInOutScenes>().FadeImage.color == Color.clear)
		{
			FadingObject.transform.gameObject.GetComponent<Animator>().enabled = true;
	  }
	  yield return new WaitUntil(()=>FindObjectOfType<FadeInOutScenes>().FadeImage.color.a==1);
		FadingObject.transform.gameObject.GetComponent<Animator>().enabled = false;
		FadingObject.transform.gameObject.GetComponent<Animator>().Play("FadeInBlackImage", -1, 0f);
		_player.transform.position = new Vector3(playerPositionX, playerPositionY, 0f);
		breathingIsland.transform.position = new Vector3(playerPositionX, breathingIsland.transform.position.y, 0f);
		FindObjectOfType<GameManager>().GameState(2);
	}

	IEnumerator FadingOutSideScrollerToHome()
	{
		SingleJoystickPlayerController.playerXMovementEnable = false;
		SingleJoystickPlayerController.playerYMovementEnable = false;
		//FindObjectOfType<FadeInOutScenes>().Fade("BlackImage", 0, 1);//Scene become black
		if(FindObjectOfType<FadeInOutScenes>().FadeImage.color == Color.clear)
		{
			FadingObject.transform.gameObject.GetComponent<Animator>().enabled = true;
		}
		yield return new WaitUntil(()=>FindObjectOfType<FadeInOutScenes>().FadeImage.color.a==1);
		FadingObject.transform.gameObject.GetComponent<Animator>().enabled = false;
		FadingObject.transform.gameObject.GetComponent<Animator>().Play("FadeInBlackImage", -1, 0f);
		FindObjectOfType<GameManager>().GameState(0);
	}

}
