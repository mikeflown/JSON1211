using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SoundData
{
    public float Volume = 1f;
}
public class SavedDataController : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    public Slider volumeSlider;
    private string filePath;
    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "Resources", "SoundData.txt");
    }
    private void Start()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SoundData data = JsonConvert.DeserializeObject<SoundData>(json);
            if (data != null)
            {
                float vol = Mathf.Clamp01(data.Volume);
                if (audioSource != null) audioSource.volume = vol;
                if (volumeSlider != null) volumeSlider.value = vol;
                Debug.Log($"Загружена громкость: {vol}");
            }
        }
        else
        {
            Save();
        }
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }
    private void OnSliderChanged(float value)
    {
        if (audioSource != null)
            audioSource.volume = value;
        Save();
    }
    private void Save()
    {
        float current = volumeSlider != null ? volumeSlider.value :
            (audioSource != null ? audioSource.volume : 1f);
        SoundData data = new SoundData { Volume = current };
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(filePath, json);
    }
    private void OnDestroy()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}