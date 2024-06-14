using UnityEngine;

namespace Scripts.Arkanoid
{
    public class VausSpriteUpdater: MonoBehaviour, IUpdateVaus
    {
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D vausCollider;
        [SerializeField] private Sprite[] sprites;

        public void UpdateVaus(VausState vausState)
        {
            spriteRenderer.sprite = sprites[(int)vausState]; // Updates Sprite
            vausCollider.size = spriteRenderer.sprite.bounds.size; // Updates collider
        }
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            vausCollider = GetComponent<BoxCollider2D>();
        }
    }

}
