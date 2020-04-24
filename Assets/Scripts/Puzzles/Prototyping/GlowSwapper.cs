using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class GlowSwapper : MonoBehaviour
{

    public Material glowMaterial;
    private Material[] origionalMaterials;
    private Material[] glowMaterials;
    private InteractableHoverEvents hoverEvents;
    
    public enum ShaderType
    {
        Default,
        WoodShader
    }

    public ShaderType type = ShaderType.Default;
    
    private Renderer mr;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            hoverEvents = transform.parent.gameObject.GetComponent<InteractableHoverEvents>();
            hoverEvents.onHandHoverBegin.AddListener(OnHoverStart);
            hoverEvents.onHandHoverEnd.AddListener(OnHoverEnd);
            mr = GetComponent<Renderer>();
            origionalMaterials = mr.materials;
            glowMaterials = new Material[origionalMaterials.Length];
            for (int i = 0; i < origionalMaterials.Length; i++)
            {
                switch (type)
                {
                    case ShaderType.Default:
                        glowMaterials[i] = new Material(Shader.Find(glowMaterial.shader.name));
                        glowMaterials[i].SetTexture("GlowBaseTexture", origionalMaterials[i].mainTexture);
                        glowMaterials[i].SetColor("GlowBaseColor", origionalMaterials[i].color);
                        break;
                    case ShaderType.WoodShader:
                        glowMaterials[i] = new Material(Shader.Find(glowMaterial.shader.name));
                        glowMaterials[i].SetTexture("GlowBaseTexture", origionalMaterials[i].GetTexture("Texture2D_CD56FE87"));
                        Color c = origionalMaterials[i].GetColor("Color_A657990C");
                        c.a = 1;
                        glowMaterials[i].SetColor("GlowBaseColor", c);
                        break;
                }
            
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.StackTrace, this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverStart()
    {
        mr.materials = glowMaterials;
    }

    public void OnHoverEnd()
    {
        mr.materials = origionalMaterials;
    }
}
