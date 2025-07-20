using UnityEngine;
using UnityEngine.Rendering;

public class clickSound : MonoBehaviour
{
    public static clickSound Instance;  
    [SerializeField] private AudioSource clickAudio;

    void Start()
    {
        clickAudio = GetComponent<AudioSource>();
       if(Instance == null)
        {
            Instance = this; 
        } 
    }
    public void playSound()
    {
        if(clickAudio != null && Settings.Instance.isPlayingSound)
        {
            clickAudio.Play(); 
        }
    }
   
}
