using System.IO;
using UnityEngine;

public class MinotaurAudio : MonoBehaviour
{
    private AudioSource m_Source;

    private float m_PlannedTime;

    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        m_Source = GetComponent<AudioSource>();
        if (m_Source == null)
        {
            Debug.LogError("AudioSource is missing from object " + name + "!");
            return;
        }
        
        PlanAudioRandom();
    }

    private void PlanAudioRandom()
    {
        m_PlannedTime = Time.time + 10; //Random.Range(30, 90); // Somewhere between 30s and 1.5min
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Source != null && clips.Length > 0 && Time.time >= m_PlannedTime)
        {
            int clip = Random.Range(0, clips.Length);
            m_Source.PlayOneShot(clips[clip]);
            // Play a random sound
            PlanAudioRandom();
        }
    }
}