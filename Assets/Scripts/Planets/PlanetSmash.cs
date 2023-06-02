using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSmash : MonoBehaviour
{
    [SerializeField]
    TicketMaster tickMast;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name);
        tickMast.SendTicket(new Ticket(gameObject, collision.gameObject));
    }
}
