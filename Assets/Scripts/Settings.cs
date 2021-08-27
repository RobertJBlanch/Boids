using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    public bool seperation;
    public bool cohesion;
    public bool alignment;
    public bool stayInRadius;
    public float seperationFactor = 1f;
    public float cohesionFactor = 1f;
    public float alignmentFactor = 1f;
    public float stayInRadiusFactor = 1f;
    public float neighbourRadius = 1.5f;
    public float seperationRadiusFactor = 0.5f;
    public Vector2 center;
    public float radius = 15f;
    public TextMeshProUGUI seperationText;
    public Slider sepSlider;
    public TextMeshProUGUI cohesionText;
    public Slider cohSlider;
    public TextMeshProUGUI alignmentText;
    public Slider aliSlider;
    

    void Awake(){
        instance = this;
    }

    void Start(){
        sepSlider.value = seperationFactor;
        cohSlider.value = cohesionFactor;
        aliSlider.value = alignmentFactor;
    }

    public void SetSeperation(float amount){
        seperationFactor = amount;
        seperationText.text = amount.ToString();
    }

    public void SetCohesion(float amount){
        cohesionFactor = amount;
        cohesionText.text = amount.ToString();
    }

    public void SetAlignment(float amount){
        alignmentFactor = amount;
        alignmentText.text = amount.ToString();
    }

    

}
