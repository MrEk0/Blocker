using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance=null;

    [Header("Audio Clips")]

    [SerializeField] AudioClip mainClip;
    [SerializeField] AudioClip mouseClickClip;
    [SerializeField] AudioClip footstepsClip;
    [SerializeField] AudioClip blowClip;
    [SerializeField] AudioClip blockClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip lettersClip;
    [SerializeField] AudioClip buttonsClip;
    [SerializeField] AudioClip ouchClip;

    [Header("Audio Mixers Groups")]
    [SerializeField] AudioMixerGroup mainGroup;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup movementGroup;
    [SerializeField] AudioMixerGroup mouseclickGroup;
    [SerializeField] AudioMixerGroup blowGroup;

    public float volumeFootsteps;
    public float volumejump;

    public Slider volumeSlider;

    private AudioSource mainSource;
    private AudioSource mouseClickSource;
    private AudioSource movementSource;
    private AudioSource blowSource;
    private AudioSource blockSource;

    private GameObject soundOnImage;
    private GameObject soundOffImage;

    MainCanvasUI mainCanvas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //...destroy this and exit. There can be only one UIManager
            Destroy(gameObject);
            return;
        }

        //This is the current UIManager and it should persist between scene loads
        instance = this;
        DontDestroyOnLoad(gameObject);

        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);//this

        mainSource          = gameObject.AddComponent<AudioSource>() as AudioSource;
        mouseClickSource    = gameObject.AddComponent<AudioSource>() as AudioSource;
        movementSource      = gameObject.AddComponent<AudioSource>() as AudioSource;
        blowSource          = gameObject.AddComponent<AudioSource>() as AudioSource;
        blockSource         = gameObject.AddComponent<AudioSource>() as AudioSource;

        mainSource.outputAudioMixerGroup          = musicGroup;
        mouseClickSource.outputAudioMixerGroup    = mouseclickGroup;
        movementSource.outputAudioMixerGroup      = movementGroup;
        blowSource.outputAudioMixerGroup          = blowGroup;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainSource.clip = mainClip;
        mainSource.loop = true;
        mainSource.Play();
        StateVolumeSound();
    }

    public void StateVolumeSound()
    {
        volumeSlider.value = PlayerPrefsController.GetVolume();
    }

    public static void PlayMouseSound()
    {
        instance.mouseClickSource.PlayOneShot(instance.mouseClickClip);
    }

    public static void PlayFootstepsSound()
    {
        instance.movementSource.clip = instance.footstepsClip;
        instance.movementSource.volume =instance.volumeFootsteps;
        instance.movementSource.Play();
    }

    public static void PlayBlowSound()
    {
        //instance.blowSource.clip = instance.blowClip;
        instance.blowSource.PlayOneShot(instance.blowClip);
    }

    public static void PlayBlockSound()
    {
        //instance.blockSource.clip = instance.blockClip;
        instance.blockSource.PlayOneShot(instance.blockClip);
    }

    public static void PlayJumpSound()
    {
        instance.movementSource.clip = instance.jumpClip;
        instance.movementSource.volume = instance.volumejump;
        instance.movementSource.Play();
    }

    public void SetVolume(float volume)
    {
        PlayerPrefsController.SetVolume(volume);
        //Debug.Log(PlayerPrefsController.GetVolume());
        mainGroup.audioMixer.SetFloat("volume", volume);
        soundOnImage = GameObject.Find("SoundImage");
        soundOffImage = GameObject.Find("SoundOffImage");
        if (soundOnImage != null && soundOffImage != null)
        {
            if (volume <= volumeSlider.minValue / 2)
            {
                //if (soundOnImage.GetComponent<Image>().enabled)
                //{
                    soundOnImage.GetComponent<Image>().enabled = false;
                    soundOffImage.GetComponent<Image>().enabled = true;
                //}
            }
            else if (volume >= volumeSlider.minValue / 2)
            {
                //if (soundOffImage.GetComponent<Image>().enabled)
                //{
                    soundOffImage.GetComponent<Image>().enabled = false;
                    soundOnImage.GetComponent<Image>().enabled = true;
                //}
            }
        }
    }

    public void OffMusic()
    {
        instance.mainSource.volume = 0f;
        PlayMouseSound();
    }

    public void OnMusic()
    {
        instance.mainSource.volume = 1f;
        PlayMouseSound();
    }

    public static void PlayLettersSound()
    {
        instance.movementSource.PlayOneShot(instance.lettersClip);
    }

    public static void PlayButtonsSound()
    {
        instance.movementSource.PlayOneShot(instance.buttonsClip);
    }

    public static void PlayOuchSound()
    {
        instance.movementSource.PlayOneShot(instance.ouchClip);
    }

}
