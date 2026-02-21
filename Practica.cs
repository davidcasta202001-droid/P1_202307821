using System;
using System.Collections.Generic;

public abstract class MaterialBiblioteca
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Codigo { get; private set; }
    public string Estado { get; private set; }

    public MaterialBiblioteca(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
        Codigo = GenerarCodigo();
        Estado = "Disponible";
    }

    private string GenerarCodigo()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var codigo = new char[8];
        for (int i = 0; i < codigo.Length; i++)
        {
            codigo[i] = chars[random.Next(chars.Length)];
        }
        return new string(codigo);
    }

    public virtual void Prestar(int dias) 
    {
        if (Estado == "Disponible")
        {
            Estado = $"Prestado ({dias} días máx.)";
            Console.WriteLine($" {Titulo} ha sido prestado por {dias} días.");
        }
        else
        {
            Console.WriteLine($" {Titulo} ya está prestado.");
        }
    }

    public void Devolver()
    {
        if (Estado.StartsWith("Prestado"))
        {
            Estado = "Disponible";
            Console.WriteLine($" {Titulo} ha sido devuelto.");
        }
        else
        {
            Console.WriteLine($" {Titulo} no estaba prestado.");
        }
    }

    public virtual void MostrarInfo()
    {
        Console.WriteLine($" Título: {Titulo}");
        Console.WriteLine($" Autor: {Autor}");
        Console.WriteLine($" Código: {Codigo}");
        Console.WriteLine($" Estado: {Estado}");
    }
}

public class LibroFisico : MaterialBiblioteca
{
    public string Ejemplar { get; set; }

    public LibroFisico(string titulo, string autor, string ejemplar)
        : base(titulo, autor)
    {
        Ejemplar = ejemplar;
    }

    public override void Prestar(int diasMax = 7)
    {
        base.Prestar(7);
    }

    public override void MostrarInfo()
    {
        base.MostrarInfo();
        Console.WriteLine($" Ejemplar: {Ejemplar}");
    }
}

public class LibroDigital : MaterialBiblioteca
{
    public string TamanoMB { get; set; }

    public LibroDigital(string titulo, string autor, string tamanoMB)
        : base(titulo, autor)
    {
        TamanoMB = tamanoMB;
    }

    public override void Prestar(int diasMax = 3)
    {
        base.Prestar(3);
    }

    public override void MostrarInfo()
    {
        base.MostrarInfo();
        Console.WriteLine($" Tamaño: {TamanoMB} MB");
    }
}

class Program
{
    static void Main()
    {
        List<MaterialBiblioteca> materiales = new List<MaterialBiblioteca>();

        while (true)
        {
            Console.WriteLine("\n--- Biblioteca Universitaria ---");
            Console.WriteLine("1. Registrar Libro Físico");
            Console.WriteLine("2. Registrar Libro Digital");
            Console.WriteLine("3. Prestar Material");
            Console.WriteLine("4. Devolver Material");
            Console.WriteLine("5. Consultar Información");
            Console.WriteLine("6. Salir");

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();
            if(opcion == "1")
            {
                Console.Write("Título: ");
                string tituloF = Console.ReadLine();
                Console.Write("Autor: ");
                string autorF = Console.ReadLine();
                Console.Write("Número de ejemplar: ");
                string ejemplar = Console.ReadLine();
                var libro = new LibroFisico(tituloF, autorF, ejemplar);
                materiales.Add(libro);
                Console.WriteLine($" Libro físico registrado con código: {libro.Codigo}");
            }
            else if(opcion == "2")
            {
                Console.Write("Título: ");
                string tituloD = Console.ReadLine();
                Console.Write("Autor: ");
                string autorD = Console.ReadLine();
                Console.Write("Tamaño del archivo (MB): ");
                string tamano = Console.ReadLine();
                var librod = new LibroDigital(tituloD, autorD, tamano);
                materiales.Add(librod);
                Console.WriteLine($" Libro digital registrado con codigo: {librod.Codigo}");
            }
            else if(opcion == "3")
            {
                Console.Write("Ingrese código del material: ");
                string codigoP = Console.ReadLine();
                var materialP = materiales.Find(m => m.Codigo == codigoP);
                if (materialP != null) materialP.Prestar(0);
                else Console.WriteLine(" Material no encontrado.");
            }
            else if(opcion == "4")
            {
                Console.Write("Ingrese código del material: ");
                    string codigoD = Console.ReadLine();
                    var materialD = materiales.Find(m => m.Codigo == codigoD);
                    if (materialD != null) materialD.Devolver();
                    else Console.WriteLine(" Material no encontrado.");
            }
            else if (opcion == "5")
            {
                Console.Write("Ingrese código del material: ");
                    string codigoI = Console.ReadLine();
                    var materialI = materiales.Find(m => m.Codigo == codigoI);
                    if (materialI != null) materialI.MostrarInfo();
                    else Console.WriteLine(" Material no encontrado.");
            } else if (opcion == "6")
            {
                Console.WriteLine(" Saliendo del sistema...");
                Environment.Exit(0);
            } else
            {
                Console.WriteLine(" Opción inválida.");
            }
        }
    }
}