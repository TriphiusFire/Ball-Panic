using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]//For setting level brick / ball locations by x, y;

public class BrickScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AnimationClip clip;

    public float x, y;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10));//remove when levels done designing
    }

    public IEnumerator BreakTheBrick()
    {
        animator.Play("BrickBreak");
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
    }
}
