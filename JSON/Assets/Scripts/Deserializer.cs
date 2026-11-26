using Newtonsoft.Json;
using UnityEngine;

public class Deserializer : MonoBehaviour
{
    [ContextMenu("Load JSON")]
    public void Load()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("DataBase_ParsedToJson");
        if (jsonAsset == null)
        {
            Debug.LogError("Файл DataBase_ParsedToJson.txt не найден в Resources");
            return;
        }
        DataBase dataBase = JsonConvert.DeserializeObject<DataBase>(jsonAsset.text);
        if (dataBase != null && dataBase.Items != null)
        {
            Debug.Log($"Загружено записей: {dataBase.Items.Length}");
            foreach (var item in dataBase.Items)
            {
                Debug.Log($"Name: {item.Name} | Description: {item.Description}");
            }
        }
        else
        {
            Debug.LogWarning("Данные пусуют или гличнуты");
        }
    }
    private void Start()
    {
        Load();
    }
}
