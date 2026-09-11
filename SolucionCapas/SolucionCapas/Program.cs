using System;
using System.Collections.Generic;
using SolucionCapas.Negocio;
using SolucionCapas.Datos;

namespace SolucionCapas.Presentacion
{
    public class Program
    {
        public static void Main()
        {
            PersonaNegocio negocio = new PersonaNegocio();
            int opcion = 0;

            do
            {
                Console.WriteLine("MENU AGENDA");
                Console.WriteLine("1. Buscar por DNI");
                Console.WriteLine("2. Buscar por Apellido");
                Console.WriteLine("3. Buscar por Nombres");
                Console.WriteLine("4. Buscar por Calle");
                Console.WriteLine("5. Agregar persona");
                Console.WriteLine("6. Modificar persona");
                Console.WriteLine("7. Eliminar persona");
                Console.WriteLine("8. Salir");
                Console.Write("Opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción no válida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese DNI: ");
                        string dni = Console.ReadLine();
                        Persona p = negocio.ObtenerPorDni(dni);
                        CuentaCte c = negocio.ObtenerCuentaPorDni(dni);
                        MostrarPersonaYCuenta(p, c);
                        break;

                    case 2:
                        Console.Write("Ingrese Apellido: ");
                        MostrarLista(negocio.BuscarPorApellido(Console.ReadLine()));
                        break;

                    case 3:
                        Console.Write("Ingrese Nombres: ");
                        MostrarLista(negocio.BuscarPorNombres(Console.ReadLine()));
                        break;

                    case 4:
                        Console.Write("Ingrese Calle: ");
                        MostrarLista(negocio.BuscarPorCalle(Console.ReadLine()));
                        break;

                    case 5:
                        Persona nuevaP = LeerPersona();
                        CuentaCte nuevaC = LeerCuenta();
                        bool agrego = negocio.AgregarPersonaConCuenta(nuevaP, nuevaC);
                        Console.WriteLine(agrego ? "Registro guardado en ambas tablas." : "Error al guardar.");
                        break;
                    case 6:
                        Persona modP = LeerPersona();
                        CuentaCte modC = LeerCuenta();
                        bool modifico = negocio.ModificarPersonaConCuenta(modP, modC);
                        Console.WriteLine(modifico ? "Ambas tablas modificadas." : "Error al modificar.");
                        break;

                    case 7:
                        Console.Write("Ingrese DNI a eliminar: ");
                        string dniDel = Console.ReadLine();
                        bool elimino = negocio.EliminarPersonaConCuenta(dniDel);
                        Console.WriteLine(elimino ? "Eliminado de ambas tablas." : "Error al eliminar.");
                        break;  

                    case 8:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

            } while (opcion != 8);
        }

        private static Persona LeerPersona()
        {
            Persona p = new Persona();

            Console.WriteLine("Ingrese los datos de la Persona");
            Console.Write("DNI: "); p.Dni = Console.ReadLine();
            Console.Write("Apellido: "); p.Apellido = Console.ReadLine();
            Console.Write("Nombres: "); p.Nombres = Console.ReadLine();
            Console.Write("CUIL/CUIT: "); p.CuilCuit = Console.ReadLine();
            Console.Write("Calle: "); p.Calle = Console.ReadLine();

            Console.Write("Depto (Número): ");
            int depto; int.TryParse(Console.ReadLine(), out depto); p.Depto = depto;

            Console.Write("Piso (Número): ");
            int piso; int.TryParse(Console.ReadLine(), out piso); p.Piso = piso;

            Console.Write("Barrio: "); p.Barrio = Console.ReadLine();
            Console.Write("Ciudad: "); p.Ciudad = Console.ReadLine();
            Console.Write("Provincia: "); p.Provincia = Console.ReadLine();
            Console.Write("Código Postal: "); p.CodigoPostal = Console.ReadLine();
            Console.Write("Teléfono Principal: "); p.Telefono = Console.ReadLine();
            Console.Write("Teléfono Alternativo: "); p.TelefonoAlternativo = Console.ReadLine();
            Console.Write("Email: "); p.Email = Console.ReadLine();
            Console.Write("Estado Civil: "); p.EstadoCivil = Console.ReadLine();
            Console.Write("Nacionalidad: "); p.Nacionalidad = Console.ReadLine();
            Console.Write("Red Social (Instagram): "); p.RedSocial = Console.ReadLine();
            Console.Write("Profesión/Ocupación: "); p.Profesion = Console.ReadLine();
            Console.Write("Empresa/Lugar de Trabajo: "); p.EmpresaTrabajo = Console.ReadLine();
            Console.Write("Nivel de Estudios: "); p.NivelEstudios = Console.ReadLine();

            Console.Write("Estado (1: Activo / 0: Inactivo): ");
            string est = Console.ReadLine();
            p.Estado = est == "1";

            Console.Write("Método de Pago Preferido: "); p.MetodoPagoPreferido = Console.ReadLine();
            Console.Write("Observaciones: "); p.Observaciones = Console.ReadLine();

            return p;
        }

