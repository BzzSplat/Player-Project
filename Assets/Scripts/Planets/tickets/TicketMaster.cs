using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMaster : MonoBehaviour
{
    Queue<Ticket> ticketQueue = new Queue<Ticket>();
    public Queue<int> test = new Queue<int>();

    public void SendTicket(Ticket ticket)
    {
        ticketQueue.Enqueue(ticket); //add ticket to queue
        //Debug.Log("Ticket created from " + ticket.SourcePlanet + "\n"+ticketQueue.Count); //spam? extra tickets made, fine but might need fixing
    }

    void FixedUpdate()
    {
        if (ticketQueue.Count > 0)
        {
            Ticket ticket = ticketQueue.Peek();
            if ((ticket.SourcePlanet && ticket.OtherPlanet) && (ticket.SourcePlanet.GetComponent<Rigidbody>().mass >= ticket.OtherPlanet.GetComponent<Rigidbody>().mass))
            {
                //Debug.Log("Kaboom? " + ticket.SourcePlanet);

                Rigidbody rb = ticket.SourcePlanet.GetComponent<Rigidbody>(), rbOther = ticket.OtherPlanet.GetComponent<Rigidbody>();
                rb.mass += rbOther.mass; //add masses
                rb.velocity = (rb.mass * rb.velocity + rbOther.mass * rbOther.velocity) / (rb.mass + rbOther.mass); //(m1v1 + m2v2)/m1 + m2

                Destroy(ticket.OtherPlanet);

                float s = rb.mass / 50; //get size multiplier based off planet mass
                ticket.SourcePlanet.transform.localScale = Vector3.one * s;
            }
        }
    }
}
