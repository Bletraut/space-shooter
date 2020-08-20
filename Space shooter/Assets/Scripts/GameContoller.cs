using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour
{
    [Header("Level settings")]
    public LevelData LevelData;
    public LevelList LevelList;

    [Header("Game settings")]
    public bool IsPause = false;
    public bool IsGameOver = false;

    public int Score = 0;
    public Text TextVictoryInfo;
    public Text TextLifes;

    [Header("Player")]
    public GameObject PlayerPrefab;
    public VariableJoystick Joystick;
    private GameObject player;

    [Header("Screens")]
    public GameObject GameInfo;
    public GameObject PauseScreen;
    public GameObject VictoryScreen;
    public GameObject LoseScreen;
    public Text TextRespawn;

    private float startTime;
    private int playerLifes;

    // Start is called before the first frame update
    void Start()
    {
        // System
        Time.timeScale = 1;
        ObjectsPool.Clear();
        startTime = Time.time;

        // Game settings
        playerLifes = LevelData.PlayerLifes;

        // Player
        PlayerRespawn(false);
        player.GetComponent<PlayerController>().Joystick = Joystick;
        player.GetComponent<Breakable>()?.OnDestroy.AddListener(PlayerDestroyed);

        // Start game
        StartCoroutine(MakeWave());
        UpdateGameInfo();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameInfo();
    }

    void UpdateGameInfo()
    {
        // Lifes text
        TextLifes.text = $"x{playerLifes}";

        // Victory text
        if (LevelData.VictoryCondition == LevelData.Conditions.Survive)
        {
            var time = Time.time - startTime;
            TextVictoryInfo.text = $"Survive: {(int)time} / {LevelData.SurviveTime} sec";
            if (time > LevelData.SurviveTime) Won();
        }
        else
        {
            TextVictoryInfo.text = $"Destroy: {Score} / {LevelData.MeteorsCount} sec";
            if (Score >= LevelData.MeteorsCount) Won();
        }
    }

    public void Won()
    {
        GameOver();
        VictoryScreen.SetActive(true);

        var message = LevelData.VictoryCondition == LevelData.Conditions.Survive
                ? $"You survive {(int)(Time.time - startTime)} / {LevelData.SurviveTime} sec"
                : $"You destroyed {Score} / {LevelData.MeteorsCount} meteors";

        GameObject.Find("TextVictory").GetComponent<Text>().text = "You won!\n" + message;

        PlayerPrefs.SetInt(LevelData.LevelName, 2); // set current level passed
        var nextLevelIndex = System.Array.IndexOf(LevelList.Levels, LevelData) + 1;
        if (nextLevelIndex < LevelList.Levels.Length)
        {
            var nextLevelName = LevelList.Levels[nextLevelIndex].LevelName;
            PlayerPrefs.SetInt(nextLevelName, Mathf.Max(1,PlayerPrefs.GetInt(nextLevelName))); // open next level
        }
        else
        {
            GameObject.Find("BtnNextLevel").GetComponent<Button>().interactable = false;
        }
    }
    public void Lose()
    {
        var message = LevelData.VictoryCondition == LevelData.Conditions.Survive
                        ? $"You survive {(int)(Time.time - startTime)} / {LevelData.SurviveTime} sec"
                        : $"You destroyed {Score} / {LevelData.MeteorsCount} meteors";

        StartCoroutine(ShowLoseScreen(message, 2f));
    }
    private IEnumerator ShowLoseScreen(string message = "", float delay = 1)
    {
        yield return new WaitForSeconds(delay);

        GameOver();
        LoseScreen.SetActive(true);

        GameObject.Find("TextLose").GetComponent<Text>().text = "You lose :(\n" + message;
    }
    public void GameOver()
    {
        IsGameOver = false;
        Time.timeScale = 0;
        GameInfo.SetActive(false);
    }

    private float respawnTime = 3f;
    private IEnumerator StartRespawn()
    {
        TextRespawn.gameObject.SetActive(true);
        for (int i = (int)respawnTime; i > 0; i--)
        {
            TextRespawn.text = $"Respawn... {i} sec";
            yield return new WaitForSeconds(1f);
        }
        TextRespawn.gameObject.SetActive(false);

        PlayerRespawn();
    }
    public void PlayerRespawn(bool activateShield = true)
    {
        player = ObjectsPool.Create(PlayerPrefab);
        player.transform.position = new Vector3(0, -3, 0);
        if (activateShield) player.GetComponent<Shied>()?.Activate();
    }

    private void GotScore() => Score++;
    private void PlayerDestroyed()
    {
        if (playerLifes > 0)
        {
            playerLifes--;
            StartCoroutine(StartRespawn());
        }
        else Lose();
    }

    private IEnumerator MakeWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(LevelData.WaveStartDelay);

            for (int i = 0; i < LevelData.HazardsCount; i++)
            {
                var hazard = ObjectsPool.Create(LevelData.HazardsTypes[Random.Range(0, LevelData.HazardsTypes.Length)]);
                var xpos = Random.Range(LevelData.SpawnBounds.xMin, LevelData.SpawnBounds.xMax);
                var ypos = Random.Range(LevelData.SpawnBounds.yMin, LevelData.SpawnBounds.yMax);
                hazard.transform.position = new Vector3(xpos, ypos, 0);

                hazard.GetComponent<Breakable>()?.OnDestroy.AddListener(GotScore);

                yield return new WaitForSeconds(LevelData.HazardsDelay);
            }

            if (IsGameOver) break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPause = true;

        GameInfo.SetActive(false);
        PauseScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsPause = true;

        GameInfo.SetActive(true);
        PauseScreen.SetActive(false);
    }
    public void NextLevel()
    {
        var nextLevelName = LevelList.Levels[System.Array.IndexOf(LevelList.Levels, LevelData) + 1].LevelName;
        Debug.Log(nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ShowMainMenu() => SceneManager.LoadScene("MainMenu");
}
