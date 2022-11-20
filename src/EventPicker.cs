using System;
using System.IO;
using Newtonsoft.Json;

namespace P2Game;

public class EventPicker
{
    //Creates an array of events to store the events from the json
    public Event[] events;
    private bool ranOnce;
    public EventPicker()
    {
        //Opens the json
        using (StreamReader reader = File.OpenText("Content/Events.json"))
        {
            string json = reader.ReadToEnd();
            //Takes all the values from the json and inputs them into the event array
            events = JsonConvert.DeserializeObject<Event[]>(json);

            foreach (Event _event in events)
            {
                _event.description += "\n\n";
            }
        }
    }
    
    //Picks a random event from the list of events
    public Event GenerateEvent()
    {
        Random random = new Random();
        Event randomEvent = events[random.Next(events.Length)];
        // if (ranOnce)
        // {
        //     randomEvent.description = randomEvent.description;
        // }
        // ranOnce = true;
        return randomEvent;
    }
}

//A class created to store events and all their values
public class Event
{
    public string name;
    public string description;
    public string effects;
    public int[] cost;
    public int[] pollutionCost;
    public int[] popularityCost;
}

