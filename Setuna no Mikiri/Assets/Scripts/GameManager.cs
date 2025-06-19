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

    // ゲームスタート
    void GameStart()
    {
        StartCoroutine(Ready());
        rndTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
    }

    // 準備
    IEnumerator Ready()
    {
        yield return new WaitForSeconds(1);
        uiManager.ActiveReadyText();
        audioManager.PlaySound("WindSE");
        GameObject enemyObj = Instantiate(enemy);
        animatorEnemy = enemyObj.GetComponent<Animator>();

        while (time < 3.0f)
        {
            Debug.Log($"開始まで【{3 - (int)time}】");
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
        }

        uiManager.ReadyTextFade();
        StartCoroutine(Timing());
    }

    // インゲーム
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
                Debug.Log("クリック！！");
            }

            if (signalTime > 0)
            {
                float currentReactionTime = Time.time - signalTime;

                // プレイヤーが反応する前に敵の反応時間を超えたら負け
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

    // リトライ待機
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

    // プレイヤーの攻撃
    void AttackPlayer(float errorTime)
    {
        uiManager.ActiveClickIcon(false);
        audioManager.PlaySound("AttackSE");

        time = 0;
        AnimationTrigger.AttackAnim(animatorPlayer);
        AnimationTrigger.WinAnim(animatorPlayer);
        AnimationTrigger.DeadAnim(animatorEnemy);

        uiManager.WinText();

        // errorTimeの小数点2以下を切り捨てる処理
        errorTime = Mathf.Floor(errorTime * 100);
        errorTime *= 0.01f;
        Debug.Log($"誤差は{errorTime}秒です");

        StartCoroutine(Retry());
    }

    // エネミーの攻撃
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

    // 敵の初期化
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

    // 難易度設定
    public void SetLevel(STAGE level)
    {
        this.level = level;
        SetEnemy();

        GameStart();
    }
}
