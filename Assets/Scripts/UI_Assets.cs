using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Assets : MonoBehaviour
{
    public static UI_Assets instance { get; private set; }

    void Awake() {
        instance = this;
    }

    public Sprite pauseBtnSprite;
    public Sprite playBtnSprite;
}
