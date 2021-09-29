using System;
using System.Collections.Generic;

class DaySixteen
{
    public void Run()
    {
        List<string> strList = Input.GetStrings("input_files/Day16.txt");
        List<List<string>> splitLists = SplitLists(strList);
        List<Field> fieldList = MakeFields(splitLists[0]);
        List<Ticket> ticketList = MakeTickets(splitLists[2]);
        DiscardInvalidTickets(fieldList, ticketList);
        FigureOutFields(fieldList, ticketList);

    }

    private List<List<string>> SplitLists(List<string> strList)
    {
        strList.Add("");
        List<List<string>> lists = new();
        List<string> list = new();

        for(int i = 0; i < strList.Count; i++)
        {
            if (String.IsNullOrEmpty(strList[i]))
            {
                lists.Add(list);
                list = new();
            } else
            {
                list.Add(strList[i]);
            }
        }
        return lists;
    }

    private List<Field> MakeFields(List<string> strList)
    {
        List<Field> fieldList = new();
        foreach(string str in strList)
        {
            fieldList.Add(new Field(str));
        }
        return fieldList;
    }

    private List<Ticket> MakeTickets(List<string> infoList)
    {
        List<Ticket> ticketList = new();

        infoList.RemoveAt(0);

        foreach(string info in infoList)
        {
            ticketList.Add(new Ticket(info));
        }
        return ticketList;
    }

    private void DiscardInvalidTickets(List<Field> fieldList, List<Ticket> ticketList)
    {
        List<Ticket> invalidTickets = FindInvalidTickets(fieldList, ticketList);
        RemoveInvalidTicketsFromList(ticketList, invalidTickets);
    }

    private List<Ticket> FindInvalidTickets(List<Field> fieldList, List<Ticket> ticketList)
    {
        List<Ticket> invalidTickets = new();
        foreach (Ticket ticket in ticketList)
        {
            if (!CheckTicketValidity(fieldList, ticket))
            {
                invalidTickets.Add(ticket);
            }
        }
        return invalidTickets;
    }

    private bool CheckTicketValidity(List<Field> fieldList, Ticket ticket)
    {
        foreach(int value in ticket.values)
        {
            bool valid = false;
            foreach(Field field in fieldList)
            {
                if (field.IsValid(value)) valid = true;
            }
            if (!valid) return false;
        }
        return true;
    }
    private void RemoveInvalidTicketsFromList(List<Ticket> ticketList, List<Ticket> invalidTickets)
    {
        foreach(Ticket invalidTicket in invalidTickets)
        {
            ticketList.Remove(invalidTicket);
        }
    }

    private void FigureOutFields(List<Field> fieldList, List<Ticket> ticketList)
    {
        Field[] solvedFields = new Field[fieldList.Count];
        List<List<int>> ticketValuesByPosition = GetTicketValuesByPosition(ticketList);


    }

    private int CountValidPositions(Field field, List<List<int>> ticketValuesByPosition)
    {
        int count = 0;
        foreach(List<int> position in ticketValuesByPosition)
        {
            if (CheckValuesConformToField(field, position)) count++;
        }
        return count;
    }

    private bool CheckValuesConformToField(Field field, List<int> values)
    {
        foreach(int value in values)
        {
            if (!field.IsValid(value)) return false;
        }
        return true;
    }

    private List<List<int>> GetTicketValuesByPosition(List<Ticket> ticketList)
    {
        List<List<int>> ticketValuesByPosition = new();
        for(int i = 0; i < ticketList[0].values.Count; i++)
        {
            ticketValuesByPosition.Add(new List<int>());
            foreach(Ticket ticket in ticketList)
            {
                ticketValuesByPosition[i].Add(ticket.values[i]);
            }
        }
        return ticketValuesByPosition;
    }
}

class Field
{
    string name;
    int lowerOne;
    int higherOne;
    int lowerTwo;
    int higherTwo;

    public Field(string info)
    {
        string[] parts = info.Split(": ");
        name = parts[0];
        parts = parts[1].Split(" or ");

        string[] bounds = parts[0].Split("-");
        lowerOne = Int32.Parse(bounds[0]);
        higherOne = Int32.Parse(bounds[1]);

        bounds = parts[1].Split("-");
        lowerTwo = Int32.Parse(bounds[0]);
        higherTwo = Int32.Parse(bounds[1]);
    }

    public bool IsValid(int value)
    {
        return (value >= lowerOne
            && value <= higherOne)
            ||
            (value >= lowerTwo
            && value <= higherTwo);
    }

    public override string ToString()
    {
        return $"{name}: {lowerOne}-{higherOne} or {lowerTwo}-{higherTwo}";
    }
}

class Ticket
{
    public List<int> values = new();

    public Ticket(string info)
    {
        string[] parts = info.Split(",");
        foreach(string str in parts)
        {
            values.Add(Int32.Parse(str));
        }
    }

    public override string ToString()
    {
        return String.Join(',', values);
    }
}