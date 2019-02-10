using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Template : MonoBehaviour
{
    public Text title;
    public Text points;
    public Image image;
    public Sprite checkedImage;
    public Sprite uncheckedImage;
    public bool checkBox;
    

   public void SetTitle(string TitleJSon)
   {
       title.text =TitleJSon;
   }
  
   public int GetPoints()
   {
       return int.Parse(points.text);
   }
   public void SetPoints(int PointsJSon)
   {
       points.text ="+"+ PointsJSon.ToString();
   }
   public void SetBool(bool Status)
   {
       checkBox= Status;
       
       if(checkBox==true)
       {
           title.color = new Color32(0,255,0,255);
           points.color = new Color32(0,255,0,255);
           image.sprite=checkedImage;
       }
       else
       {
           image.sprite=uncheckedImage;
       }
   }
  
}
