using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public Lang language = Lang.Eng;
    public bool ending;
    [SerializeField] private Button playBtn;

    private void Start()
    {
        if(GameStats.Instance == null)
        {
            GameStats.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeLang(int number)
    {
        if(number == 0)
        {
            GameStats.Instance.language = Lang.Eng;
            if(playBtn != null)
            {
                playBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
            }
        }
        else if (number == 1)
        {
            GameStats.Instance.language = Lang.Rus;
            if (playBtn != null)
            {
                playBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Играть";
            }
        }
    }
}

public enum Lang
{
    Rus,
    Eng
}
