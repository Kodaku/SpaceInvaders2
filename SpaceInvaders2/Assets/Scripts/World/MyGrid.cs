using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject cellPrefab;
    private Cell[,] grid;
    private List<Cell> unassignedStars = new List<Cell>();

    private void Awake()
    {
        grid = new Cell[rows, columns];
        InstantiateGrid();
    }

    private void InstantiateGrid()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject cellInstance = Instantiate(cellPrefab, new Vector2(j, i), Quaternion.identity);
                cellInstance.transform.parent = this.transform;
                Cell cell = cellInstance.GetComponent<Cell>();
                cell.Row = i;
                cell.Column = j;
                grid[i, j] = cell;
                unassignedStars.Add(cell);
            }
        }
        EventHandler.CallGenerateStarsEvent();
    }

    public Cell GetStarCell()
    {
        int starIndex = Random.Range(0, unassignedStars.Count);
        //print(starIndex);
        Cell starCell = unassignedStars[starIndex];
        unassignedStars.RemoveAt(starIndex);
        return starCell;
    }

}
