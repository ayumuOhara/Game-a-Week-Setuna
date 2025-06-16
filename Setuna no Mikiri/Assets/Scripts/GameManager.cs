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

    // �Q�[���X�^�[�g
    IEnumerator Timing()
    {
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
                Debug.Log("�N���b�N�I�I");
            }

            if (signalTime > 0)
            {
                // ���}���o����
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Attack(Time.time - signalTime);
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


    // �v���C���[�̍U��
    void Attack(float errorTime)
    {
        time = 0;
        AnimationTrigger.AttackAnim(animator);
        Debug.Log($"�덷��{errorTime}�b�ł�");
    }
}
