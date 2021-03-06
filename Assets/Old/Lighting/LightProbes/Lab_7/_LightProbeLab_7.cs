using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
public class _LightProbeLab_7 : MonoBehaviour {
    public Transform actor;
    Vector3 w2Scr;
    public float posx;
    public float factor;
    public Rect[] rects;
    public string[] labels;
    public Rect[] rectL;
    public string shs;
    public Rect rectSH;
    public GUISkin skin;
    //
    public SphericalHarmonicsL2[] m_BaseCoefficients;
    public SphericalHarmonicsL2[] m_Coefficients;
	// Use this for initialization
	void Start () {
        if (LightmapSettings.lightProbes == null) return;
        factor = 1.0f;
        m_BaseCoefficients = new SphericalHarmonicsL2[LightmapSettings.lightProbes.bakedProbes.Length];
        Array.Copy(LightmapSettings.lightProbes.bakedProbes, m_BaseCoefficients, LightmapSettings.lightProbes.bakedProbes.Length);

        m_Coefficients = new SphericalHarmonicsL2[LightmapSettings.lightProbes.bakedProbes.Length];
	}
	
	// Update is called once per frame
	void Update () {
        
        w2Scr=Camera.main.WorldToScreenPoint(actor.position);
        if (LightmapSettings.lightProbes == null) return;
        actor.position = new Vector3(posx, actor.position.y, actor.position.z);
        for (int i=0;i<m_BaseCoefficients.Length;i++)
        {
            m_Coefficients[i] = m_BaseCoefficients[i] *factor;
        }
        LightmapSettings.lightProbes.bakedProbes = m_Coefficients;
	}
    void OnGUI()
    {
        GUI.skin = skin;
        for (int i = 0; i < rectL.Length; i++)
        {
            GUI.Label(rectL[i], labels[i]);
        }
        posx = GUI.HorizontalSlider(rects[0], posx, -4.2f, 4.2f);
        factor = GUI.HorizontalSlider(rects[1], factor, 0f, 2f);
        GUI.Label(new Rect(w2Scr.x-rectSH.width/2,rectSH.y,rectSH.width,rectSH.height),shs);
    }
}
