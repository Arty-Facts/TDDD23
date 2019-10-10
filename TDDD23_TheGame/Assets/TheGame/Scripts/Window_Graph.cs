using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    public GameObject acc; 
    public GameObject wpm; 
    public GameObject ct; 
    public GameObject time; 
    public GameObject F1;
    public GameObject bar;


    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }



    public void ShowGraph(List<Storing> valueList) {
        foreach (Transform child in transform.Find("graphContainer").GetComponent<Transform>()) {
            GameObject.Destroy(child.gameObject);
        }
        float WPM = ShowGraphWPM(valueList);
        float ACC = ShowGraphACC(valueList);
        float CT = ShowGraphCT(valueList);
        float Time = ShowGraphTime(valueList);
        //print(WPM +","+  ACC +","+  CT +","+  Time);
        float f1 = 4*(WPM * ACC * CT * Time)/(WPM + ACC + CT + Time);
        //endGameStats.SetF1(f1.ToString("F3"));
        F1.GetComponent<RectTransform>().anchoredPosition = new Vector2(1125f, graphContainer.sizeDelta.y*f1 + 30);
        F1.GetComponent<Text>().text =  f1.ToString("F2");

        bar.GetComponent<RectTransform>().anchoredPosition = new Vector2((graphContainer.sizeDelta.y*f1/2)+10, 0);
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(graphContainer.sizeDelta.y*f1, 90f);
    }
    public float ShowGraphWPM(List<Storing> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 0f;
        float yMinimum = 999999999f;
        for (int i = 0; i < valueList.Count; i++) {
            yMaximum = Mathf.Max(valueList[i].WPM, yMaximum);
            yMinimum = Mathf.Min(valueList[i].WPM, yMinimum);
        }
        float xSize = graphWidth/valueList.Count;
        float yPosition = 0;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize/2 + i * xSize;
            yPosition = ((valueList[i].WPM - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), new Color(1,1,1,1f));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Color(1,1,1,.5f));
            }
            lastCircleGameObject = circleGameObject;
        }
        //wpm.GetComponent<RectTransform>().anchoredPosition = new Vector2(graphWidth+100, yPosition+40);
        wpm.GetComponent<Text>().text = "WPM: " + valueList[valueList.Count-1].WPM.ToString("F2");
        return yPosition/graphHeight;
    }
    public float ShowGraphACC(List<Storing> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 0f;
        float yMinimum = 1f;        
        for (int i = 0; i < valueList.Count; i++) {
            yMaximum = Mathf.Max(valueList[i].ACC, yMaximum);
            yMinimum = Mathf.Min(valueList[i].ACC, yMinimum);
        }
        float xSize = graphWidth/valueList.Count;
        float yPosition = 0;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize/2 + i * xSize;
            yPosition = ((valueList[i].ACC - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), new Color(1,1,0,1f));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Color(1,1,0,.5f));
            }
            lastCircleGameObject = circleGameObject;
        }
        //acc.GetComponent<RectTransform>().anchoredPosition = new Vector2(graphWidth+100, yPosition+40);
        acc.GetComponent<Text>().text = "ACC: " + valueList[valueList.Count-1].ACC.ToString("F2");
        return yPosition/graphHeight;
    }
    public float ShowGraphCT(List<Storing> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 0f;
        float yMinimum = 0.6f;        
        for (int i = 0; i < valueList.Count; i++) {
            yMaximum = Mathf.Max(valueList[i].CT, yMaximum);
            yMinimum = Mathf.Min(valueList[i].CT, yMinimum);
        }
        float xSize = graphWidth/valueList.Count;
        float yPosition = 0;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize/2 +i * xSize;
            yPosition = ((valueList[i].CT - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), new Color(1,0,1,1f));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Color(1,0,1,.5f));
            }
            lastCircleGameObject = circleGameObject;
        }
        //ct.GetComponent<RectTransform>().anchoredPosition = new Vector2(graphWidth+100, yPosition+40);
        ct.GetComponent<Text>().text = "CT: " + valueList[valueList.Count-1].CT.ToString("F2");
        
        return yPosition/graphHeight;
    }
    public float ShowGraphTime(List<Storing> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 180f;
        float yMinimum = 0f;        
        for (int i = 0; i < valueList.Count; i++) {
            yMaximum = Mathf.Max(Mathf.Min(valueList[i].Time, 180), yMaximum);
            yMinimum = Mathf.Min(valueList[i].Time, yMinimum);
        }
        float xSize = graphWidth/valueList.Count;
        float yPosition = 0;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize/2 + i * xSize;
            yPosition = ((Mathf.Min(valueList[i].Time, 180) - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), new Color(0,1,1,1f));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Color(0,1,1,.5f));
            }
            lastCircleGameObject = circleGameObject;
        }
        //time.GetComponent<RectTransform>().anchoredPosition = new Vector2(graphWidth+100, yPosition+40);
        time.GetComponent<Text>().text = "Time: " + valueList[valueList.Count-1].Time.ToString("F1") + "s";
        return yPosition/graphHeight;
    }
    private GameObject CreateCircle(Vector2 anchoredPosition, Color color ) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }


    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color ) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
