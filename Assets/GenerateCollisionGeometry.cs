using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class GenerateCollisionGeometry : MonoBehaviour
{
    [SerializeField]
    private GameObject playArea = null;
    private Sprite pas = null;

    PolygonCollider2D pc;

    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
        pc.pathCount = 2;

        if (playArea != null) {
            pas = playArea.GetComponent<SpriteRenderer>().sprite;
        }

        updateGeometry();
    }

    void Update()
    {
        updateGeometry();
    }

    private void updateGeometry() {
        float x = playArea.transform.position.x;
        float y = playArea.transform.position.y; 

        float w = pas.bounds.extents.x * playArea.transform.localScale.x;
        float h = pas.bounds.extents.y * playArea.transform.localScale.y;

        Vector2[] playAreaVertices = new Vector2[]
        {
            new Vector2(x - w, y + h),
            new Vector2(x + w, y + h),
            new Vector2(x + w, y - h),
            new Vector2(x - w, y - h),
        };

        pc.SetPath(1, playAreaVertices);
    }
}
