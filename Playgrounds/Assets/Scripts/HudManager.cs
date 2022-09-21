using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Stats stats;


    public Text statText;
    public Text statLevelText;
    public Text statHPText;
    public Text statDamageText;
    public Text statSpeedText;


    //public Image statImageSprite;

    void Start()
    {
        statText.text = stats.Name;
        //statImageSprite.sprite = stats.Image;
        statLevelText.text = "Level = " + stats.characterExperience.GetLevel();
        statHPText.text = "HP = " + stats.characterAttributes.HP;
        statDamageText.text = "Damage = " + stats.characterAttributes.Damage;
        statSpeedText.text = "Speed = " + stats.characterAttributes.Speed;
    }
}
