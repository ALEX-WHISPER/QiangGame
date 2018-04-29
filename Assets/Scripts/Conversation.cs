using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class Conversation : MonoBehaviour {
    public string[] NPCWordsContent;    //  NPC对话内容
    public string[] heroWordsContent;   //  小女孩对话内容

    public GameObject heroTalkingPanel;
    public GameObject NPCTalkingPanel;
    public bool ifMarkedWords = true;

	[HideInInspector]
	public int count = -1;

	protected bool isAlreadyTalk = false;

	protected List<string> heroWords;
	protected List<string> NPCWords;
    private Text NPCText;
    private Text heroText;
    //public Text NPCText;
    //private Text heroText;
    protected Text markedWords;
	protected void Awake()
	{
		heroWords = new List<string> ();
        NPCWords = new List<string>();

        NPCText = NPCTalkingPanel.GetComponentInChildren<Text>();
        heroText = heroTalkingPanel.GetComponentInChildren<Text>();
        //NPCText = GameObject.FindWithTag("NPCText").GetComponent<Text>();
        //heroText = GameObject.FindWithTag("heroText").GetComponent<Text>();
        if (GameObject.Find("MarkedWords") != null)
        {
            markedWords = GameObject.Find("MarkedWords").GetComponent<Text>();
        }  
	}

	protected virtual void Start()
	{
        AddNPCWordsToList();
		AddHeroWordsToList ();

        heroTalkingPanel.SetActive(false);
        NPCTalkingPanel.SetActive(false);
	}

	protected virtual void Update()
	{
        if (Input.GetMouseButtonDown(0) && count <= 2 * NPCWords.Count)
        {
            count++;

            if (count == 0)
            {
                if (markedWords != null)
                {
                    markedWords.text = "";
                }       
            }

            if (count == 2 * NPCWords.Count)
            {
                isAlreadyTalk = true;
                heroTalkingPanel.SetActive(false);
                NPCTalkingPanel.SetActive(false);

                return;
            }

            if (count % 2 != 0)
            {
                NPCText.text = NPCWords[(count - 1) / 2];

                heroTalkingPanel.SetActive(false);
                NPCTalkingPanel.SetActive(true);
            }
            else
            {
                heroText.text = heroWords[count / 2];

                heroTalkingPanel.SetActive(true);
                NPCTalkingPanel.SetActive(false);
            }

        }
	}

    protected void AddNPCWordsToList()     //  将 NPC 的对话内容添加至相应列表中
    {
        NPCWords.Clear();

        for (int i = 0; i < NPCWordsContent.Length; i++)
        {
            NPCWords.Add(NPCWordsContent[i]);
        }
    }

    protected void AddHeroWordsToList()    //  将小女孩的对话内容添加至相应列表中
    {
        heroWords.Clear();

        for (int i = 0; i < heroWordsContent.Length; i++)
        {
            heroWords.Add(heroWordsContent[i]);
        }
    }
}
