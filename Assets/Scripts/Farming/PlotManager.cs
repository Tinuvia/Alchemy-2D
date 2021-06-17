using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotManager : MonoBehaviour, IPointerClickHandler
{
    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;
    int plantStage = 0;
    float timer;

    public PlantObject selectedPlant;

    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }


    /* OnAwake or Start
     * if wild
     * RandomizeColor();
    */

    // Update is called once per frame
    void Update()
    {
        if (isPlanted) // || (wild && plantstage < max)
        {
            timer -= Time.deltaTime;

            if ((timer < 0) && (plantStage < (selectedPlant.plantStages.Length - 1)))
            {
                // if wild, multiply timeBtwStages with WildMultiplierTimeBtwStages
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }     
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isPlanted) // or wild
            {
                if (plantStage == selectedPlant.plantStages.Length - 1)
                {
                    Harvest();
                }
            }
            else // check that this is skipped if wild
            {
                Plant();
            }
        }
    }

    void Harvest()
    {        
        /*
        if wild
        { 
            plantStage = 1;
            timer = selectedPlant.timeBtwStages
        } else (do things below)
        */
        isPlanted = false;
        plant.gameObject.SetActive(false);

    }

    void Plant()
    {
        plant.color = RandomizeColor(selectedPlant.colorValues);
        Debug.Log("Color:" + plant.color);
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

    Color RandomizeColor(float[] colorValues)
    {
        Color randomColor = new Color(Random.Range(colorValues[0], colorValues[3]), Random.Range(colorValues[1], colorValues[4]), Random.Range(colorValues[2], colorValues[5]), 1);
        return randomColor;
    }
}
