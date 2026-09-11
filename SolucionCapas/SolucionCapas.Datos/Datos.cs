using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SolucionCapas.Datos
{
    public class CuentaCte
    {
        public int Id { get; set; }
        public string DniPersona { get; set; }
        public DateTime FechaApertura { get; set; }
        public decimal LimiteCredito { get; set; }
        public string EstadoCredito { get; set; } 
    }
    public class Persona
    {
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Calle { get; set; }
        public int Depto { get; set; }
        public int Piso { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CuilCuit { get; set; }
        public DateTime FechaAlta { get; set; }
        public string EstadoCivil { get; set; }
        public string Nacionalidad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Barrio { get; set; }
        public string TelefonoAlternativo { get; set; }
        public string RedSocial { get; set; }
        public string Profesion { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string NivelEstudios { get; set; }
        public bool Estado { get; set; }
        public string MetodoPagoPreferido { get; set; }
        public string Observaciones { get; set; }
    }

   
    public class PersonaDatos
    {

        public CuentaCte BuscarCuentaPorDni(string dni)
        {
            string query = "SELECT * FROM CuentaCte WHERE DniPersona = @Dni";
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);
                conexion.Open();
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new CuentaCte
                        {
                            Id = lector.GetInt32("Id"),
                            DniPersona = lector.GetString("DniPersona"),
                            FechaApertura = lector.GetDateTime("FechaApertura"),
                            LimiteCredito = lector.GetDecimal("LimiteCredito"),
                            EstadoCredito = lector.GetString("EstadoCredito")
                        };
                    }
                }
            }
            return null;
        }

       
        public bool AgregarConCuenta(Persona p, CuentaCte c)
        {
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();
                MySqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string queryPersona = @"INSERT INTO personas (Dni, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email, CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal, Barrio, TelefonoAlternativo, RedSocial, Profesion, EmpresaTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, Observaciones) 
                                    VALUES (@Dni, @Apellido, @Nombres, @Calle, @Depto, @Piso, @Ciudad, @Telefono, @Email, @CuilCuit, @FechaAlta, @EstadoCivil, @Nacionalidad, @Provincia, @CodigoPostal, @Barrio, @TelefonoAlternativo, @RedSocial, @Profesion, @EmpresaTrabajo, @NivelEstudios, @Estado, @MetodoPagoPreferido, @Observaciones)";

                    MySqlCommand cmdPersona = new MySqlCommand(queryPersona, conexion, transaccion);
                    CargarParametros(cmdPersona, p);
                    cmdPersona.ExecuteNonQuery();


                    string queryCuenta = @"INSERT INTO CuentaCte (DniPersona, FechaApertura, LimiteCredito, EstadoCredito) 
                                   VALUES (@DniPersona, @FechaApertura, @LimiteCredito, @EstadoCredito)";

                    MySqlCommand cmdCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                    cmdCuenta.Parameters.AddWithValue("@DniPersona", p.Dni);
                    cmdCuenta.Parameters.AddWithValue("@FechaApertura", c.FechaApertura);
                    cmdCuenta.Parameters.AddWithValue("@LimiteCredito", c.LimiteCredito);
                    cmdCuenta.Parameters.AddWithValue("@EstadoCredito", c.EstadoCredito);
                    cmdCuenta.ExecuteNonQuery();

                    transaccion.Commit();
                    return true;
                }
                catch
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }


        public bool ModificarConCuenta(Persona p, CuentaCte c)
        {
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();
                MySqlTransaction transaccion = conexion.BeginTransaction();

                try
                {

                    string queryPersona = @"UPDATE personas SET Apellido=@Apellido, Nombres=@Nombres, Calle=@Calle, Depto=@Depto, Piso=@Piso, Ciudad=@Ciudad, Telefono=@Telefono, Email=@Email, CuilCuit=@CuilCuit, EstadoCivil=@EstadoCivil, Nacionalidad=@Nacionalidad, Provincia=@Provincia, CodigoPostal=@CodigoPostal, Barrio=@Barrio, TelefonoAlternativo=@TelefonoAlternativo, RedSocial=@RedSocial, Profesion=@Profesion, EmpresaTrabajo=@EmpresaTrabajo, NivelEstudios=@NivelEstudios, Estado=@Estado, MetodoPagoPreferido=@MetodoPagoPreferido, Observaciones=@Observaciones WHERE Dni=@Dni";

                    MySqlCommand cmdPersona = new MySqlCommand(queryPersona, conexion, transaccion);
                    CargarParametros(cmdPersona, p);
                    cmdPersona.ExecuteNonQuery();

                    string queryCuenta = @"UPDATE CuentaCte SET LimiteCredito=@LimiteCredito, EstadoCredito=@EstadoCredito WHERE DniPersona=@DniPersona";

                    MySqlCommand cmdCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                    cmdCuenta.Parameters.AddWithValue("@DniPersona", p.Dni);
                    cmdCuenta.Parameters.AddWithValue("@LimiteCredito", c.LimiteCredito);
                    cmdCuenta.Parameters.AddWithValue("@EstadoCredito", c.EstadoCredito);
                    cmdCuenta.ExecuteNonQuery();

                    transaccion.Commit();
                    return true;
                }
                catch
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }


        public bool EliminarConCuenta(string dni)
        {
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();
                MySqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string queryCuenta = "DELETE FROM CuentaCte WHERE DniPersona = @Dni";
                    MySqlCommand cmdCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                    cmdCuenta.Parameters.AddWithValue("@Dni", dni);
                    cmdCuenta.ExecuteNonQuery();

                    string queryPersona = "DELETE FROM personas WHERE Dni = @Dni";
                    MySqlCommand cmdPersona = new MySqlCommand(queryPersona, conexion, transaccion);
                    cmdPersona.Parameters.AddWithValue("@Dni", dni);
                    cmdPersona.ExecuteNonQuery();

                    transaccion.Commit();
                    return true;
                }
                catch
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }
        private string _conexionString = "server=localhost;port=3307;database=agenda;user=root;password=;";

        public Persona BuscarPorDni(string dni)
        {
            string query = "SELECT * FROM personas WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);
                conexion.Open();

                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return MapearPersona(lector);
                }
            }
            return null;
        }

        public List<Persona> BuscarPorTexto(string campo, string valor)
        {
            var resultado = new List<Persona>();
            string query = $"SELECT * FROM personas WHERE {campo} LIKE @Valor";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Valor", "%" + valor + "%");
                conexion.Open();

                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        resultado.Add(MapearPersona(lector));
                }
            }
            return resultado;
        }

        public bool Agregar(Persona p)
        {
            string query = @"INSERT INTO personas 
                (Dni, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email, CuilCuit, FechaAlta, 
                 EstadoCivil, Nacionalidad, Provincia, CodigoPostal, Barrio, TelefonoAlternativo, RedSocial, 
                 Profesion, EmpresaTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, Observaciones) 
                VALUES 
                (@Dni, @Apellido, @Nombres, @Calle, @Depto, @Piso, @Ciudad, @Telefono, @Email, @CuilCuit, @FechaAlta, 
                 @EstadoCivil, @Nacionalidad, @Provincia, @CodigoPostal, @Barrio, @TelefonoAlternativo, @RedSocial, 
                 @Profesion, @EmpresaTrabajo, @NivelEstudios, @Estado, @MetodoPagoPreferido, @Observaciones)";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                CargarParametros(comando, p);
                conexion.Open();

                return comando.ExecuteNonQuery() > 0;
            }
        }

        public bool Modificar(Persona p)
        {
            string query = @"UPDATE personas SET 
                Apellido=@Apellido, Nombres=@Nombres, Calle=@Calle, Depto=@Depto, Piso=@Piso, Ciudad=@Ciudad, 
                Telefono=@Telefono, Email=@Email, CuilCuit=@CuilCuit, EstadoCivil=@EstadoCivil, 
                Nacionalidad=@Nacionalidad, Provincia=@Provincia, CodigoPostal=@CodigoPostal, Barrio=@Barrio, 
                TelefonoAlternativo=@TelefonoAlternativo, RedSocial=@RedSocial, Profesion=@Profesion, 
                EmpresaTrabajo=@EmpresaTrabajo, NivelEstudios=@NivelEstudios, Estado=@Estado, 
                MetodoPagoPreferido=@MetodoPagoPreferido, Observaciones=@Observaciones 
                WHERE Dni=@Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                CargarParametros(comando, p);
                conexion.Open();

                return comando.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(string dni)
        {
            string query = "DELETE FROM personas WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);
                conexion.Open();

                return comando.ExecuteNonQuery() > 0;
            }
        }

        private void CargarParametros(MySqlCommand comando, Persona p)
        {
            comando.Parameters.AddWithValue("@Dni", p.Dni ?? "");
            comando.Parameters.AddWithValue("@Apellido", p.Apellido ?? "");
            comando.Parameters.AddWithValue("@Nombres", p.Nombres ?? "");
            comando.Parameters.AddWithValue("@Calle", p.Calle ?? "");
            comando.Parameters.AddWithValue("@Depto", p.Depto);
            comando.Parameters.AddWithValue("@Piso", p.Piso);
            comando.Parameters.AddWithValue("@Ciudad", p.Ciudad ?? "");
            comando.Parameters.AddWithValue("@Telefono", p.Telefono ?? "");
            comando.Parameters.AddWithValue("@Email", p.Email ?? "");
            comando.Parameters.AddWithValue("@CuilCuit", p.CuilCuit ?? "");
            comando.Parameters.AddWithValue("@FechaAlta", p.FechaAlta);
            comando.Parameters.AddWithValue("@EstadoCivil", p.EstadoCivil ?? "");
            comando.Parameters.AddWithValue("@Nacionalidad", p.Nacionalidad ?? "");
            comando.Parameters.AddWithValue("@Provincia", p.Provincia ?? "");
            comando.Parameters.AddWithValue("@CodigoPostal", p.CodigoPostal ?? "");
            comando.Parameters.AddWithValue("@Barrio", p.Barrio ?? "");
            comando.Parameters.AddWithValue("@TelefonoAlternativo", p.TelefonoAlternativo ?? "");
            comando.Parameters.AddWithValue("@RedSocial", p.RedSocial ?? "");
            comando.Parameters.AddWithValue("@Profesion", p.Profesion ?? "");
            comando.Parameters.AddWithValue("@EmpresaTrabajo", p.EmpresaTrabajo ?? "");
            comando.Parameters.AddWithValue("@NivelEstudios", p.NivelEstudios ?? "");
            comando.Parameters.AddWithValue("@Estado", p.Estado);
            comando.Parameters.AddWithValue("@MetodoPagoPreferido", p.MetodoPagoPreferido ?? "");
            comando.Parameters.AddWithValue("@Observaciones", p.Observaciones ?? "");
        }

        private Persona MapearPersona(MySqlDataReader lector)
        {
            return new Persona
            {
                Dni = lector.GetString("Dni"),
                Apellido = lector.GetString("Apellido"),
                Nombres = lector.GetString("Nombres"),
                Calle = lector.GetString("Calle"),
                Depto = lector.GetInt32("Depto"),
                Piso = lector.GetInt32("Piso"),
                Ciudad = lector.GetString("Ciudad"),
                Telefono = lector.GetString("Telefono"),
                Email = lector.GetString("Email"),
                CuilCuit = lector.GetString("CuilCuit"),
                FechaAlta = lector.GetDateTime("FechaAlta"),
                EstadoCivil = lector.GetString("EstadoCivil"),
                Nacionalidad = lector.GetString("Nacionalidad"),
                Provincia = lector.GetString("Provincia"),
                CodigoPostal = lector.GetString("CodigoPostal"),
                Barrio = lector.GetString("Barrio"),
                TelefonoAlternativo = lector.GetString("TelefonoAlternativo"),
                RedSocial = lector.GetString("RedSocial"),
                Profesion = lector.GetString("Profesion"),
                EmpresaTrabajo = lector.GetString("EmpresaTrabajo"),
                NivelEstudios = lector.GetString("NivelEstudios"),
                Estado = lector.GetBoolean("Estado"),
                MetodoPagoPreferido = lector.GetString("MetodoPagoPreferido"),
                Observaciones = lector.GetString("Observaciones")
            };
        }
    }
}