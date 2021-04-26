using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeOnTrigger : MonoBehaviour
{
    public Material materialToChangeTo;

    private Material _originalMaterial;
    private SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        _originalMaterial = rend.material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.material = materialToChangeTo;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.material = _originalMaterial;
        }
    }

}
