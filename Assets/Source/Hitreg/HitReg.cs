using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HitReg : MonoBehaviour
{
    public enum PlayerAttackTypes
    {
        LightAttack,
        HeavyAttack
    }

    private PlayerAttackTypes lastPlayerAttackType;
    
    [SerializeField] private bool debug;

    [SerializeField] private string[] tag;

    [SerializeField] private GameObject[] hitRegNodes;

    private Vector3[] hitRegNodeOldPositions;
    private Vector3[] hitRegNodeCurrentPositions;

    //Isaac's shitty audio implement, fix later
    public AudioClip MusicClip;
    public AudioSource MusicSource;

    private RaycastHit hit;

    //Attack handlers
    private PlayerAttack playerAttack;
    private PlayerController playerController;

    private bool isActive;
    private bool hasHit;

    public float hitRegStartDelay = 0.2f;
    public float hitRegActiveTime = 0.6f;

    int tagIndex;

    //Runs on instantiation
    void Start()
    {
        InstantiatePositionArray();
        SetAttack();
        hasHit = true;
        MusicSource.clip = MusicClip;
    }
    
    //Runs at start
    void Update()
    {
        hitRegNodeCurrentPositions = GetCurrentHitRegNodePositions();
        DrawLines();
        SetOldHitRegNodePositions();
    }

    //Sets the relevant attack method
    void SetAttack()
    {
        if (System.Array.IndexOf(tag, "Boss") != 1) //tag == "Boss"
            playerAttack = gameObject.GetComponent<PlayerAttack>();
        else
            playerController = gameObject.GetComponent<BossCollider>().playerController;
    }

    //Toggles whether or nor hitreg is active
    public void ToggleHitreg()
    {
        StartCoroutine(ToggleHitRegCoroutine());
    }
    
    public void ToggleHitreg(PlayerAttackTypes attack)
    {
        lastPlayerAttackType = attack;
        StartCoroutine(ToggleHitRegCoroutine());
    }

    private IEnumerator ToggleHitRegCoroutine()
    {
        yield return new WaitForSeconds(hitRegStartDelay);
        isActive = true;
        //Debug.Log("Active");
        {
            yield return new WaitForSeconds(hitRegActiveTime);
            isActive = false;
            hasHit = false;
            //Debug.Log("Inactive");
        }
    }

    //Called when the gameObject the hitreg is looking for is first hit
    private void Hit()
    {
        if (!hasHit)
        {
            if (tag[tagIndex] == "Boss")
            {
                switch (lastPlayerAttackType)
                {
                    case PlayerAttackTypes.LightAttack:
                        playerAttack.DamageBoss(playerAttack.lightAttackDamage);
                        break;

                    case PlayerAttackTypes.HeavyAttack:
                        playerAttack.DamageBoss(playerAttack.heavyAttackDamage);
                        break;
                }

                hasHit = true;
                MusicSource.Play();
                UI.instance.bossAttacked.GetComponent<Image>().enabled = true;
                UI.instance.bossAttacked.GetComponent<Animator>().SetTrigger("BossContact");
                StartCoroutine(HitBoss());
            }

            if (tag[tagIndex] == "Player")
            {
                if (!PlayerHealth.instance.isInvincible)
                {
                    hasHit = true;
                    UI.instance.playerAttacked.GetComponent<Image>().enabled = true;
                    UI.instance.playerAttacked.GetComponent<Animator>().SetTrigger("IsFlinched");
                    StartCoroutine(HitBoss());

                    PlayerHealth.instance.TakeDamage(0.3f);
                }
            }

            if (tag[tagIndex] == "Orb")
            {
                OrbSetUp.orbHealth[OrbSetUp.healthIndex] -= .2f;
                hasHit = true;
            }
        }
    }

    //HitStop (Isaac)
    private IEnumerator HitBoss() 
    {
        Time.timeScale = 0f;
        {
            yield return new WaitForSecondsRealtime(0.05f);
            Time.timeScale = 1f;
            Debug.Log("HitStop");
        }
    }

    //Instantiates the array of hit nodes
    void InstantiatePositionArray()
    {
        hitRegNodeOldPositions = new Vector3[hitRegNodes.Length];
        hitRegNodeCurrentPositions = new Vector3[hitRegNodes.Length];
    }

    //Returns the current positions of all nodes as an array
    Vector3[] GetCurrentHitRegNodePositions()
    {
        List<Vector3> list = new List<Vector3>();

        for (int i = 0; i < hitRegNodeOldPositions.Length; i++)
        {
            list.Add(hitRegNodes[i].transform.position);
        }

        return list.ToArray();
    }

    //Run at the end, sets the positions for the last frame to be used next frame
    void SetOldHitRegNodePositions()
    {
        for (int i = 0; i < hitRegNodeOldPositions.Length; i++)
        {
            hitRegNodeOldPositions[i] = hitRegNodes[i].transform.position;
        }
    }

    //Draws the lines for hitreg
    void DrawLines()
    {
        for (int i = 0; i < hitRegNodes.Length; i++)
        {
            if (Physics.Linecast(hitRegNodeOldPositions[i], hitRegNodeCurrentPositions[i], out hit))
            {
                //if (hit.transform.tag == tag) // remove length if it fucks up // create for loop to check string in the array
                //{
                //    Hit();

                //    if (debug)
                //        Debug.DrawLine(hitRegNodeOldPositions[i],
                //            hitRegNodeCurrentPositions[i],
                //            Color.red,
                //            0.5f);
                //}

                for (tagIndex = 0; tagIndex < tag.Length; tagIndex++)
                {
                    if(hit.transform.tag == tag[tagIndex])
                    {
                        Hit();

                        if (debug)
                            Debug.DrawLine(hitRegNodeOldPositions[i],
                                hitRegNodeCurrentPositions[i],
                                Color.red,
                                0.5f);

                        Debug.Log(tagIndex);
                    }
                }
            }
            else
            {
                if (debug)
                    Debug.DrawLine(hitRegNodeOldPositions[i],
                        hitRegNodeCurrentPositions[i],
                        Color.green,
                        0.5f);
            }
        }
    }
}