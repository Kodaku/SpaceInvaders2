using UnityEngine;

public class StarsMaker : MonoBehaviour
{
    public GameObject[] stars;
    public int totalStars;
    private int starCount;
    private int maxStars;
    private MyGrid myGrid;

    private void Awake()
    {
        myGrid = GetComponent<MyGrid>();
        maxStars = stars.Length;
        starCount = 0;
        EventHandler.GenerateStarsEvent += MakeStars;
    }

    private void MakeStars()
    {
        for(int i = 0; i < totalStars; i++)
        {
            Cell starCell = myGrid.GetStarCell();
            float x = Mathf.Clamp(starCell.Column + Random.Range(0.0f, 1.0f), 0.0f, myGrid.columns - 1);
            float y = Mathf.Clamp(starCell.Row + Random.Range(0.0f, 1.0f), 0.0f, myGrid.rows - 1);
            Instantiate(stars[starCount], new Vector2(x, y), Quaternion.identity).transform.parent = this.transform;
            starCount = (starCount + 1) % maxStars;
        }
    }    
}
