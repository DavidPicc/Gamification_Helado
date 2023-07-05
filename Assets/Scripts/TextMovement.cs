using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public float speed = 5f;
    public float resetPosition = 10f;
    [SerializeField] TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private TextMeshProUGUI cloneText;

    [SerializeField] public static int[] _scores;
    [SerializeField] int[] scores;
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    bool updatedScore = false;
    int lastScore;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        lastScore = PlayerPrefs.GetInt("lastScore", 0);

        for(int i  = 0; i < scores.Length; i++)
        {
            scores[i] = PlayerPrefs.GetInt("topScore" + i, 0);
            Debug.Log(PlayerPrefs.GetInt("topScore" + i, 0));
        }

        for (int i = 0; i < scores.Length; i++)
        {
            int temp1 = scores[i];
            if (lastScore > scores[i])
            {
                // Update the top score
                if(i+2 < scores.Length)
                {
                    scores[i + 2] = scores[i+1];
                }
                if (i + 1 < scores.Length)
                {
                    scores[i + 1] = scores[i];
                }
                scores[i] = lastScore;


                // Save the updated top scores
                SaveTopScores();

                // Break the loop since we only update one score
                break;
            }
        }

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] != 0)
            {
                scoreTexts[i].text = (i + 1).ToString() + ". " + scores[i].ToString() + " pts";
            }
            else
            {
                scoreTexts[i].text = (i + 1).ToString() + ". TBD";
            }
        }

        CreateCloneText();
    }

    private void SaveTopScores()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            PlayerPrefs.SetInt("topScore" + i, scores[i]);
        }

        PlayerPrefs.DeleteKey("lastScore");

        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void Start()
    {
        StartCoroutine(MoveText());
    }

    private IEnumerator MoveText()
    {
        while (true)
        {
            while (textMeshPro.rectTransform.anchoredPosition.x > -textMeshPro.rectTransform.sizeDelta.x)
            {
                textMeshPro.rectTransform.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0f);
                yield return null;
            }

            textMeshPro.rectTransform.anchoredPosition = new Vector2(resetPosition, textMeshPro.rectTransform.anchoredPosition.y);
        }
    }

    private void CreateCloneText()
    {
        cloneText = Instantiate(textMeshPro, textMeshPro.rectTransform);
        RectTransform cloneRT = cloneText.GetComponent<RectTransform>();
        cloneRT.SetParent(textMeshPro.rectTransform);
        cloneRT.anchorMin = new Vector2(1, 0.5f);
        cloneRT.anchorMax = new Vector2(1, 0.5f);
        cloneRT.pivot = new Vector2(1, 0.5f);
        cloneRT.anchoredPosition = new Vector2(textMeshPro.rectTransform.sizeDelta.x, 0f);
    }
}
