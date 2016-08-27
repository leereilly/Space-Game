using UnityEngine;
using System.Collections;

public interface IQuest {

    int QuestID { get; }

    string Description { get; }

    void Complete();

    void Fail();
}
