using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public LevelList LevelList;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadLevel(int index)
    {
        try
        {
            SceneManager.LoadScene(LevelList.Levels[index].LevelName);
        }
        catch
        {
            Debug.Log($"Level not found, index = {index}");
        }
    }
}
