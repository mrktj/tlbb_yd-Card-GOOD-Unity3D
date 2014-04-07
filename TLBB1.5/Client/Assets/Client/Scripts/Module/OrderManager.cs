using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Games.CharacterLogic;
using System;

public class OrderManager
{
    public enum OrderState
    {
        STATE_UNKNOWN,
        STATE_SUCCESS,
        STATE_FAIL,
    }
    private static OrderManager m_instance = null;
    public static OrderManager Instance()
    {
        if (null == m_instance)
        {
            m_instance = new OrderManager();
            m_instance.LoadData();
        }
        m_instance.FreshDataList();
        return m_instance;
    }

    private List<GlobalSave.SOrder> m_orderList = new List<GlobalSave.SOrder>();

    public List<GlobalSave.SOrder> OrderList()
    {
        return m_orderList;
    }

    public void FreshDataList()
    {
        if (m_curLoadedPath != OrderPath())
        {
            LoadData();
        }
    }
	
	public bool HaveOrder(string orderID){
        for (int i = 0; i < m_orderList.Count; i++)
        {
            if (orderID == m_orderList[i].strOder)
            {
                return true;
            }
        }
		
		return false;
	}
	
    public void AddOrder(GlobalSave.SOrder orderData)
    {
        GlobalSave.SOrder newOrder = new GlobalSave.SOrder();
        newOrder.Copy(orderData);
        for (int i = 0; i < m_orderList.Count; i++)
        {
            if (newOrder.strOder == m_orderList[i].strOder)
            {
                return;
            }
        }
        newOrder.date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        newOrder.channel = DeviceHelper.GetChannelID();
        newOrder.state = 0;
        m_orderList.Insert(0, newOrder);

        if (m_orderList.Count > 100)
        {
            m_orderList.RemoveRange(100, m_orderList.Count - 100);
        }

        SaveOrder();
    }

    public void SaveOrder()
    {
        if (m_orderList.Count == 0)
        {
            return;
        }

        FileStream fs = new FileStream(OrderPath(), FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);

        foreach (GlobalSave.SOrder order in m_orderList)
        {
            sw.WriteLine(order.ToString());
        }

        sw.Close();
    }

    public bool SetOrderState(string orderID, OrderState newState)
    {
        if (m_orderList.Count == 0)
        {
            return false;
        }
        foreach (GlobalSave.SOrder curOrder in m_orderList)
        {
            if (curOrder.strOder == orderID)
            {
                if (curOrder.state != (int)newState)
                {
                    curOrder.state = (int)newState;
                    SaveOrder();
                }

                return true;
            }
        }

        return false;
    }

    public string GenerateOrderString(int index)
    {
        return (Obj_MyselfPlayer.GetMe().accountID + index.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss"));
    }
    private string OrderPath()
    {

        return Application.persistentDataPath + "/" + Obj_MyselfPlayer.GetMe().accountID + DeviceHelper.GetChannelID() + ".data";
    }

    private string m_curLoadedPath = "";
    private void LoadData()
    {
        m_curLoadedPath = OrderPath();
        m_orderList.Clear();

        string strLine;
        if (!File.Exists(OrderPath()))
        {
            return;
        }
        FileStream aFile = new FileStream(OrderPath(), FileMode.Open);
        StreamReader sr = new StreamReader(aFile);
        strLine = sr.ReadLine();

        while (strLine != null)
        {
            GlobalSave.SOrder newOrder = new GlobalSave.SOrder();
            if (newOrder.FromString(strLine))
            {
                m_orderList.Add(newOrder);
            }
            strLine = sr.ReadLine();
        }
        sr.Close();




    }


}
