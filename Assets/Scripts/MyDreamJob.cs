using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MyDreamJob : MonoBehaviour
{
    [SerializeField]
    private Slider dreamJobSlider;
    [SerializeField]
    private DreamJobInfo dreamJobInfoManager;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checking the scene to see if the slider has reached 100%
        //dreamJobInfoManager.CheckSlider();
    }
}
