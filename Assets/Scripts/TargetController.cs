using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;
    public void Hit(int score)
    {
        scoretext.text = "+" + score;
        StartCoroutine("OneSecond");
    }
    IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(0.5f);
        scoretext.text = "";
    }
}
