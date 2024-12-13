using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private Animator TransitionAnimator;
    private void Awake()
    {
        TransitionAnimator = GameObject.Find("TransitionController").GetComponent<Animator>();
    }
    public void MakeTransition()
    {
        StartCoroutine(Transition());
    }
    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            StartCoroutine(Transition());
        }
    }
    IEnumerator Transition()
    {
        TransitionAnimator.SetBool("changeAnim", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScreen");
    }
}
