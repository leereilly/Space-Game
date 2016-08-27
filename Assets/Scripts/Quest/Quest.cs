using UnityEngine;
using System.Collections;
using System;

public class SteveQuest : IQuest {
    private string _description;
    private int _questId;
    private object _objectToRunInto;

    public SteveQuest (int id)
    {
        _questId = id;
        // ideally, we'll set the other private variables based on the passed in ID
    }

    public string Description
    {
        get
        {
            return "Quest ID: " + _questId.ToString() + _description;
        }
    }

    public int QuestID
    {
        get
        {
            return _questId;
        }
    }

    public void Complete()
    {
        _description = "Hey, you finished it!";
    }

    public void Fail()
    {
        _description = "Haha!";
    }

    public object QuestItemToRunInto
    {
        get
        {
            return _objectToRunInto;
        }
    }
}
