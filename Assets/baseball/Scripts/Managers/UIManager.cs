using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance 
    { 
        get => FindObjectOfType<UIManager>(); 
    }

    private int score;
    [SerializeField] Text scoreText;

    private GameObject _gameRef;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject top;
    [SerializeField] GameObject game;
    [SerializeField] GameObject pause;

    private void Awake()
    {
        Bucket.OnCollisionEnter += () =>
        {
            scoreText.text = $"{++score}";

            if(SettingsManager.VibraEnbled)
            {
                Handheld.Vibrate();
            }
        };
    }


    private void Start()
    {
        OpenMenu();
    }

    public void StartGame()
    {
        if (_gameRef)
        {
            Destroy(_gameRef);
        }

        Time.timeScale = 1;

        score = 0;
        scoreText.text = $"{score}";

        var _parent = GameObject.Find("Environment").transform;
        var _prefab = Resources.Load<GameObject>("level");

        _gameRef = Instantiate(_prefab, _parent);
        Bucket.UpdatePositiion();

        pause.SetActive(false);
        menu.SetActive(false);

        game.SetActive(true);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenTop()
    {
        top.SetActive(true);
        menu.SetActive(false);
    }

    public void Pause(bool IsPause)
    {
        pause.SetActive(IsPause);
        Time.timeScale = IsPause ? 0 : 1;
    }

    public void OpenMenu()
    {
        if (_gameRef)
        {
            Destroy(_gameRef);
        }

        game.SetActive(false);
        settings.SetActive(false);
        top.SetActive(false);
        pause.SetActive(false);

        menu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
