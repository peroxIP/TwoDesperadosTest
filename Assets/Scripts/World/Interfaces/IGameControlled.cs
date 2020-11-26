using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameControlled
{
    void SetGameController(GameController controller);
    void SubscribeToController();
    void CustomStart();
}
