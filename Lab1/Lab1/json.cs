using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;

using Lab1;

namespace Lab1
{
    public class json
    {
        public static ArbolAVL<Persona> arbol = new ArbolAVL<Persona>();


        public static void leerArchivo()
        {
            string filePath = "datos.txt"; 
            string filePathJsonL = "convertidos.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 2)
                    {
                        string action = parts[0];
                        string dataJson = parts[1].Trim();

                        Persona person = Newtonsoft.Json.JsonConvert.DeserializeObject<Persona>(dataJson);
                        commandReader(action, person, arbol);
                    }
                }
                GuardarArbolEnJsonl(arbol, filePathJsonL);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
        }

        public static void commandReader(string action, Persona person, ArbolAVL<Persona> arbol)
        {
            if (action == "INSERT")
            {
                try
                {
                    arbol.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                    throw;
                }


            }
            else if (action == "DELETE")
            {
                try
                {
                    arbol.Remove(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("El error es " + ex);
                    throw;
                }

            }
            else if (action == "PATCH")
            {
                try
                {
                    arbol.Update(person, person.DPI);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("El error es " + ex);
                    throw;
                }

            }
        }

        public static void GuardarArbolEnJsonl(ArbolAVL<Persona> arbol, string filePath)
        {
            try
            {
                List<string> jsonLines = new List<string>();

                List<Persona> elementos = arbol.ObtenerListaOrdenada(); // Cambia esto según el nombre de tu método

                foreach (var persona in elementos)
                {
                    string jsonData = JsonConvert.SerializeObject(persona);
                    jsonLines.Add($"{jsonData}");
                }

                File.WriteAllLines(filePath, jsonLines);
                Console.WriteLine($"Árbol guardado en '{filePath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el árbol en el archivo JSONL: " + ex.Message);
            }
        }


        public static string personaBuscada(long dpiABuscar)
        {
            nodo<Persona> nodoEncontrado = arbol.GetDPI(dpiABuscar);

            if (nodoEncontrado != null)
            {
                Persona personaEncontrada = nodoEncontrado.value;
                return ($"DPI: {personaEncontrada.DPI}, Nombre: {personaEncontrada.name}, Nacimiento: {personaEncontrada.datebirth}, Direccion: {personaEncontrada.address}");
            }
            else
            {
                return ($"No se encontró un nodo con DPI: {dpiABuscar}");
            }

        }
    }
}
