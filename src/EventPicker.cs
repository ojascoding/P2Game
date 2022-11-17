using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace P2Game;

public class EventPicker
{
    //Creates an array of events to store the events from the json
    public Event[] events;
    private bool ranOnce = false;
    public EventPicker()
    {
        //Opens the json
        using (StreamReader reader = File.OpenText("Content/Events.json"))
        {
            string json = reader.ReadToEnd();
            //Takes all the values from the json and inputs them into the event array
            events = JsonConvert.DeserializeObject<Event[]>(json);
        }
    }

    public Event GenerateEvent()
    {
        Random random = new Random();
        Event randomEvent = events[random.Next(events.Length)];
        if (ranOnce)
        {
            randomEvent.description = randomEvent.description + "\n\n";
        }
        ranOnce = true;
        return randomEvent;
    }
}

public class Event
{
    public string name;
    public string description;
    public string effects;
    public int cost;
    public int pollutionCost;
    public int popularityCost;
}

