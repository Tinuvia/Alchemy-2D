using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerColor
{
    Blue,
    Red
};

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    [Header("Particles systems")]
    public GameObject particleObject;

    [Header("Particles systems Materials"), Space(10)]
    public Material greenMaterial;
    public Material redMaterial;

    private List<ParticleSystem> particlesList;
    private ParticleSystemRenderer redrer;

    public void Awake()
    {
        Instance = this;
        particlesList = new List<ParticleSystem>();
    }

    public void PlayParticleFromPool(PlayerColor color, Vector3 position, Quaternion rotation)
    {
        if (particlesList.Count < 1)
        {
            GameObject firstParticleObject = Instantiate(particleObject, position, rotation);
            particlesList.Add(firstParticleObject.GetComponent<ParticleSystem>());
            firstParticleObject.transform.parent = transform;
        }

        for (int i = 0; i < particlesList.Count; i++)
        {
            if (!particlesList[i].isPlaying)
            {
                redrer = particlesList[i].GetComponent<ParticleSystemRenderer>();
                switch (color)
                {
                    case PlayerColor.Blue:
                        redrer.material = greenMaterial;
                        break;
                    default:
                        redrer.material = redMaterial;
                        break;
                }

                particlesList[i].Play();
                return;
            }
        }

        GameObject newParticleObject = Instantiate(particleObject, position, rotation);
        newParticleObject.transform.parent = transform;
        particlesList.Add(newParticleObject.GetComponent<ParticleSystem>());
        redrer = newParticleObject.GetComponent<ParticleSystemRenderer>();
        switch (color)
        {
            case PlayerColor.Blue:
                redrer.material = greenMaterial;
                break;
            default:
                redrer.material = redMaterial;
                break;
        }
        newParticleObject.GetComponent<ParticleSystem>().Play();
    }
}
