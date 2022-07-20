using System;
using Comics.Controladores;
using Comics.Funciones;
using Comics.Modelos;
using Comics;
MenuGeneral();

void MenuGeneral()
{
    int opcion = -1;
    while (opcion != 0)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("0) Salir.");
        Console.WriteLine("1) Menu Categorías.");
        Console.WriteLine("2) Menu Autores.");
        Console.WriteLine("3) Menu Comics.");
        Console.WriteLine("4) Menu Editoriales.");
        Console.WriteLine("5) Menu Autores y Comics.");
        Console.WriteLine("6) Estadisticas.");
        Console.Write("Opcion: ");
        int.TryParse(Console.ReadLine(), out opcion);

        (int indice, string entidad)[] opciones = { (0, "salir"), (1, "Categoria"), (2, "Autor"), (3, "Comic"), (4, "Editorial") , (5,"Autor/Comic")};
        if (opcion == 0) break;
        foreach (var item in opciones)
        {
            if (item.indice == opcion)
            {
                MenuCRUD(item.entidad, funcionRequerida(opcion));
            }
        }       
        if (opcion == 6) MenuEstadisticas();
    }
}

void MenuCRUD(string nombreEntidad, IFunciones funciones)
{
    int opcion = -1;
    while (opcion != 0)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("0) Salir.");
        Console.WriteLine("1)Añadir " + nombreEntidad);
        Console.WriteLine("2)Mostrar " + nombreEntidad);
        Console.WriteLine("3)Modificar " + nombreEntidad);
        Console.WriteLine("4)Borrar " + nombreEntidad);
        Console.Write("Opcion: ");
        int.TryParse(Console.ReadLine(), out opcion);

        switch (opcion)
        {
            case 0:
                break;
            case 1:
                funciones.Añadir();
                break;
            case 2:
                funciones.Mostrar();
                break;
            case 3:
                funciones.Modificar();
                break;
            case 4:
                funciones.Eliminar();
                break;
            default:
                Console.WriteLine("Perdona no le he entendido.");
                break;
        }
    }
}
 
IFunciones funcionRequerida(int opcion)
{
    if (opcion == 1) return new FuncionesCategoria();
    if (opcion == 2) return new FuncionesAutor();
    if (opcion == 3) return new FuncionesComic();
    if (opcion == 4) return new FuncionesEditorial();
    return new FuncionesAutoresYComics();
}

void MenuEstadisticas()
{
    int opcion = -1;
    while (opcion != 0)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("0) Salir.");
        Console.WriteLine("1)Numero de comics por categoria.");
        Console.WriteLine("2)Numero de cómics por autor.");
        Console.WriteLine("3)Número de autores por nacionalidad.");
        Console.WriteLine("4)Los 5 cómics con mayor número de páginas.");

        Console.Write("Opcion: ");
        int.TryParse(Console.ReadLine(), out opcion);
        if (opcion == 0) break;
        if (opcion == 1) Estadisticas.NumeroComicsCategoria();
        if (opcion == 2) Estadisticas.NumeroComicsAutor();
        if (opcion == 3) Estadisticas.AutoresPorNacionalidad();
        if (opcion == 4) Estadisticas.ComicsConMayorNumeroPaginas(5);
    }
}