        private static void MostrarPersona(Persona p)
        {
            if (p == null)
            {
                Console.WriteLine("No se encontró el registro.");
                return;
            }

           
            Console.WriteLine($"DNI: {p.Dni} | CUIL/CUIT: {p.CuilCuit}");
            Console.WriteLine($"Nombre Completo: {p.Apellido}, {p.Nombres}");
            Console.WriteLine($"Dirección: {p.Calle} Piso {p.Piso} Depto {p.Depto}, B° {p.Barrio}, {p.Ciudad}, {p.Provincia} (CP: {p.CodigoPostal})");
            Console.WriteLine($"Contactos: Tel: {p.Telefono} | Alt: {p.TelefonoAlternativo} | Email: {p.Email} | IG: {p.RedSocial}");
            Console.WriteLine($"Info Personal: Estado Civil: {p.EstadoCivil} | Nacionalidad: {p.Nacionalidad}");
            Console.WriteLine($"Laboral/Educación: Profesión: {p.Profesion} | Trabajo: {p.EmpresaTrabajo} | Estudios: {p.NivelEstudios}");
            Console.WriteLine($"Sistema: Estado: {(p.Estado ? "ACTIVO" : "INACTIVO")} | Alta: {p.FechaAlta:dd/MM/yyyy HH:mm} | Pago: {p.MetodoPagoPreferido}");
            Console.WriteLine($"Observaciones: {p.Observaciones}");
            
        }
        private static void MostrarPersonaYCuenta(Persona p, CuentaCte c)
        {
            if (p == null)
            {
                Console.WriteLine("No se encontró la persona.");
                return;
            }

            Console.WriteLine("DATOS DE PERSONA");
            Console.WriteLine($"DNI: {p.Dni} | Nombre: {p.Apellido}, {p.Nombres}");
            Console.WriteLine($"Teléfono: {p.Telefono} | Email: {p.Email}");

            Console.WriteLine(" CUENTA CORRIENTE ");
            if (c != null)
            {
                Console.WriteLine($"ID Cuenta: {c.Id}");
                Console.WriteLine($"Fecha Apertura: {c.FechaApertura:dd/MM/yyyy}");
                Console.WriteLine($"Límite de Crédito: ${c.LimiteCredito}");
                Console.WriteLine($"Estado Crédito: {c.EstadoCredito}");
            }
            else
            {
                Console.WriteLine("No posee Cuenta Corriente asignada.");
            }
      
        }

   
        private static CuentaCte LeerCuenta()
        {
            CuentaCte c = new CuentaCte();
            Console.WriteLine(" Datos de Cuenta Corriente ");

            Console.Write("Límite de Crédito: ");
            decimal limite;
            decimal.TryParse(Console.ReadLine(), out limite);
            c.LimiteCredito = limite;

            Console.Write("Estado del Crédito (1: Activo / 2: Suspendido): ");
            string est = Console.ReadLine();
            c.EstadoCredito = (est == "1") ? "Activo" : "Suspendido";

            return c;
        }
        private static void MostrarLista(List<Persona> lista)
        {
            if (lista == null || lista.Count == 0)
            {
                Console.WriteLine("No se encontraron resultados.");
                return;
            }
            foreach (var p in lista)
            {
                MostrarPersona(p);
            }
        }
    }
}