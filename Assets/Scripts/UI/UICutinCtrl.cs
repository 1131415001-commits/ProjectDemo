using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICutInCtrl : MonoBehaviour
{
    public static UICutInCtrl instance;

    public Animator animator;

    public Text cutInText;
    public string startInfo;
    public string endInfo;

private void Awake()
{
instance = this;
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("開始訊息")]
    public void StartInfo()
    {
        cutInText.text = startInfo;
        animator.SetTrigger("Start")
    }

 [ContextMenu("結束訊息")]
public void EndInfo()
{
cutInText.text = EndInfo;
        animator.SetTrigger("End")
  }
}