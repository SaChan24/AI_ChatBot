using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using uLipSync;

public class WordToVoice : MonoBehaviour
{
    public InputField inputField;
    public AudioSource audioSource1;
    public uLipSync.uLipSync lipSync;

    // กำหนด mapping คำ → เสียง
    public AudioClip helloClip;
    public AudioClip campfireClip;
    public AudioClip catClip;

    private Dictionary<string, AudioClip> voiceDictionary;

    private void Start()    
    {
        // สร้าง dictionary
        voiceDictionary = new Dictionary<string, AudioClip>()
        {
            { "hello", helloClip },
            { "campfire", campfireClip },
            { "cat", catClip }
        };

        // ฟัง event ตอนพิมเสร็จ (กด Enter)
        inputField.onEndEdit.AddListener(OnSubmitText);

        // ผูก lipsync กับ audio source
        lipSync.audioSource1 = audioSource1;
    }

    private void OnSubmitText(string text)
    {
        if (string.IsNullOrEmpty(text)) return;

        text = text.ToLower().Trim(); // ทำให้เล็กหมด ป้องกันพิมพ์ผิด case

        if (voiceDictionary.TryGetValue(text, out AudioClip clip))
        {
            // หยุดเสียงเก่า
            if (audioSource1.isPlaying) audioSource1.Stop();

            // เล่นเสียงใหม่
            audioSource1.clip = clip;
            audioSource1.Play();
        }
        else
        {
            Debug.Log("ไม่พบเสียงสำหรับคำว่า: " + text);
        }

        // ล้าง input หลังพิม
        inputField.text = "";
    }
}
