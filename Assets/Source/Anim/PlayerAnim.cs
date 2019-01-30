using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim instance;
    
    public Animator anim;
    public ParticleManager particleManager;

    public AudioSource MusicSource;
    public AudioSource MusicSource1;
    public AudioSource MusicSource2;
    public AudioSource MusicSource3;
    public AudioSource MusicSource4;

    public AudioClip MusicClip;
    public AudioClip MusicClip1;
    public AudioClip MusicClip2;
    public AudioClip MusicClip3;
    public AudioClip MusicClip4;

    private GameObject go;

    void Awake()
    {
        if (!instance)
            instance = this;
        else 
            Debug.LogError("More than one instance of PlayerAnim in the scene");
        
        go = FindObjectOfType<GameManager>().gameObject;
        particleManager = go.GetComponent<ParticleManager>();
    }

    //Runs on instantiation
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Player run animation
    public void Run()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRolling", false);
        anim.SetBool("IsIdle", false);

        anim.SetBool("IsRunning", true);
    }

    //Player light attack animation
    public void LightAttack()
    {
        anim.SetBool("IsWeakAtt", true);
        MusicSource.clip = MusicClip;
        MusicSource.Play();
    }

    //Player idle animation
    public void Idle()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRolling", false);
        anim.SetBool("IsRunning", false);

        anim.SetBool("IsIdle", true);
    }

    //Player heal animation
    public void Heal()
    {
        anim.SetBool("IsHealing", true);
        particleManager.HealParticleSystem();
    }

    //Player roll animation
    public void Roll()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);


        anim.SetBool("IsRolling", true);
        if (!MusicSource2.isPlaying)
            MusicSource2.Play();
    }

    //Player roll right animation
    public void RollRight()
    {
        anim.SetBool("RollRight", true);
    }

    //Player roll left animation
    public void RollLeft()
    {
        anim.SetBool("RollLeft", true);
    }

    //Player walk animation
    public void Walk()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsRolling", false);
        anim.SetBool("IsIdle", false);

        anim.SetBool("IsWalking", true);
        if (!MusicSource1.isPlaying)
            MusicSource1.Play();
    }

    //Player death animation
    public void Death()
    {
        anim.SetTrigger("IsDefoDead");
            MusicSource4.Play();
    }

    //Player jog animation
    public void Jog()
    {
        anim.SetFloat("MoveSpeed", PlayerMovement.instance.velocity);
    }

    //Player flinch animation
    public void Flinch()
    {
        anim.SetBool("IsWeakAtt", false);
        anim.SetTrigger("IsDamaged");
        if (!MusicSource3.isPlaying)
            MusicSource3.Play();

    }

    public void Cinematic()
    {
        anim.SetTrigger("Cinematic");
    }
}