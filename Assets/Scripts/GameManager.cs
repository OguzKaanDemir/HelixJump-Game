using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject PuanPanel;
    public GameObject loadingPanel;

    public Transform[] levelPozisions;
    public Transform[] parkours;

    GameObject Canvas;
    Ball ball;
    public int point = 0, levelIndex = 0;
    Text pointText;

    Image image;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] myColors;
    int colorIndex = 0, len;
    float t = 0f;
    
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        pointText = PuanPanel.GetComponentInChildren<Text>();
        image = loadingPanel.GetComponent<Image>();
        len = myColors.Length;
    }

    void Update()
    {
        IsDied();
        StartGame(levelIndex);
        ChangePoint();

        image.color = Color.Lerp(image.color, myColors[colorIndex], lerpTime);
        t = Mathf.Lerp(t, 1f, lerpTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }

    void IsDied()
    {
        if (ball.isDied)
        {
            StartPanel.SetActive(true);
        }
    }

    void StartGame(int index)
    {
        if (StartPanel.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                loadingPanel.SetActive(true);
                ball.isDied = false;
                StartPanel.SetActive(false);

                ball.transform.DOMove(levelPozisions[levelIndex].position, 1f).OnComplete(() =>
                {
                    System.Threading.Thread.Sleep(100);
                    loadingPanel.SetActive(false);
                    ball.rb.isKinematic = false;
                    parkours[levelIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
                    point *= 0;
                });
                Time.timeScale = 1f;
            }
        }
    }

    void ChangePoint()
    {
        pointText.text = point.ToString();
    }
}
