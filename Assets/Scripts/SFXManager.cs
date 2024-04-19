using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SFXManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

    public SoundEffect[] soundEffects;
    private AudioSource audioSource;

    // Singleton instance of the SoundManager
    public static SFXManager Instance;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            audioSource = GetComponent<AudioSource>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Method to play a sound effect by name
    public void PlaySound(string clipName)
    {
        // Find the sound effect with the specified name
        SoundEffect soundEffect = System.Array.Find(soundEffects, s => s.name == clipName);

        // If the sound effect is found, play it
        if (soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect with name '" + clipName + "' not found.");
        }
    }
}


