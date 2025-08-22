using UnityEngine;
using System;

public class PlayerDemonController : MonoBehaviour
{

    public DemonData playerTemplate;
    public FactionData noFactionTemplate;
    public DemonState player;

    void Start()
    {
        player = new DemonState(playerTemplate);
        player.faction = new FactionState(noFactionTemplate);
    }
}
