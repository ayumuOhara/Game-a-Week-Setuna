using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject clickIcon;

    [SerializeField] GameObject WinTextObj;
    [SerializeField] GameObject LoseTextObj;
    [SerializeField] GameObject FastTextObj;
    [SerializeField] GameObject ReadyTextObj;
    [SerializeField] GameObject RetryTextObj;

    [SerializeField] GameObject HomeWindow;
    [SerializeField] GameObject SelectWindow;
    [SerializeField] GameObject PlayWindow;

    GameObject currentWindow;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        currentWindow = HomeWindow;
        currentWindow?.SetActive(true);

        Initialize();
    }

    void Initialize()
    {
        clickIcon.SetActive(false);
        WinTextObj.SetActive(false);
        LoseTextObj.SetActive(false);
        FastTextObj.SetActive(false);
        ReadyTextObj.SetActive(false);
        SelectWindow.SetActive(false);
        PlayWindow.SetActive(false);
    }

    // スタートボタン
    public void OnClickStart()
    {
        audioManager.PlaySound("StartButtonSE");
        WindowTransition(SelectWindow);
    }

    // 難易度選択ボタン
    public void OnClickSelect(int num)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager.PlaySound("SelectSE");

        switch (num)
        {
            case 0:
                gameManager.SetLevel(GameManager.STAGE.EASY);
                break;
            case 1:
                gameManager.SetLevel(GameManager.STAGE.NORMAL);
                break;
            case 2:
                gameManager.SetLevel(GameManager.STAGE.HARD);
            break;
            case 3:
                gameManager.SetLevel(GameManager.STAGE.VERY_HARD);
                break;
        }

        WindowTransition(PlayWindow);
    }

    // もどるボタン
    public void OnClickBack()
    {
        audioManager.PlaySound("ButtonSE");
        WindowTransition(HomeWindow);
    }

    // UIウィンドウ切り替え
    void WindowTransition(GameObject nextWindow)
    {
        if (currentWindow == nextWindow) return;

        currentWindow?.SetActive(false);
        currentWindow = nextWindow;
        currentWindow?.SetActive(true);
    }

    // 準備テキスト
    public void ActiveReadyText()
    {
        ReadyTextObj.SetActive(true);
    }

    // 準備テキストフェードアウト
    public void ReadyTextFade()
    {
        Animator animator = ReadyTextObj.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
    }

    // リトライテキスト
    public void RetryTextFade()
    {
        RetryTextObj.SetActive(true);

        Animator animator = RetryTextObj.GetComponent<Animator>();
        animator.SetTrigger("Fade");
    }

    // クリックタイミング表示
    public void ActiveClickIcon(bool isActive)
    {
        clickIcon.SetActive(isActive);
    }

    // 勝利時テキスト
    public void WinText()
    {
        WinTextObj.SetActive(true);
    }

    // 敗北時テキスト
    public void LoseText()
    {
        LoseTextObj.SetActive(true);
    }

    // おてつき時テキスト
    public void FastText()
    {
        FastTextObj.SetActive(true);
    }
}
