using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    public GameObject player;
    public Slider healthSlider;
    Health playerhealth;

    private void Start()
    {
        //Debug.Log(player == null);
        playerhealth = player.GetComponent<Health>();
        // Debug.Log("health:");
        // Debug.Log(playerhealth == null);
        // Debug.Log(healthSlider.maxValue);

        healthSlider.wholeNumbers = true; //Accept whole numbers only
        healthSlider.minValue = 0; //Set min and max health
        healthSlider.maxValue = playerhealth.GetMaxHealth();
        healthSlider.value = playerhealth.GetHealth(); //Set current value of Healthbar

        //Debug.Log(healthSlider.value);

    }

    //Update health on slider
    private void Update(){

        healthSlider.value = playerhealth.GetHealth(); 
    }
}
