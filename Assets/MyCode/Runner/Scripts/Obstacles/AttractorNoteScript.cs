using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorNoteScript : MonoBehaviour
{
  public float AttractorSpeed;
  private float noteSpeed = -1f;
  private Rigidbody2D rigidBody;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
          transform.position = Vector3.MoveTowards(transform.position, other.transform.position, AttractorSpeed * Time.deltaTime);
          Debug.Log("hit player");
        }
    }
    void Awake ()
  	{
  		// get the rigidbody of the obstacle
  		rigidBody = GetComponent<Rigidbody2D>();
  	}
    // Update is called once per frame
  	void Update () {

  		// move the object to the left
      if(FindObjectOfType<BGLooper>().scrollingEnable == true)
      rigidBody.velocity = new Vector2(noteSpeed, 0f);
      else
      rigidBody.velocity = new Vector2(0f, 0f);
      
      if(transform.childCount < 1)
      {
        this.gameObject.SetActive(false);
        Debug.Log("Note Destroy");
      }
  	}
}
