using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRender smallRenderer;
    public PlayerSpriteRender bigRenderer;
    private DeathAnimation dan;
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool death => dan.enabled;
    public void Awake()
    {
        dan = GetComponent<DeathAnimation>();
    }
    public void hit()
    {
        if (big)
        {

            Shrink();
        }

        else
        {
            Death();
        }
    }
    private void Shrink()
    {

    }
    private void Death()
    {
        smallRenderer.enabled = false;  
        bigRenderer.enabled = false;
        dan.enabled = true;
        GameManager.instance.ResetLevel(3f);
    }
}
