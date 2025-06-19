using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;

    UIManager uiManager;
    AudioManager audioManager;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    Animator animatorPlayer;
    Animator animatorEnemy;

    public enum STAGE
    {
        EASY,
        NORMAL,
        HARD,
        VERY_HARD,
    }

    [SerializeField] STAGE level = STAGE.EASY;

    float rndTime = 0;

    float time = 0;
    float reactionTime = 0;
    float enemyReactionTime = 0;

    const float WAIT_TIME_MIN = 2f;
    const float WAIT_TIME_MAX = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        animatorPlayer = player.GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // �Q�[���X�^�[�g
    void GameStart()
    {
        StartCoroutine(Ready());
        rndTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
    }

    // ����
    IEnumerator Ready()
    {
        yield return new WaitForSeconds(1);
        uiManager.ActiveReadyText();
        audioManager.PlaySound("WindSE");
        GameObject enemyObj = Instantiate(enemy);
        animatorEnemy = enemyObj.GetComponent<Animator>();

        while (time < 3.0f)
        {
            Debug.Log($"�J�n�܂Ły{3 - (int)time}�z");
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
        }

        uiManager.ReadyTextFade();
        StartCoroutine(Timing());
    }

    // �C���Q�[��
    IEnumerator Timing()
    {
        time = 0;
        float signalTime = -1f;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= rndTime && signalTime < 0)
            {
                uiManager.ActiveClickIcon(true);
                audioManager.PlaySound("StartSE");
                signalTime = Time.time;
                Debug.Log("�N���b�N�I�I");
            }

            if (signalTime > 0)
            {
                float currentReactionTime = Time.time - signalTime;

                // �v���C���[����������O�ɓG�̔������Ԃ𒴂����畉��
                if (currentReactionTime > enemyReactionTime)
                {
                    AttackEnemy();
                    yield break;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackPlayer(currentReactionTime);
                    yield break;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    uiManager.FastText();
                    StartCoroutine(Retry());
                    yield break;
                }
            }

            yield return null;
        }
    }

    // ���g���C�ҋ@
    IEnumerator Retry()
    {
        yield return new WaitForSeconds(1);

        uiManager.RetryTextFade();

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Main");
                yield break;
            }

            yield return null;
        }
    }

    // �v���C���[�̍U��
    void AttackPlayer(float errorTime)
    {
        uiManager.ActiveClickIcon(false);
        audioManager.PlaySound("AttackSE");

        time = 0;
        AnimationTrigger.AttackAnim(animatorPlayer);
        AnimationTrigger.WinAnim(animatorPlayer);
        AnimationTrigger.DeadAnim(animatorEnemy);

        uiManager.WinText();

        // errorTime�̏����_2�ȉ���؂�̂Ă鏈��
        errorTime = Mathf.Floor(errorTime * 100);
        errorTime *= 0.01f;
        Debug.Log($"�덷��{errorTime}�b�ł�");

        StartCoroutine(Retry());
    }

    // �G�l�~�[�̍U��
    void AttackEnemy()
    {
        uiManager.ActiveClickIcon(false);
        audioManager.PlaySound("EnemyAttackSE");

        AnimationTrigger.AttackAnim(animatorEnemy);
        AnimationTrigger.DeadAnim(animatorPlayer);
        AnimationTrigger.WinAnim(animatorEnemy);

        uiManager.LoseText();
        StartCoroutine(Retry());
    }

    // �G�̏�����
    void SetEnemy()
    {
        switch (level)
        {
            case STAGE.EASY:
                enemy = enemyStats.enemyData[0].enemyObj;
                enemyReactionTime = enemyStats.enemyData[0].reactionTime;
                break;
            case STAGE.NORMAL:
                enemy = enemyStats.enemyData[1].enemyObj;
                enemyReactionTime = enemyStats.enemyData[1].reactionTime;
                break;
            case STAGE.HARD:
                enemy = enemyStats.enemyData[2].enemyObj;
                enemyReactionTime = enemyStats.enemyData[2].reactionTime;
                break;
            case STAGE.VERY_HARD:
                enemy = enemyStats.enemyData[3].enemyObj;
                enemyReactionTime = enemyStats.enemyData[3].reactionTime;
                break;
        }
    }

    // ��Փx�ݒ�
    public void SetLevel(STAGE level)
    {
        this.level = level;
        SetEnemy();

        GameStart();
    }
}
