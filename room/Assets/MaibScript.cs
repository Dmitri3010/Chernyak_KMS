using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaibScript : MonoBehaviour {

    public GameObject water;
    public float V, T, U, R, h=10, startpozition;
    int time = 1, timer = 1;
    public TextMesh Ttext, Rtext, Utext, Timetext;
    public bool IsWork = false;
    [SerializeField]
    private AudioClip water_sound;


    IEnumerator Example()
    {
        yield return new WaitForSeconds(4.0f);
        time = 1;
    }

    private void UpdateTextValues()
    {
        GameObject _RValue = GameObject.Find("Rvalue");
        GameObject _UValue = GameObject.Find("UValue");
      
        Rtext = _RValue.GetComponent<TextMesh>(); 
        Utext = _UValue.GetComponent<TextMesh>();
         
       
       Utext.text = U.ToString("f2");
       Rtext.text = R.ToString("f2");
    }

    public void UpdateTempridge()
    {

        if (time == 1)
        {

            StartCoroutine(Example());
            if (T <= 100)
            {
                if((R-5)>0 && (U - 200f) > 0)
                {
                    T += (R - 5) + (U - 200f);
                }

                else
                {
                    T += R;
                }

                Invoke("Timer", 0.1f);
                
            }           

            Ttext.text = T.ToString("f0");
            Timetext.text = timer.ToString("f2");
          

        }
    }

    public void Timer()
    {
        timer++;
    }

    void Start () {
        GameObject _TimeValue = GameObject.Find("time");
        GameObject _TValue = GameObject.Find("TValue");

        Timetext = _TimeValue.GetComponent<TextMesh>();
        Ttext = _TValue.GetComponent<TextMesh>();

    }

    void Update () {
		  if (Input.GetKeyDown(KeyCode.F))
          {
            IsWork = true;
              GetComponent<AudioSource>().PlayOneShot(water_sound); 
                
          }

        if (IsWork == true)
        {
            UpdateTempridge();
            
        }
        

    }

    public void Restart()
    {
        var temp = water.transform.position;
        temp.y += startpozition;
        water.transform.position = temp;
        startpozition = 0f;
        IsWork = false;
        U = 0;
        R = 0;
        T = 0;
        Ttext.text = "0";
        Timetext.text = "0";
        Utext.text = "0";
        Rtext.text = "0";
        timer = 0;

    }

    public void ApplyParams()
    {
        UpdateTextValues();
        startpozition = 0f;
        var temp = water.transform.position;
        if (V > 9)
        {
            startpozition = 0.01f;
        }
       else if (V >8 && V<9)
        {
            startpozition = 0.01f;
        }

        else if (V > 7 && V < 8)
        {
            startpozition = 0.1f;
        }

        else if (V > 6 && V < 7)
        {
            startpozition = 0.2f;
        }
        else if (V > 5 && V < 6)
        {
            startpozition = 0.3f;
        }
        else if (V > 4 && V < 5)
        {
            startpozition = 0.3f;
        }
        else if (V > 3 && V < 5)
        {
            startpozition = 0.4f;
        }
        else if (V > 2 && V < 3)
        {
            startpozition =0.5f;
        }
        else if (V >= 1 && V < 2)
        {
            startpozition = 0.60f;
        }

        temp.y -= startpozition;
        water.transform.position = temp;

    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, h, 350, 320));
        GUI.Box(new Rect(0, 0, 200, 270), "Параметры установки");
        GUI.Label(new Rect(15, 20, 200, 30), "Велечина напряжения   " + U);
        U = GUI.HorizontalSlider(new Rect(15, 55, 170, 30), U, 200.0f, 215.0f);
        GUI.Label(new Rect(15, 90, 200, 30), "Сопротивление   " + R);
        R = GUI.HorizontalSlider(new Rect(15, 140, 170, 30), R, 1.0f, 10.0f);
        GUI.Label(new Rect(15, 160, 200, 30), "Объем воды   " +V );
        V = GUI.HorizontalSlider(new Rect(15, 190, 170, 30), V, 1, 10);
        if (GUI.Button(new Rect(15, 220, 170, 20), "ок"))
        {
            ApplyParams();
        }
       
        if (GUI.Button(new Rect(15, 240, 170, 20), "Заново"))
        {
            Restart();
        }
        GUI.EndGroup();
    }
}
