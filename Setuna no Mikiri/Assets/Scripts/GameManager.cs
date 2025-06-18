using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    Animator animatorPlayer;
    Animator animatorEnemy;

    enum STAGE
    {
        EASY,
        NORMAL,
        HARD,
    }

    [SerializeField] STAGE stage = STAGE.EASY;

    float rndTime = 0;

    float time = 0;
    float enemyReactionTime = 0;

    const float WAIT_TIME_MIN = 1.2f;
    const float WAIT_TIME_MAX = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        animatorPlayer = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Timing());
            rndTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
        }
    }

    // ゲームスタート
    IEnumerator Timing()
    {
        SetEnemy();
        GameObject enemyObj = Instantiate(enemy);
        animatorEnemy = enemyObj.GetComponent<Animator>();

        while (time < 3.0f)
        {
            Debug.Log($"開始まで【{3 - time}】");
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
        }

        time = 0;

        float signalTime = -1f; // 初期化（まだ「！」は出ていない）

        while (true)
        {
            time += Time.deltaTime;

            // 「！」が出るタイミング
            if (time >= rndTime && signalTime < 0)
            {
                signalTime = Time.time; // 初回のみ記録
                AttackEnemy();
                Debug.Log("クリック！！");
            }

            if (signalTime > 0)
            {
                // 合図が出た後
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackPlayer(Time.time - signalTime);
                    yield break;
                }
            }
            else
            {
                // 合図前に押したらおてつき
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("おてつき！");
                    yield break;
                }
            }

            yield return null;
        }
    }

    // 勝敗判定
    void WinnerChecker(float errorTime)
    {
        if(errorTime <= enemyReactionTime)
        {
            AnimationTrigger.WinAnim(animatorPlayer);
            AnimationTrigger.DeadAnim(animatorEnemy);
        }
        else
        {
            AnimationTrigger.WinAnim(animatorEnemy);
            AnimationTrigger.DeadAnim(animatorPlayer);
        }
    }

    // プレイヤーの攻撃
    void AttackPlayer(float errorTime)
    {
        time = 0;
        AnimationTrigger.AttackAnim(animatorPlayer);

        // errorTimeの小数点2以下を切り捨てる処理
        errorTime = Mathf.Floor(errorTime * 100);
        errorTime *= 0.01f;
        Debug.Log($"誤差は{errorTime}秒です");

        WinnerChecker(errorTime);
    }

    // エネミーの攻撃
    void AttackEnemy()
    {
        AnimationTrigger.AttackAnim(animatorEnemy);
    }

    // 敵の初期化
    void SetEnemy()
    {
        switch (stage)
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
        }
    }
}
