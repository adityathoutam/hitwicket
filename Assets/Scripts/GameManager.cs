using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public GameObject TemplateObject;
    private int CurrentPoints;
    public Text CurrentPointsText;
    public GameObject Canvas;
    public GameObject Chest;
    public Vector3 startPos;
    public Vector3 endPos;

    private List<GameObject> Achivements;
    BaseObject baseObject = new BaseObject();


    private void Start()
    {
        StartCoroutine(GetText());
    }
    public void HideCanvas()
    {
        Canvas.SetActive(false);
    }

    public void ClaimRewardButton()
    {
        bool AllRewardsClaimed()
        {
            for (int i = 0; i < Achivements.Count; i++)
            {
                if (Achivements[i].GetComponent<Template>().checkBox == false)
                {
                    return false;
                }
            }
            Canvas.GetComponent<Canvas>().enabled = false;
          StartCoroutine(MoveObject(Chest,startPos,endPos,10f));
            return true;
            

        }
        if(!AllRewardsClaimed())
        for (int i = 0; i < Achivements.Count; i++)
        {
            
            if (Achivements[i].GetComponent<Template>().checkBox == false)
            {
                
                int score = Achivements[i].GetComponent<Template>().GetPoints();

                CurrentPoints = CurrentPoints+score;
                 CurrentPointsText.text = CurrentPoints.ToString();
                 Achivements[i].GetComponent<Animator>().SetTrigger("Checked");
                Achivements[i].GetComponent<Template>().SetBool(true);
                

                break;
            }
        }
    }

    
    
    public class Task
    {
        public string title { get; set; }
        public int points { get; set; }
        public bool completed { get; set; }
    }

    public class BaseObject
    {
        public int current_points { get; set; }
        public List<Task> tasks { get; set; }
    }
    IEnumerator MoveObject(GameObject localGB, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate * 4f;
            if (localGB != null)
            {
                localGB.transform.position = Vector3.Lerp(startPos, endPos, i);
            }
            yield return null;
        }
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_user_data.json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var n = JSON.Parse(System.Text.Encoding.Default.GetString(www.downloadHandler.data));
            CurrentPoints = n["current_points"];
             CurrentPointsText.text = CurrentPoints.ToString();
            var items = n["tasks"];
            baseObject.tasks = new List<Task>();
            Achivements = new List<GameObject>();

            for (int i = 0; i < items.Count; i++)
            {
                baseObject.tasks.Add(new Task { title = n["tasks"][i]["title"], points = n["tasks"][i]["points"], completed = n["tasks"][i]["completed"] });
                GameObject Template = Instantiate(TemplateObject) as GameObject;
                Achivements.Add(Template);
                Template.SetActive(true);
                Template.GetComponent<Template>().SetTitle(baseObject.tasks[i].title.ToUpper());
                Template.GetComponent<Template>().SetPoints(baseObject.tasks[i].points);
                Template.GetComponent<Template>().SetBool(baseObject.tasks[i].completed);

                Template.transform.SetParent(TemplateObject.transform.parent, false);
            }
        }
    }
}
