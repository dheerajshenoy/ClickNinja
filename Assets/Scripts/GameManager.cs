using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public List<Target> spawnedTargetList;
    public BackgroundColor bgColorChanger;
    private float spawnRate = 2.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int score;
    public int maxScore;
    public bool isGameRunning;
    public bool isGamePaused;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI creatorText;
    public AudioSource audioSource;
    private readonly int defaultVolume = 20;
    public List<AudioClip> backgroundMusicList;
    public AudioSource effectSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
        audioSource.clip = backgroundMusicList[UnityEngine.Random.Range(0, backgroundMusicList.Count)];
        audioSource.Play();
        titleScreen.gameObject.SetActive(true);
    }

    IEnumerator spawnTargets()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = UnityEngine.Random.Range(0, targets.Count);
            //int index = 11;
            Instantiate(targets[index]);
        }
    }

    public void updateScore(int scoreAdd)
    {
        int temp = score + scoreAdd;
        if (temp > maxScore)
            maxScore = temp;

        if (temp < 0) return;
        score = temp;
        scoreText.text = "Score: " + score;
    }

    public void StartGame(int difficulty)
    {
        
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        spawnRate = spawnRate / difficulty;
        isGameRunning = true;
        StartCoroutine(spawnTargets());
        score = 0;
        updateScore(0);
        bgColorChanger.updateColor();
    }

    public void GameOver()
    {
        highScoreText.text = "High Score: " + maxScore;
        isGameRunning = false;
        gameOverScreen.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeVolume()
    {
        volumeText.text = "VOL: " + Mathf.Round(volumeSlider.value * 100);
        audioSource.volume = volumeSlider.value;

    }

    public void PauseGame(bool pause)
    {
        if (!isGameRunning) return;
        if (pause)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        isGamePaused = pause;
        pauseScreen.gameObject.SetActive(isGamePaused);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            PauseGame(isGamePaused);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
