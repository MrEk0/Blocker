using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingsData
{
    public float volume;

    public SettingsData(GameObject gameObject)
    {
        volume = gameObject.GetComponent<Slider>().value;
    }
 
}
