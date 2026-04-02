using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScore : MonoBehaviour
{
    [Header("Sprite số 0-9 (kéo đúng thứ tự)")]
    public Sprite[] digitSprites;
    public float digitWidth = 32f;
    public float digitSpacing = 4f;

    private int currentScore = 0;
    private bool initialized = false;

    void OnEnable()
    {
        if(!initialized)
        {
            initialized = true;
            UpdateDisplay(0);
        }
        else
        {
            UpdateDisplay(currentScore);
        }
    }

    /*void Start()
    {
        UpdateDisplay(0);
    }*/

    public void SetScore(int score)
    {
        initialized = true;
        currentScore = score;
        UpdateDisplay(score);
    }

    public void UpdateDisplay(int score)
    {
        // Xoá hết Image con cũ
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Tách từng chữ số
        string scoreStr = score.ToString();

        // Tính tổng chiều rộng để căn giữa
        float totalWidth = scoreStr.Length * (digitSpacing + digitWidth) - digitSpacing;
        float startX = -totalWidth / 2f + digitWidth / 2f;

        for (int i = 0; i < scoreStr.Length; i++)
        {
            int digit = int.Parse(scoreStr[i].ToString());

            // Tạo Image cho từng chữ số
            GameObject digitObj = new GameObject("digit_" + i);
            digitObj.transform.SetParent(transform, false);

            Image img = digitObj.AddComponent<Image>();
            img.sprite = digitSprites[digit];
            img.SetNativeSize(); // dùng đúng kích thước sprite gốc

            //Can vi tri
            RectTransform rt = digitObj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + i * (digitWidth + digitSpacing), 0);

        }
    }
   
}
