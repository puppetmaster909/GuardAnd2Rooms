using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject tempHUD;
    private float HUDTimer = 0.0f;
    private bool HUDBeenUp = true;

    [System.Serializable]
    public struct Screen
    {
        public string name;
        public GameObject screen;
        public Button firstButton;
    }

    public List<Screen> screens = new List<Screen>();
    public int curScreen;

    public void ShowScreen(string name)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CameraController.instance.menuUp = true;
        Time.timeScale = 0;
        BGMController.instance.BGM.Pause();

        for (int i = 0; i < screens.Count; i++)
        {
            if (screens[i].name.Equals(name))
            {
                screens[curScreen].screen.SetActive(false);
                screens[i].screen.SetActive(true);
                screens[i].firstButton.Select();
                curScreen = i;
            }
        }
    }

    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
        } else
        {
            GameObject.Destroy(gameObject);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        screens[curScreen].screen.SetActive(false);
        BGMController.instance.BGM.Play();
        CameraController.instance.menuUp = false;
        HUDBeenUp = false;
    }

    public void Back()
    {
        ShowScreen("PauseMenu");
    }

    public void Controls()
    {
        ShowScreen("ControlsScreen");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!HUDBeenUp)
        {
            HUDTimer += Time.deltaTime;
            if (HUDTimer >= 5.0f)
            {
                HUDTimer = 0.0f;
                tempHUD.SetActive(false);
                HUDBeenUp = true;
            }
        }
    }
}
