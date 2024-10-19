using System;
using agencia.Interfaces;

namespace agencia.Classes;

public class Travel : ITravel
{
    private string _destination;
    private double _price;
    private DateTime _date;
    
    public string Destination { get => _destination; set => _destination = value; }
    public double Price { get => _price; set => _price = value; }
    public DateTime Date { get => _date; set => _date = value; }
    
}