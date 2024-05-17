using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite EnterShell;
    public bool Shelled;
    public bool ShellMoving;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Shelled && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().hit();
        }
    }
    public void Entershell()
    {
        if (!Shelled)
        {
            Shelled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EntityMovement>().enabled = false;
            GetComponent<AnimationSprites>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = EnterShell;
        }
    }

}
