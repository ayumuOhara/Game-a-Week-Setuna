using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip ButtonSE;
    [SerializeField] AudioClip StartButtonSE;
    [SerializeField] AudioClip SelectSE;
    [SerializeField] AudioClip StartSE;
    [SerializeField] AudioClip WindSE;
    [SerializeField] AudioClip AttackSE;
    [SerializeField] AudioClip EnemyAttackSE;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // �w�肳�ꂽ���ʉ���炷
    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "ButtonSE": audioSource.PlayOneShot(ButtonSE); break;
            case "StartButtonSE" : audioSource.PlayOneShot(StartButtonSE); break;
            case "SelectSE": audioSource.PlayOneShot(SelectSE); break;
            case "StartSE": audioSource.PlayOneShot(StartSE); break;
            case "WindSE": audioSource.PlayOneShot(WindSE); break;
            case "AttackSE": audioSource.PlayOneShot(AttackSE); break;
            case "EnemyAttackSE": audioSource.PlayOneShot(EnemyAttackSE); break;
        }
    }
}
