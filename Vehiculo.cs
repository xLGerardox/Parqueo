using System;

public abstract class Vehiculo
{
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Color { get; set; }
    public DateTime HoraEntrada { get; set; }

    public Vehiculo(string placa, string marca, string modelo, string color)
    {
        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        Color = color;
        HoraEntrada = DateTime.Now;
    }

    public abstract double CalcularCostoEstacionamiento(TimeSpan tiempoEstacionado);
}
