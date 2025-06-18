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

    // �X�^�[�g�{�^��
    public void OnClickStart()
    {
        WindowTransition(SelectWindow);
    }

    // ��Փx�I���{�^��
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

    // ���ǂ�{�^��
    public void OnClickBack()
    {
        WindowTransition(HomeWindow);
    }

    // UI�E�B���h�E�؂�ւ�
    void WindowTransition(GameObject nextWindow)
    {
        if (currentWindow == nextWindow) return;

        currentWindow?.SetActive(false);
        currentWindow = nextWindow;
        currentWindow?.SetActive(true);
    }

    // �e�L�X�g�\���؂�ւ�
    public void TextSwicther(bool isActive)
    {
        startText?.SetActive(isActive);
    }
}
