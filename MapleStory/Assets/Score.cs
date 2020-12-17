using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public enum ScoreKind
{
    ATK,
    EXP,
    HP
}
public class Score : MonoBehaviour
{
    public string basicContent;
    public ScoreKind scoreKind;
    Status status;
    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.Find("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreKind == ScoreKind.ATK)
        {
            GetComponent<TextMeshProUGUI>().text = basicContent + status.aTK;
        }
        else if (scoreKind == ScoreKind.EXP)
        {
            GetComponent<TextMeshProUGUI>().text = basicContent + status.eXPGained;
        }
        else if (scoreKind == ScoreKind.HP)
        {
            GetComponent<TextMeshProUGUI>().text = basicContent + status.currentHP;
        }
    }
}
