using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public ParticleSystem dustTrail;
    public Transform feetBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // DustTrail:
        // --- if player is walking - play Particle effect
        // --- if player is not walking - stop effect

        // JumpDust:
        // --- when player lands, one shot
    }
}
