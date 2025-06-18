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

    // �Q�[���X�^�[�g
    IEnumerator Timing()
    {
        SetEnemy();
        GameObject enemyObj = Instantiate(enemy);
        animatorEnemy = enemyObj.GetComponent<Animator>();

        while (time < 3.0f)
        {
            Debug.Log($"�J�n�܂Ły{3 - time}�z");
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
        }

        time = 0;

        float signalTime = -1f; // �������i�܂��u�I�v�͏o�Ă��Ȃ��j

        while (true)
        {
            time += Time.deltaTime;

            // �u�I�v���o��^�C�~���O
            if (time >= rndTime && signalTime < 0)
            {
                signalTime = Time.time; // ����̂݋L�^
                AttackEnemy();
                Debug.Log("�N���b�N�I�I");
            }

            if (signalTime > 0)
            {
                // ���}���o����
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackPlayer(Time.time - signalTime);
                    yield break;
                }
            }
            else
            {
                // ���}�O�ɉ������炨�Ă�
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("���Ă��I");
                    yield break;
                }
            }

            yield return null;
        }
    }

    // ���s����
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

    // �v���C���[�̍U��
    void AttackPlayer(float errorTime)
    {
        time = 0;
        AnimationTrigger.AttackAnim(animatorPlayer);

        // errorTime�̏����_2�ȉ���؂�̂Ă鏈��
        errorTime = Mathf.Floor(errorTime * 100);
        errorTime *= 0.01f;
        Debug.Log($"�덷��{errorTime}�b�ł�");

        WinnerChecker(errorTime);
    }

    // �G�l�~�[�̍U��
    void AttackEnemy()
    {
        AnimationTrigger.AttackAnim(animatorEnemy);
    }

    // �G�̏�����
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
