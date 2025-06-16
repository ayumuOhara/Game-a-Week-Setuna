using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    Animator animator;

    float rndTime = 0;

    float time = 0;

    const float WAIT_TIME_MIN = 1.2f;
    const float WAIT_TIME_MAX = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        animator = player.GetComponent<Animator>();
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
                Debug.Log("クリック！！");
            }

            if (signalTime > 0)
            {
                // 合図が出た後
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Attack(Time.time - signalTime);
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


    // プレイヤーの攻撃
    void Attack(float errorTime)
    {
        time = 0;
        AnimationTrigger.AttackAnim(animator);
        Debug.Log($"誤差は{errorTime}秒です");
    }
}
