using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingDmg : MonoBehaviour {

    public Animator animator;
    Text dmgText;
    AnimatorClipInfo[] clipInfo;

    public void SetText(string dmg) {
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        dmgText = GetComponentInChildren<Text>();
        dmgText.text = dmg;
        Destroy(gameObject, clipInfo[0].clip.length);
    }
}
