using UnityEngine;

public class Parser : MonoBehaviour
{
    [ContextMenu("Parse CSV to JSON")]
    public void Parse()
    {
        Utils.ParseCSV();
    }
    private void Start()
    {
        Utils.ParseCSV();
    }
}
