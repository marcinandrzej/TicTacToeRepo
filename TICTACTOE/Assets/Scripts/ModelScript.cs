using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript
{
    private int[] grid;

    public ModelScript()
    {
        grid = new int[9];
        ResestGrid();
    }

    public void ResestGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            grid[i] = 0;
        }
    }

    public void FillField(int player, int fieldIndex)
    {
        grid[fieldIndex] = player;
    }

    public int CheckWin()
    {
        int win = 0;
        if (grid[0]==grid[1] && grid[1]==grid[2] && grid[2] !=0)
        {
            win = grid[2];
        }
        else if (grid[3] == grid[4] && grid[4] == grid[5] && grid[5] != 0)
        {
            win = grid[5];
        }
        else if (grid[6] == grid[7] && grid[7] == grid[8] && grid[8] != 0)
        {
            win = grid[8];
        }
        else if (grid[0] == grid[3] && grid[3] == grid[6] && grid[6] != 0)
        {
            win = grid[6];
        }
        else if (grid[1] == grid[4] && grid[4] == grid[7] && grid[7] != 0)
        {
            win = grid[7];
        }
        else if (grid[2] == grid[5] && grid[5] == grid[8] && grid[8] != 0)
        {
            win = grid[8];
        }
        else if (grid[0] == grid[4] && grid[4] == grid[8] && grid[8] != 0)
        {
            win = grid[8];
        }
        else if (grid[2] == grid[4] && grid[4] == grid[6] && grid[6] != 0)
        {
            win = grid[6];
        }
        else if (grid[0] != 0 && grid[1] != 0 && grid[2] != 0 && grid[3] != 0 && grid[4] != 0 && grid[5] != 0 &&
            grid[6] != 0 && grid[7] != 0 && grid[8] != 0)
        {
            win = 2;
        }
        return win;
    }

    public bool IsFieldFree(int index)
    {
        if (grid[index] == 0)
            return true;
        return false;
    }
}
