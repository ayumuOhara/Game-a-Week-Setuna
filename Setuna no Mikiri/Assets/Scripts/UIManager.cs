using TMPro;
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

    void Start()
    {
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
        ReadyTextObj.SetActive(true);
        SelectWindow.SetActive(false);
        PlayWindow.SetActive(false);
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

    // �����e�L�X�g�t�F�[�h�A�E�g
    public void ReadyTextFade()
    {
        Animator animator = ReadyTextObj.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
    }

    // ���g���C�e�L�X�g
    public void RetryTextFade()
    {
        RetryTextObj.SetActive(true);

        Animator animator = RetryTextObj.GetComponent<Animator>();
        animator.SetTrigger("Fade");
    }

    // �N���b�N�^�C�~���O�\��
    public void ActiveClickIcon(bool isActive)
    {
        clickIcon.SetActive(isActive);
    }

    // �������e�L�X�g
    public void WinText()
    {
        WinTextObj.SetActive(true);
    }

    // �s�k���e�L�X�g
    public void LoseText()
    {
        LoseTextObj.SetActive(true);
    }

    // ���Ă����e�L�X�g
    public void FastText()
    {
        FastTextObj.SetActive(true);
    }
}
