using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private int m_row;
    private int m_column;
    
    public int Row
    {
        get { return m_row; }
        set { m_row = value; }
    }

    public int Column
    {
        get { return m_column; }
        set { m_column = value; }
    }
}
