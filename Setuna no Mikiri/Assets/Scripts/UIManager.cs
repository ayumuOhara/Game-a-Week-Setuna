using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startText;

    [SerializeField] GameObject HomeWindow;
    [SerializeField] GameObject SelectWindow;
    [SerializeField] GameObject PlayWindow;

    GameObject currentWindow;

    void Start()
    {
        currentWindow = HomeWindow;
        currentWindow?.SetActive(true);
    }

    // スタートボタン
    public void OnClickStart()
    {
        WindowTransition(SelectWindow);
    }

    // 難易度選択ボタン
    public void OnClickSelect(int num)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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

    // テキスト表示切り替え
    public void TextSwicther(bool isActive)
    {
        startText?.SetActive(isActive);
    }
}
