using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private HexGrid hexGridScript;
    private NodeManager nodeManagerScript;
    public int column;
    public int row;
    public GameObject thisHex;
    public GameObject targetHex;
    public GameObject targetHexReverse;
    public bool isMatched;

    void Start()
    {
        hexGridScript = FindObjectOfType<HexGrid>();
        nodeManagerScript= FindObjectOfType<NodeManager>();
        FindMyPos();
        thisHex = this.gameObject;
        FindMatches();
    }
    private void FixedUpdate()
    {
        if (nodeManagerScript.currentNode!=null && nodeManagerScript.access)
        {
            hexGridScript.allHexs[column, row] = this.gameObject;
            gameObject.name = "(" + column.ToString() + "," + row.ToString() + ")";
            FindNextHexs();
            FindBehindHexs();
        }
        if (nodeManagerScript.control)
        {
            hexGridScript.allHexs[column, row] = this.gameObject;
            gameObject.name = "(" + column.ToString() + "," + row.ToString() + ")";
            FindMatches();
        }
        if (isMatched)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = new Color(0f, 0f, 0f, .2f);
        } 
    }
    public void FindMyPos()
    {
        int width = hexGridScript.width;
        int height= hexGridScript.height;
        if (hexGridScript.allHexs[width - 1, height - 1] != null)
        {
            for (int x = 0; x < hexGridScript.allHexs.GetLength(0); x++)
            {
                for (int y = 0; y < hexGridScript.allHexs.GetLength(1); y++)
                {
                    if (hexGridScript.allHexs[x, y] != null)
                    {
                        if (hexGridScript.allHexs[x, y].name == gameObject.name)
                        {
                            column = x;
                            row = y;
                        }
                    }
                }
            }
        }
    }
    public void FindNextHexs()
    {
        GameObject currentnode = nodeManagerScript.currentNode;
            if (thisHex == currentnode.GetComponent<Node>().nodeChilds[0])
            {
                targetHex = currentnode.GetComponent<Node>().nodeChilds[1];
            }
            else if (thisHex == currentnode.GetComponent<Node>().nodeChilds[1])
            {
                targetHex = currentnode.GetComponent<Node>().nodeChilds[2];
            }
            else if (thisHex == currentnode.GetComponent<Node>().nodeChilds[2])
            {
                targetHex = currentnode.GetComponent<Node>().nodeChilds[0];
            }
    }
    public void FindBehindHexs()
    {
        GameObject currentnode = nodeManagerScript.currentNode;
        if (thisHex == currentnode.GetComponent<Node>().nodeChilds[0])
        {
            targetHexReverse = currentnode.GetComponent<Node>().nodeChilds[2];
        }
        else if (thisHex == currentnode.GetComponent<Node>().nodeChilds[1])
        {
            targetHexReverse = currentnode.GetComponent<Node>().nodeChilds[0];
        }
        else if (thisHex == currentnode.GetComponent<Node>().nodeChilds[2])
        {
            targetHexReverse = currentnode.GetComponent<Node>().nodeChilds[1];
        }
    }
    private void FindMatches()
    {
            GameObject neighborHex1;
            GameObject neighborHex2;
            GameObject neighborHex3;
            GameObject neighborHex4;
            GameObject neighborHex5;
            GameObject neighborHex6;

            if (column % 2 == 0)//yüksek sütun
            {
                if (column == 0 && row == 0)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row + 1] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row + 1];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column == 0 && row < hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row + 1] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row + 1];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row -1];
                else
                    neighborHex4 = this.gameObject;
                    if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column == 0 && row == hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                    if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column > 0 && row == 0)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row + 1] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row + 1];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row + 1] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row + 1];
                else
                    neighborHex6 = this.gameObject;
                    if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }

                }
                else if (column > 0 && row < hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row + 1] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row + 1];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row + 1] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row + 1];
                else
                    neighborHex6 = this.gameObject;
                    if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && this.gameObject.tag == neighborHex6.tag)
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6) && checkMatch(this.gameObject, neighborHex1))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex6) && checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column > 0 && row == hexGridScript.height - 1)
                {
                
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex5 = this.gameObject;
                
                if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
            }
            else //alçak sütun
            {
                if (column < hexGridScript.width - 1 && row == 0)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column == hexGridScript.width - 1 && row == 0)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column == hexGridScript.width - 1 && row == hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row - 1] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row - 1];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column == hexGridScript.width - 1 && row > hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row - 1] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row - 1];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column > 0 && row == hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row - 1] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row - 1];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row - 1] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row - 1];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex6) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (this.gameObject.tag == neighborHex5.tag && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                }
                else if (column < hexGridScript.width - 1 && row < hexGridScript.height - 1)
                {
                if (hexGridScript.allHexs[column, row + 1] != null)
                    neighborHex1 = hexGridScript.allHexs[column, row + 1];
                else
                    neighborHex1 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row] != null)
                    neighborHex2 = hexGridScript.allHexs[column + 1, row];
                else
                    neighborHex2 = this.gameObject;
                if (hexGridScript.allHexs[column + 1, row - 1] != null)
                    neighborHex3 = hexGridScript.allHexs[column + 1, row - 1];
                else
                    neighborHex3 = this.gameObject;
                if (hexGridScript.allHexs[column, row - 1] != null)
                    neighborHex4 = hexGridScript.allHexs[column, row - 1];
                else
                    neighborHex4 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row - 1] != null)
                    neighborHex5 = hexGridScript.allHexs[column - 1, row - 1];
                else
                    neighborHex5 = this.gameObject;
                if (hexGridScript.allHexs[column - 1, row] != null)
                    neighborHex6 = hexGridScript.allHexs[column - 1, row];
                else
                    neighborHex6 = this.gameObject;
                    if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6) && checkMatch(this.gameObject, neighborHex1))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex6) && checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex1) && checkMatch(this.gameObject, neighborHex2))
                    {
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex2) && checkMatch(this.gameObject, neighborHex3))
                    {
                        neighborHex2.GetComponent<Hex>().isMatched = true;
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex3) && checkMatch(this.gameObject, neighborHex4))
                    {
                        neighborHex3.GetComponent<Hex>().isMatched = true;
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex4) && checkMatch(this.gameObject, neighborHex5))
                    {
                        neighborHex4.GetComponent<Hex>().isMatched = true;
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject, neighborHex5) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex5.GetComponent<Hex>().isMatched = true;
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                    else if (checkMatch(this.gameObject,neighborHex1) && checkMatch(this.gameObject, neighborHex6))
                    {
                        neighborHex6.GetComponent<Hex>().isMatched = true;
                        neighborHex1.GetComponent<Hex>().isMatched = true;
                        isMatched = true;
                    }
                
            }
        }
    }
   private bool checkMatch(GameObject me,GameObject target) 
    {
        if (target != null && me != target)
        {
            if (me.tag == target.tag)
            {
                return true;
            }
            else return false;
        }
        return false;
    }

}
