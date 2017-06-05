using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu: Photon.MonoBehaviour
{
    public GameObject menue;
    // 可以不用，因為 button 已經寫入 On Click() to cell back startlevel()
    //public Button startbutton;

    public Text ID;
    public InputField input;

    // Use this for initialization
    void Start () {
        //startbutton = startbutton.GetComponent<Button>(); // 不用理由同上
        // 取得 Canvas 中的 Text Component
        ID = GameObject.Find("name").GetComponent<Text>();
        // name 那邊沒有用 InputField 物件，這邊就不用在取得一次，因為 unity 中已經有拉進去了
        //input = GetComponent<InputField>();
    }

    // 按下按鈕 On Click()'s cell back
    public void startlevel() {
        // 將選單關閉
        menue.SetActive(false);
    }

    void Update () {
        ID.text = input.text;
    }
}
