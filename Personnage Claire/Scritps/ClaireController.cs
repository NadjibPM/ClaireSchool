using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaireController : MonoBehaviour {

    Animator claireAnimator;
    AudioSource claireAudioSource;
    CapsuleCollider claireCapsule;

    float axisH, axisV;

    [SerializeField]
    float walkSpeed = 2f, runSpeed = 8f, rotSpeed = 100f, jumpForce = 350;

    Rigidbody rb;

    const float timeout = 60.0f;
    [SerializeField] float countdown = timeout;

   
    [SerializeField] AudioClip sndJump, sndImpact, sndLeftFoot, sndRightFoot, sndDead;
    bool switchFoot = false;

    [SerializeField] bool isJumping = false;

    private void Awake()
    {
        claireAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        claireAudioSource = GetComponent<AudioSource>();
        claireCapsule = GetComponent<CapsuleCollider>();
    }

    void Update () {

        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");

        if(axisV>0)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(Vector3.forward * runSpeed * axisV * Time.deltaTime);
                claireAnimator.SetFloat("run", axisV);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * axisV * Time.deltaTime);
                claireAnimator.SetBool("walk", true);
                claireAnimator.SetFloat("run", 0);
            }            
        }
        else
        {
            claireAnimator.SetBool("walk", false);
        }

        if (axisH != 0 && axisV == 0)
        {
            claireAnimator.SetFloat("h", axisH);
        }
        else
        {
            claireAnimator.SetFloat("h", 0);
        }


        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * axisH);

        if(axisV < 0)
        {            
            claireAnimator.SetBool("walkBack", true);
            claireAnimator.SetFloat("run", 0);
            transform.Translate(Vector3.forward * walkSpeed * axisV * Time.deltaTime);
        }
        else
        {
            claireAnimator.SetBool("walkBack", false);            

        }

        //Idle Dance Twerk

        if(axisH==0 && axisV==0)
        {
            countdown -= Time.deltaTime;

            if(countdown<=0)
            {
                claireAnimator.SetBool("dance", true);
                transform.Find("AudioDance").GetComponent<AudioSource>().enabled = true;
            }
        }
        else
        {
            countdown = timeout;
            claireAnimator.SetBool("dance", false);
            transform.Find("AudioDance").GetComponent<AudioSource>().enabled = false;
        }

       

        //curve de saut
        if(isJumping)
        claireCapsule.height = claireAnimator.GetFloat("colheight");
              
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce);
            claireAudioSource.pitch = 1f;
            claireAnimator.SetTrigger("jump");
            claireAudioSource.PlayOneShot(sndJump);
        }
    }

    public void ClaireDead()
    {
        claireAudioSource.pitch = 1f;
        claireAnimator.SetTrigger("dead");
        GetComponent<ClaireController>().enabled = false;

        
        claireAudioSource.PlayOneShot(sndDead);

    }

    public void PlaySoundImpact()
    {
        claireAudioSource.pitch = 1f;
        claireAudioSource.PlayOneShot(sndImpact);
    }

    public void PlayFootStep()
    {
        if(!claireAudioSource.isPlaying)
        {
            switchFoot = !switchFoot;

            if(switchFoot)
            {
                claireAudioSource.pitch = 2f;
                claireAudioSource.PlayOneShot(sndLeftFoot);
            }
            else
            {
                claireAudioSource.pitch = 2f;
                claireAudioSource.PlayOneShot(sndRightFoot);
            }
        }
    }

    public void SwitchIsJumping()
    {
        isJumping = false;
    }
}
