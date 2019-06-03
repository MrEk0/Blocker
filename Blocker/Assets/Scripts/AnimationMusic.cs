using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMusic : MonoBehaviour
{
    public void PlayAnimation()
    {
        AudioManager.PlayLettersSound();
    }

    public void PlayAnimationButtons()
    {
        AudioManager.PlayButtonsSound();
    }

    public void PlayAnimationfootsteps()
    {
        AudioManager.PlayFootstepsSound();
    }

    //public void Next()
    //{
    //    Debug.Log("OOah");
    //}
}

