using System;

namespace agencia.Interfaces;

public interface ITravel
{
    string Destination { get; set; }
    double Price { get; set; }
    DateTime Date { get; set; }
}