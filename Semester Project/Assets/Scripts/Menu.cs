using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public Animator animator;

    private bool isOpen = true;

    private void Update()
    {
        coinsText.text = GameManager.master.coins.ToString();
    }

    // https://docs.unity3d.com/ScriptReference/Animator.SetBool.html
    public void MenuToggle()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

}
