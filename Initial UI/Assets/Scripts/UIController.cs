using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Slider _Slider, _sfxSlider;
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
 
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_Slider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

}
