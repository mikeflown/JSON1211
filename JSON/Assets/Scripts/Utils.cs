using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{
    public static void ParseCSV(string csvFileName = "DataTable", string outputFileName = "DataBase_ParsedToJson")
    {
        TextAsset csvAsset = Resources.Load<TextAsset>(csvFileName);
        if (csvAsset == null)
        {
            Debug.LogError($"CSV файл '{csvFileName}' не найден -_-");
            return;
        }
        string[] lines = csvAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 2)
        {
            Debug.LogError("идиот иди поспи уже");
            return;
        }
        List<Data> dataList = new List<Data>();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');
            if (columns.Length >= 2)
            {
                string name = columns[0].Trim();
                string description = columns[1].Trim();
                dataList.Add(new Data(name, description));
            }
        }
        DataBase dataBase = new DataBase(dataList.ToArray());
        string resourcesPath = Path.Combine(Application.dataPath, "Resources");
        if (!Directory.Exists(resourcesPath))
            Directory.CreateDirectory(resourcesPath);
        string outputPath = Path.Combine(resourcesPath, outputFileName + ".txt");
        using (StreamWriter sw = new StreamWriter(outputPath))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            writer.Formatting = Formatting.Indented;
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, dataBase);
        }
        Debug.Log($"JSON успешно создан: {outputPath}");
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
