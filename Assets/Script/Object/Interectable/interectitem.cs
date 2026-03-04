using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class interectitem :  MonoBehaviour, Iinterectable
    {

    public  bool isStuck { get; }

    public virtual void Oninterect() { };
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null) player.SetInteractable(this);
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null) player.ClearInteractable();
    }
}

