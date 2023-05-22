using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GiantBoss : MonoBehaviour
{
    public float health;

    //Intro
    private bool isPlayingIntro;
    private bool isZoomingOut;
    private bool hasPlayedIntro;
    private bool isPlayingAnimation;

    public GameObject stinkerPrefab;
    public Transform stinkerSpawnPoint;

    public GameObject NormalGiant;
    public GameObject ThrowingGiant;
    public GameObject bossCrate;

    private bool isAttacking;

    private CinemachineVirtualCamera vc;

    [Header("Audio")]
    public AudioSource audioSource;
    [Space(10)]

    public AudioClip IntroClip;
    public AudioClip SmellyFeetClip;

    public Slider healthSlider;
    GameObject healthSliderCanvas;

    GameObject Foot;


    void Awake()
    {
        vc = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>().enabled = false;
        Foot = GameObject.Find("Foot");
        healthSliderCanvas = GameObject.Find("HealthSliderCanvas");
        healthSliderCanvas.SetActive(false);

        hasPlayedIntro = false;
        isPlayingAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = GetComponent<Enemy>().health;
        if (Input.GetKeyDown(KeyCode.H))
        {
            BossStink();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            BossStomp();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            spawnAllies();
        }

        if (isPlayingIntro && GameObject.Find("Player").GetComponent<PlayerMovement>().isGrounded)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().Freeze(true);
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerAnimationController>().enabled = false;
            if (!isPlayingAnimation)
            {
                isPlayingAnimation = true;
                GameObject.Find("Player").GetComponent<Animator>().Play("PlayerLookingAtBoss");
            }
        }
        if (isZoomingOut)
        {
            vc.m_Lens.OrthographicSize = Mathf.Lerp(vc.m_Lens.OrthographicSize, 8, 1 * Time.deltaTime);
        }

        if(hasPlayedIntro && !isAttacking)
        {
            StartCoroutine(randomAttack());
        }
    }

    public void randomPosition()
    {
        transform.position = new Vector3(Random.Range(GameObject.Find("BossAreaPoint1").transform.position.x, GameObject.Find("BossAreaPoint2").transform.position.x), 0, 0);
    }

    public void BossStomp()
    {
        GetComponent<Animator>().Play("BossStomp");
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }
    public void spawnAllies()
    {
        if (Random.Range(0, 2) == 1 )
        {
            GameObject Enemy = Instantiate(NormalGiant, new Vector3(Random.Range(GameObject.Find("BossAreaPoint1").transform.position.x, GameObject.Find("BossAreaPoint2").transform.position.x), 50, 0), Quaternion.identity);
            Enemy.transform.parent = GameObject.Find("Enemies").transform;

        }
        else
        {
            GameObject Enemy = Instantiate(ThrowingGiant, new Vector3(Random.Range(GameObject.Find("BossAreaPoint1").transform.position.x, GameObject.Find("BossAreaPoint2").transform.position.x), 50, 0), Quaternion.identity);
            Enemy.transform.parent = GameObject.Find("Enemies").transform;
        }
    }

    public void spawnCrates()
    {
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            GameObject Crate = Instantiate(bossCrate, new Vector3(Random.Range(GameObject.Find("BossAreaPoint1").transform.position.x, GameObject.Find("BossAreaPoint2").transform.position.x), 50, 0), Quaternion.identity);
            Crate.transform.parent = GameObject.Find("Enemies").transform;
        }
    }

    IEnumerator randomAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        int randomNumber = Random.Range(0, 4);
        if (randomNumber == 1)
        {
            BossStink();
        } else if(randomNumber == 2)
        {
            BossStomp();
        } else if(randomNumber == 3)
        {
            spawnAllies();
        } else
        {
            spawnCrates();
        }
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        isAttacking = false;
    }

    #region smellyFeetAttack
    public void BossStink()
    {
        GetComponent<Animator>().Play("SmellyFootAttack");
        audioSource.clip = SmellyFeetClip;
        audioSource.Play();
    }

    public void SpawnStinkerLeft()
    {
        GameObject Stinker = Instantiate(stinkerPrefab, stinkerSpawnPoint);
        Stinker.GetComponent<Stinker>().isFacingLeft = true;
    }
    public void SpawnStinkerRight()
    {
        GameObject Stinker = Instantiate(stinkerPrefab, stinkerSpawnPoint);
        Stinker.GetComponent<Stinker>().isFacingLeft = false;
    }

    #endregion

    #region intro
    public void PlayerDetected()
    {
        audioSource.clip = IntroClip;
        audioSource.Play();
        GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>().enabled = true;
        isZoomingOut = true;
        StartCoroutine(bossIntro());
    }
    IEnumerator bossIntro()
    {
        healthSliderCanvas.SetActive(true);

        yield return new WaitForSeconds(.15f);
        isPlayingIntro = true;

        yield return new WaitForSeconds(3);
        isPlayingIntro = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().Freeze(false);
        GameObject.Find("Player").GetComponent<PlayerAnimationController>().enabled = true;
        hasPlayedIntro = true;
    }
    #endregion
}
