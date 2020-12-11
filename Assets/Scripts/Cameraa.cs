using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameraa : MonoBehaviour
{
    public Rocket rocket;
    public Text fuelText;

    void setFuelText(){
        fuelText.text = "Fuel: " + Mathf.Round( rocket.getFuel() ).ToString();
    }

    void Start()
    {
        setFuelText();
    }

    void Update()
    {
        setFuelText();
    }
}
