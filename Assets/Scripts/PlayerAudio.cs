using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip splashSound;
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambCaveSnapshot;
    public AudioMixerSnapshot ambForestSnapshot;
    public float enemyTransitionTime = 0.5f;
    public float ambTransitionTime = 0.5f;
    public float enemyProximityDistance = 5f;
    public LayerMask enemyMask;

    public AudioClip[] softSteps;
    public AudioClip[] hardSteps;
    public AudioClip[] wetSteps;
    private AudioClip[] _stepClipToPlay;

    bool _enemyNear;


    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, enemyProximityDistance, transform.right, 0f, enemyMask);
        if(hits.Length > 0) {
            if(!_enemyNear) {
                Debug.Log("Enemy near");
                auxInSnapshot.TransitionTo(enemyTransitionTime);
                _enemyNear = true;
            }
        } else {
            if(_enemyNear)
            {
                Debug.Log("Enemy gone");
                idleSnapshot.TransitionTo(enemyTransitionTime);
                _enemyNear = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water")) {
            audioS.PlayOneShot(splashSound);
            _stepClipToPlay = wetSteps;
        }

        if (collision.CompareTag("Enemy"))
        {
            auxInSnapshot.TransitionTo(enemyTransitionTime);
        }

        if (collision.CompareTag("AmbientCave"))
        {
            ambCaveSnapshot.TransitionTo(ambTransitionTime);
            _stepClipToPlay = wetSteps;

        }

        if (collision.CompareTag("AmbientForest"))
        {
            ambForestSnapshot.TransitionTo(ambTransitionTime);
            _stepClipToPlay = softSteps;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) {
            audioS.PlayOneShot(splashSound);
        }

        if (collision.CompareTag("Enemy"))
        {
            idleSnapshot.TransitionTo(enemyTransitionTime);
        }

        if (collision.CompareTag("AmbientCave") || collision.CompareTag("AmbientForest"))
        {
            ambIdleSnapshot.TransitionTo(ambTransitionTime);
        }
        _stepClipToPlay = hardSteps;
    }

    public void PlayFootsteps()
    {
        Debug.Log("Play step");
        if (_stepClipToPlay == null)
        {
            _stepClipToPlay = hardSteps;
        }
        int r = Random.Range(0, _stepClipToPlay.Length);
        audioS.PlayOneShot(_stepClipToPlay[r]);
    }
}
