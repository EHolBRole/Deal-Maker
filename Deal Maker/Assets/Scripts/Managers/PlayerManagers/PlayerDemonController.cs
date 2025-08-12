using UnityEngine;
using System;

public class PlayerDemonController : MonoBehaviour
{

    public DemonData player_template;
    public DemonState player;

    void Start()
    {
        player = new DemonState(player_template);
    }
}
