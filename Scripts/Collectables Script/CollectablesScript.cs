using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesScript : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if(this.gameObject.tag != "InGameCollectable")
        {
            Invoke("DeactivateGameObject", 10);
        }
    }

    void DeactivateGameObject()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BottomBrick")
        {
            //myRigidbody.isKinematic = true;
           
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = new Vector2(0, 0);
            Vector3 temp = target.transform.position;
            temp.y += 0.5f;
            transform.position = new Vector2(transform.position.x, temp.y);

        }

        if(target.tag == "Player")
        {
            if(this.gameObject.tag == "InGameCollectable")
            {
                GameController.instance.collectedItems[GameController.instance.currentLevel] = true;
                GameController.instance.Save();

                if(GameplayController.instance!= null)
                {
                    if (GameController.instance.currentLevel == 0)
                    {
                        GameplayController.instance.playerScore += 1000;
                    }
                    else
                    {
                        GameplayController.instance.playerScore += GameController.instance.currentLevel*1000;
                    }
                }

                GameplayController.instance.levelFinishedPanel.SetActive(true);
                GameplayController.instance.levelInProgress = false;
                PlayerScript.instance.StopMoving();
                Time.timeScale = 0;
                GameplayController.instance.showScoreAtTheEndOfLevelText.text = "" + GameplayController.instance.playerScore;
            }

            this.gameObject.SetActive(false);
        }
    }


}
