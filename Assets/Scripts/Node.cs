using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private HexGrid hexgrid;
    private NodeManager nodeManagerScript;
    public List<GameObject> nodeChilds = new List<GameObject>();
    public int column;
    public int row;
    GameObject nodechild;

    void Start()
    {
        nodeManagerScript = FindObjectOfType<NodeManager>();
        hexgrid = FindObjectOfType<HexGrid>();
        FindMyPos();
        FindChilds();      
    }
    private void OnMouseDown()
    {
        nodeManagerScript.Clicked(this.gameObject);
        //Debug.Log("tıklandı");
    }
    public void FindChilds()
    {
        nodeChilds.Clear();
        if (column%2==0 && row % 2 == 1)
        {
            for (int i = 0; i < 3; i++)
            {     
               for (int x = 0; x < hexgrid.allNodes.GetLength(0); x++)
               {
                  for (int y = 0; y < hexgrid.allNodes.GetLength(1); y++)
                  {
                        if (column == x && row == y)
                        {
                            if (i == 0)
                            {
                                nodechild = hexgrid.allHexs[x, (y + 1) / 2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 1)
                            {
                                nodechild = hexgrid.allHexs[x + 1, (y + 1) / 2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 2)
                            {
                                nodechild = hexgrid.allHexs[x, (y - 1) / 2];
                                nodeChilds.Add(nodechild);              
                            }
                        }
                    }
               }
            }
        }
        else if (column % 2 == 1 && row % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < hexgrid.allNodes.GetLength(0); x++)
                {
                    for (int y = 0; y < hexgrid.allNodes.GetLength(1); y++)
                    {
                        if (column == x && row == y)
                        {
                            if (i == 0)
                            {
                                nodechild = hexgrid.allHexs[x, (y/2)+1];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 1)
                            {
                                nodechild = hexgrid.allHexs[x + 1, y / 2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 2)
                            {
                                nodechild = hexgrid.allHexs[x, y / 2];
                                nodeChilds.Add(nodechild);
                            }
                        }
                    }
                }
            }
        }
        else if (column % 2 == 1 && row % 2 == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < hexgrid.allNodes.GetLength(0); x++)
                {
                    for (int y = 0; y < hexgrid.allNodes.GetLength(1); y++)
                    {
                        if (column == x && row == y)
                        {
                            if (i == 0)
                            {
                                nodechild = hexgrid.allHexs[x, (y + 1) / 2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 1)
                            {
                                nodechild = hexgrid.allHexs[x+1, (y + 1) / 2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 2)
                            {
                                nodechild = hexgrid.allHexs[x + 1, (y - 1) / 2];
                                nodeChilds.Add(nodechild);
                            }
                        }
                    }
                }
            }
        }
        else if (column % 2 == 0 && row % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < hexgrid.allNodes.GetLength(0); x++)
                {
                    for (int y = 0; y < hexgrid.allNodes.GetLength(1); y++)
                    {
                        if (column == x && row == y)
                        {
                            if (i == 0)
                            {
                                nodechild = hexgrid.allHexs[x, y/2];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 1)
                            {
                                nodechild = hexgrid.allHexs[x+1, (y/2)+1];
                                nodeChilds.Add(nodechild);
                            }
                            else if (i == 2)
                            {
                                nodechild = hexgrid.allHexs[x + 1, y/2];
                                nodeChilds.Add(nodechild);
                            }
                        }
                    }
                }
            }
        }
    }
    private void FindMyPos()
    {
        for (int x = 0; x < hexgrid.allNodes.GetLength(0); x++)
        {
            for (int y = 0; y < hexgrid.allNodes.GetLength(1); y++)
            {
                if (hexgrid.allNodes[x, y].name == gameObject.name)
                {
                    column = x;
                    row = y;
                }
                    
            }
        }
    }
    
}
