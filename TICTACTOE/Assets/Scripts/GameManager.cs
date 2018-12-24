using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    public Sprite spr;
    public Sprite cross;
    public Sprite circle;

    public Sprite reload;
    public Sprite exit;

    private GameObject gamePanel;
    private GameObject menuPanel;
    private List<GameObject> gameButtonList;
    private List<GameObject> menuButtonList;
    private GridScript gridScr;
    private ModelScript modelScr;
    private int player;
    private Sprite img;

    // Use this for initialization
    void Start()
    {
        player = 1;
        img = cross;
        gridScr = new GridScript();
        modelScr = new ModelScript();
        gamePanel = gridScr.CreatePanel(gameObject, "GameGrid", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
            new Vector2(100, 50), new Vector2(-100, -50), false, new Vector2(400, 400), new Vector3(-75, 0, 0), spr, new Color32(100,200,100,0));
        menuPanel = gridScr.CreatePanel(gameObject, "MenuPanel", new Vector2(1.0f, 0.5f), new Vector2(1.0f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
            new Vector2(0, 0), new Vector2(0, 0), false, new Vector2(160, 320), new Vector3(-80, 0, 0), spr, new Color32(255, 255, 255, 0));
        gameButtonList = gridScr.FillWithButtons(gamePanel, 3, 3, spr);
        menuButtonList = gridScr.FillWithButtons(menuPanel, 1, 2, spr);

        menuButtonList[0].GetComponent<Button>().image.sprite = reload;
        menuButtonList[1].GetComponent<Button>().image.sprite = exit;

        menuButtonList[0].GetComponent<Button>().onClick.AddListener(
            delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            );
        menuButtonList[1].GetComponent<Button>().onClick.AddListener(delegate { Application.Quit(); });

        action act = new action(ButtonAction);
        gridScr.SetAction(gameButtonList, act);
    }

    public void ButtonAction(GameObject button)
    {
        int index = gameButtonList.IndexOf(button);
        if (modelScr.IsFieldFree(index))
        {
            modelScr.FillField(player, index);
            button.GetComponent<Button>().image.sprite = img;
            int winner = modelScr.CheckWin();
            if (winner != 0)
            {
                gridScr.BlockGrid(gameButtonList);
                StartCoroutine(ShowWinner(winner));
            }
            ChangePlayer(player);
        }
    }

    public void ChangePlayer(int pl)
    {
        if (pl == 1)
        {
            player = -1;
            img = circle;
        }
        else
        {
            player = 1;
            img = cross;
        }
    }

    public IEnumerator ShowWinner(int winner)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject textPanel = gridScr.CreatePanel(gameObject, "TextPanel", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
           new Vector2(100, 50), new Vector2(-100, -50), false, new Vector2(400, 200), new Vector3(-75, 0, 0), spr, new Color32(255, 255, 255, 255));
        GameObject textt = new GameObject("Text");
        textt.transform.SetParent(textPanel.transform);
        textt.AddComponent<RectTransform>();
        textt.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        textt.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
        textt.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        textt.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        textt.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        textt.AddComponent<Text>();
        textt.GetComponent<Text>().text = "DRAW";
        if (winner == 1)
        {
            textt.GetComponent<Text>().text = "PLAYER 1 WINS";
        }
        else if (winner == -1)
        {
            textt.GetComponent<Text>().text = "PLAYER 2 WINS";
        }
        textt.GetComponent<Text>().resizeTextForBestFit = true;
        textt.GetComponent<Text>().resizeTextMaxSize = 40;
        textt.GetComponent<Text>().resizeTextMinSize = 10;
        textt.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
        textt.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        textt.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        textt.GetComponent<Text>().color = new Color32(0, 0, 0, 255);

        for (float i = 0.1f; i < 1.0f; i+= 0.05f)
        {
            textPanel.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.025f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
