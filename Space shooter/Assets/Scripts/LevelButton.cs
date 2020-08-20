using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelData LevelData;
    public bool IsLock = true;

    public GameObject Star;
    public GameObject Lock;

    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();

        // 0 - lock, 1 - open, 2 - passed
        var levelInfo = PlayerPrefs.GetInt(LevelData.LevelName);
        if (!IsLock) levelInfo = Mathf.Max(1, levelInfo);
        switch(levelInfo)
        {
            // lock
            case 0:
                Lock.SetActive(true);
                btn.interactable = false;
                break;
            // passed
            case 2:
                Star.SetActive(true);
                break;
        }
    }
}
