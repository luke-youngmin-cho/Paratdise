using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// CellualarAutomata 알고리즘 계산
/// </summary>
public class CellularAutomata
{
    private static int[,] field;
    public static int[,] Calc(int[,] map, int algoNum)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        field = map;
        RuleSet gameOfLife = new ConwaysGameOfLife(field, width, height);
        for (int i = 0; i < algoNum; i++)
        {
            gameOfLife.GetNewState();
        }
        return field;
    }
}

public abstract class RuleSet
{
    protected int _maxX = 0;
    protected int _maxY = 0;
    protected int[,] _field;

    public RuleSet(int[,] field, int maxX, int maxY)
    {
        _field = field;
        _maxX = maxX;
        _maxY = maxY;
    }

    protected int GetNumberOfNeighbors(int x, int y)
    {

        int neighbors = 0;
        int xPos;
        int yPos;
        for (xPos = x - 1; xPos <= x + 1; xPos++)
        {
            for (yPos = y - 1; yPos <= y + 1; yPos++)
            {
                if (xPos == x && yPos == y) continue;

                if (xPos < _maxX && xPos >= 0 && yPos < _maxY && yPos >= 0 & _field[xPos, yPos] == 1)
                {
                    neighbors++;
                }

            }
        }

        return neighbors;
    }

    public void GetNewState()
    {
        int[,] field2 = NewStateAlgorithm();
        Array.Copy(field2, _field, field2.Length);
    }

    protected abstract int[,] NewStateAlgorithm();
}

public class ConwaysGameOfLife : RuleSet
{
    public ConwaysGameOfLife(int[,] field, int maxX, int maxY)
         : base(field, maxX, maxY)
    {

    }

    protected override int[,] NewStateAlgorithm()
    {
        int[,] field2 = new int[_maxX, _maxY];

        for (int y = 1; y < _maxY - 1; y++)
        {
            for (int x = 1; x < _maxX - 1; x++)
            {
                int neighbors = GetNumberOfNeighbors(x, y);
                if (neighbors == 3)
                {
                    // cell is born.  
                    field2[x, y] = 1;
                    continue;
                }

                if (neighbors == 2 || neighbors == 3)
                {
                    // cell continues.  
                    field2[x, y] = _field[x, y];
                    continue;
                }

                // cell dies.  
                field2[x, y] = 0;
            }
        }

        return field2;
    }
}

