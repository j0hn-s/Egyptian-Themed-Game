using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {
    [Header("UI Component")]
    public Text textLabel;
    public Image faceImage;

    [Header("Txt file")]
    public TextAsset textFile;
    public int index;
    public float textspeed;

    [Header("Protrait")]
    public Sprite face01;
    public Sprite face02;

    bool textFinish;
    bool cancelTyping;
    List<string> textList = new List<string>();
    // Start is called before the first frame update
    private void Awake() {
        GetTextFromFile(textFile);

    }

    private void OnEnable() {
        textFinish = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count) {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            if (textFinish && !cancelTyping) {
                StartCoroutine(SetTextUI());
            } else if (!textFinish) {
                cancelTyping = !cancelTyping;
            }

        }
    }

    void GetTextFromFile(TextAsset file) {
        textList.Clear();
        index = 0;
        // Split by line
        var lineDate = file.text.Split('\n');
        // Import each row of data read into the list
        foreach (var line in lineDate) {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI() {
        textFinish = false;
        textLabel.text = "";

        switch (textList[index][0]) {
            case 'A':
                faceImage.sprite = face01;
                index++;
                break;
            case 'B':
                faceImage.sprite = face02;
                index++;
                break;
        }
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1) {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textspeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinish = true;
        index++;
    }

}