using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BossHealth : MonoBehaviour

    
{
    public static BossHealth instance;

    Phase phase;

    [Range(0, 1)]
    public float health = 1.0f;

    bool active;

    public UI ui;
    public GameObject BossDefeated;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    private void Update()
    {
        Death();
    }

    public void Damage(float i)
    {
        health -= i;
        //TODO: SORRY ASH, YOU'LL NEED TO GET THIS REFERENCE PROPERLY
        FindObjectOfType<RFX4_CameraShake>().PlayShake();
        phase.anim.SetTrigger("IsBossDamaged");
    }

    private void Death()
    {
        if (health <= 0 && active == false)
        {
            phase.anim.SetBool("IsDead", true);
            ui.BossDefeated.GetComponent<Image>().enabled = true;
            ui.BossDefeated.GetComponent<Animator>().SetTrigger("IsDefeated");           
            BossDefeated.SetActive(false);
            audioSource.Stop();
            audioSource1.Play();
            StartCoroutine(Conclusion());
            StartCoroutine(EndState());
           

            active = true;
        }
    }
    //New Audio After Boss Dies (Isaac)
    private IEnumerator Conclusion()
    {
        yield return new WaitForSecondsRealtime(5f);
        audioSource2.Play();
        //Debug.Log("Conclusion");
    }

    private IEnumerator EndState()
    {
        yield return new WaitForSecondsRealtime(6f);
        ui.Endstate.GetComponent<Animator>().SetTrigger("isEnd");
        //Endstate();
    }

    public void Endstate()
    {
        UI.instance.Endstate.GetComponent<Image>().enabled = true;             
    }
}
