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
        }

        if(collision.CompareTag("Enemy"))
        {
            auxInSnapshot.TransitionTo(enemyTransitionTime);
        }

        if (collision.CompareTag("AmbientCave"))
        {
            ambCaveSnapshot.TransitionTo(ambTransitionTime);
            
        }

        if (collision.CompareTag("AmbientForest"))
        {
            ambForestSnapshot.TransitionTo(ambTransitionTime);
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
    }
}
