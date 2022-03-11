using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_trigger : MonoBehaviour
{
    public Text tips;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "T1")
        {
            tips.text = "Congrats on your first movement! \n" +
                "Move a little more forward....";
        }

        else if (other.tag == "T2")
        {
            tips.text = "Now try to move the boxes in front of you by using your hands.";
        }

        else if (other.tag == "T3")
        {
            tips.text = "When you are ready, pass through the door.";
        }

        else if (other.tag == "T4")
        {
            tips.text = "Congratulations!! " +
                "New scene is loading....";

            SceneManager.LoadScene("Feb-06");
        }
    }
}
