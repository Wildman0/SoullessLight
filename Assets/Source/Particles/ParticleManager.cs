using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    [Header("Particle Anchors")]
    public GameObject character;
    public GameObject boss;
    public GameObject bossSword;

    [Header("Character Particles")]
    public GameObject[] playerParticles;

    [Header("Boss Particles")]
    public Animator bossAnim;
    public GameObject[] bossParticles;

    private AnimatorClipInfo[] animInfo;
    private string animClipName;

    public bool dontDuplicate;

	// Use this for initialization
	void Start ()
    {
        dontDuplicate = false; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetBossAnimInfo();
        Explosion();
	}

    public void GetBossAnimInfo()
    {
        animInfo = bossAnim.GetCurrentAnimatorClipInfo(0);
        animClipName = animInfo[0].clip.name;
    }

    public void HealParticleSystem()
    {
        GameObject NewObject;
        NewObject = Instantiate(playerParticles[0], character.transform.position, transform.rotation);
        NewObject.transform.parent = character.transform;

        Destroy(NewObject, 3);
    }

    public void Explosion()
    {
        if (animClipName == "A_Explosion" && dontDuplicate == false)
        {
            GameObject NewObject;
            NewObject = Instantiate(bossParticles[0], boss.transform.position, transform.rotation);
            NewObject.transform.parent = boss.transform;

            StartCoroutine(Wait());
            Destroy(NewObject, 6);
            

            Debug.Log(animClipName + dontDuplicate);          
        }

    }
    
    IEnumerator Wait()
    {
        dontDuplicate = true;
        yield return new WaitForSeconds(6);
        dontDuplicate = false;
    }

    
}
