using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse Script é responsável por duas funções muito importantes na criação de um personagem, a adição de todas
// as entidades sumonaveis e linkadas diretamente à aquele personagem à uma lista, e a criação da sua interface
// se o JOGADOR estiver com essa personagem.
public class AegisEntityPackage : MonoBehaviour 
{
    static Dictionary<string, Object> Package;
    public static Dictionary<string, Object> GetEntities()
    {
        Object Aegis = Resources.Load("Entities/Aegis/AegisEntity") as GameObject;

        Package = new Dictionary<string, Object>();
        Package.Add("Aegis", Aegis);

        return Package;
    }

    public static Dictionary<string, Object> GetElements()
    {
        Object eAegisIceAAProjectile = Resources.Load("Elements_/Aegis/eAegisIceAAProjectile/eAegisIceAAProjectile") as GameObject;

        Package = new Dictionary<string, Object>();
        Package.Add("eAegisIceAAProjectile", eAegisIceAAProjectile);

        return Package;
    }

    public static Dictionary<string, Object> GetAnimations()
    {
        Object Anim_IceBurstA = Resources.Load("Animations/Aegis/Anim_IceBurstA") as GameObject;
        Object Anim_IceBurstB = Resources.Load("Animations/Aegis/Anim_IceBurstB") as GameObject;
        Object Anim_IceSpike = Resources.Load("Animations/Aegis/Anim_IceSpike") as GameObject;

        Package = new Dictionary<string, Object>();
        Package.Add("Anim_IceBurstA", Anim_IceBurstA);
        Package.Add("Anim_IceBurstB", Anim_IceBurstB);
        Package.Add("Anim_IceSpike", Anim_IceSpike);

        return Package;
    }

    public static void CreateInterface()
    {
        Sprite AegisPassive = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisPassive");
        Sprite AegisIceAA = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisIceAA");
        Sprite AegisIceA = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisIceA");
        Sprite AegisIceB = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisIceB");
        Sprite AegisIceC = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisIceC");
        Sprite AegisIceUlt = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisIceUlt");
        Sprite AegisFireAA = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisFireAA");
        Sprite AegisFireA = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisFireA");
        Sprite AegisFireB = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisFireB");
        Sprite AegisFireC = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisFireC");
        Sprite AegisFireUlt = Resources.Load<Sprite>("Characters/Aegis/Interface/_aegisFireUlt");

        Dictionary<string, Sprite> AegisImagesResources = new Dictionary<string, Sprite>();
        AegisImagesResources.Add("AegisPassive", AegisPassive);

        AegisImagesResources.Add("AegisIceAA", AegisIceAA);
        AegisImagesResources.Add("AegisIceA", AegisIceA);
        AegisImagesResources.Add("AegisIceB", AegisIceB);
        AegisImagesResources.Add("AegisIceC", AegisIceC);
        AegisImagesResources.Add("AegisIceUlt", AegisIceUlt);

        AegisImagesResources.Add("AegisFireAA", AegisFireAA);
        AegisImagesResources.Add("AegisFireA", AegisFireA);
        AegisImagesResources.Add("AegisFireB", AegisFireB);
        AegisImagesResources.Add("AegisFireC", AegisFireC);
        AegisImagesResources.Add("AegisFireUlt", AegisFireUlt);

        SkillBlockScript SkillBlock_Passive = GameObject.Find("SkillBlock_Passive").GetComponent<SkillBlockScript>();
        SkillBlock_Passive.ImageResources = AegisImagesResources;
        SkillBlock_Passive.skillId = 0;
       

        SkillBlockScript SkillBlock_AA = GameObject.Find("SkillBlock_AA").GetComponent<SkillBlockScript>();
        SkillBlock_AA.ImageResources = AegisImagesResources;
        SkillBlock_AA.skillId = 1;

        SkillBlockScript SkillBlock_A = GameObject.Find("SkillBlock_A").GetComponent<SkillBlockScript>();
        SkillBlock_A.ImageResources = AegisImagesResources;
        SkillBlock_A.skillId = 2;

        SkillBlockScript SkillBlock_B = GameObject.Find("SkillBlock_B").GetComponent<SkillBlockScript>();
        SkillBlock_B.ImageResources = AegisImagesResources;
        SkillBlock_B.skillId = 3;

        SkillBlockScript SkillBlock_C = GameObject.Find("SkillBlock_C").GetComponent<SkillBlockScript>();
        SkillBlock_C.ImageResources = AegisImagesResources;
        SkillBlock_C.skillId = 4;

        SkillBlockScript SkillBlock_Ult = GameObject.Find("SkillBlock_Ult").GetComponent<SkillBlockScript>();
        SkillBlock_Ult.ImageResources = AegisImagesResources;
        SkillBlock_Ult.skillId = 5;
    }
}
