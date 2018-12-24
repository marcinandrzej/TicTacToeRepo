using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript
{

    public GameObject CreatePanel(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax,
       Vector3 localScale, Vector3 localPosition, Vector2 offsetMin, Vector2 offsetMax, bool offset, Vector2 sizeDelta,
       Vector3 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform);
        panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>();

        panel.GetComponent<Image>().sprite = image;
        panel.GetComponent<Image>().color = color;
        panel.GetComponent<Image>().type = Image.Type.Sliced;

        panel.GetComponent<RectTransform>().anchorMin = anchorMin; // new Vector2(0, 0);
        panel.GetComponent<RectTransform>().anchorMax = anchorMax;// new Vector2(1, 0);
        panel.GetComponent<RectTransform>().localScale = localScale;// new Vector3(1, 1, 1);
        panel.GetComponent<RectTransform>().localPosition = localPosition;// new Vector3(0, 0, 0);
        panel.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;// new Vector 3(0, 0, 0);
        panel.GetComponent<RectTransform>().sizeDelta = sizeDelta;// new Vector2(200, 150);
        if (offset)
        {
            panel.GetComponent<RectTransform>().offsetMin = offsetMin;// new Vector2(200, 0);
            panel.GetComponent<RectTransform>().offsetMax = offsetMax;// new Vector2(-200, 100);
        }

        return panel;
    }

    public GameObject CreateButton(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax,
        Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector3 anchoredPosition, Sprite image)
    {
        GameObject button = new GameObject(name);
        button.transform.SetParent(parent.transform);
        button.AddComponent<RectTransform>();
        button.AddComponent<Image>();
        button.AddComponent<Button>();

        button.GetComponent<Image>().sprite = image;
        button.GetComponent<Image>().type = Image.Type.Sliced;

        button.GetComponent<RectTransform>().anchorMin = anchorMin; // new Vector2(0, 0);
        button.GetComponent<RectTransform>().anchorMax = anchorMax;// new Vector2(1, 0);
        button.GetComponent<RectTransform>().localScale = localScale;// new Vector3(1, 1, 1);
        button.GetComponent<RectTransform>().localPosition = localPosition;// new Vector3(0, 0, 0);
        button.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;// new Vector 3(0, 0, 0);
        button.GetComponent<RectTransform>().sizeDelta = sizeDelta;// new Vector2(200, 150);

        return button;
    }

    public List<GameObject> FillWithButtons(GameObject panel, int buttonCount, int rowsCount, Sprite s)
    {
        List<GameObject> buttons = new List<GameObject>();
        float buttonW = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.x) / (float)buttonCount;
        float offsetx = buttonW / 2.0f;
        float buttonH = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.y) / (float)rowsCount;
        float offsety = buttonH / 2.0f;
        for (int j = 0; j < rowsCount; j++)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                GameObject but = CreateButton(panel, ("Button" + (j * rowsCount + i).ToString()), new Vector2(0, 1), new Vector2(0, 1),
                    new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonW, buttonH),
                    new Vector3((offsetx + i * buttonW), (-offsety - j * buttonH), 0), s);
                buttons.Add(but);
            }
        }

        return buttons;
    }

    public void SetAction(List<GameObject> buttons, action act)
    {
        foreach (GameObject but in buttons)
        {
            but.GetComponent<Button>().onClick.AddListener(delegate { act.Invoke(but); });
        }
    }

    public void BlockGrid(List<GameObject> buttons)
    {
        foreach (GameObject but in buttons)
        {
            but.GetComponent<Button>().enabled = false;
        }
    }
}
