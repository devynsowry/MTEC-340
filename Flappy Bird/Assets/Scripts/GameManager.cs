using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public State CurrentState;

    public Player player;
    public Text scoreText;
    public GameObject scoreObject;
    public GameObject playButton;
    public GameObject title;
    public GameObject gameOver;
    public GameObject pauseButton;

    private int score;

    public void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Game setup
        CurrentState = State.Title;
        Application.targetFrameRate = 60;
        Pause();
        scoreObject.SetActive(false);
        gameOver.SetActive(false);
        playButton.SetActive(true);
        title.SetActive(true);
        pauseButton.SetActive(false);
    }

    private void Update()
    {
        if (CurrentState == State.Play || CurrentState == State.Pause)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                // Pause the game if player presses P
                Pause();
            }
        }
    }

    public void Play()
    {
        CurrentState = State.Play;

        // Reset game
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        player.enabled = true;
        Player.Instance.ResetPosition();

        // Remove UI
        scoreObject.SetActive(true);
        gameOver.SetActive(false);
        playButton.SetActive(false);
        title.SetActive(false);
        pauseButton.SetActive(true);

        // Destroy all previous pipes
        Movement[] pipes = FindObjectsOfType<Movement>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        if (CurrentState == State.Pause)
        {
            CurrentState = State.Play;

            // Start time
            Time.timeScale = 1f;
            player.enabled = true;
        }
        else
        {
            CurrentState = State.Pause;

            // Pause time
            Time.timeScale = 0f;
            player.enabled = false;
        }
    }

    public void GameOver()
    {
        CurrentState = State.GameOver;

        // Remove UI
        gameOver.SetActive(true);
        playButton.SetActive(true);
        pauseButton.SetActive(false);

        Pause();
    }

    public void IncreaseScore()
    {
        // Add 1 to score and update text
        score++;
        scoreText.text = score.ToString();
    }

    public void Boost()
    {
        score += 5;
        scoreText.text = score.ToString();
    }
}
