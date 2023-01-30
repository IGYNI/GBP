using UnityEngine;

public class MindView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject holder;

    public void ShowMind(PlayerMinds.Entry mind)
    {
        holder.gameObject.SetActive(true);
        spriteRenderer.sprite = mind.sprite;
    }

    public void Hide()
    {
        holder.gameObject.SetActive(false);
    }
}
