using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ARBOL
{
    
       public class ArbolAVL<T> where T : IComparable<T>
       {
           public nodo<T> root = new nodo<T>();
           public nodo<T> num = new nodo<T>();

           public List<T> list_orden = new List<T>();
           public int n = 0;
           public int rotacion = 0;

           //AÑADIR VALOR NUEVO AL ARBOL
           public void Add(T value)
           {
               Insert(value);
           }

           public int ObtenerAltura(nodo<T> n)
           {
               if (n == null)
               {
                   return -1;
               }
               else
               {
                   return n.altura;
               }
           }

           //ELIMINAR NODO
           protected void Delete(nodo<T> nodo)
           {
               if (nodo.izquierda.value == null && nodo.derecha.value == null) 
               {
                   nodo.value = nodo.derecha.value; 
               }
               else if (nodo.derecha.value == null) 
               {
                   nodo.value = nodo.izquierda.value;
                   nodo.derecha = nodo.izquierda.derecha;

                   nodo.izquierda = nodo.izquierda.izquierda;
               }
               else
               {
                   if (nodo.izquierda.value != null)
                   {
                       num = LADODERECHO(nodo.izquierda);
                   }
                   else
                   {
                       num = LADODERECHO(nodo); 
                   }
                   nodo.value = num.value;
               }
           }


           public T Remove(T borrado)
           {
                nodo<T> buscado = new nodo<T>();
                buscado = Get(root, borrado);
                if (buscado != null)
                {
                    Delete(buscado);
                }
                return borrado;
           }


           private nodo<T> LADODERECHO(nodo<T> nodo)
           {
               if (nodo.derecha.value == null)
               {
                   if (nodo.izquierda.value != null)
                   {
                       return LADODERECHO(nodo.izquierda);
                   }
                   else
                   {
                       nodo<T> temp = new nodo<T>();
                       temp.value = nodo.value;
                       nodo.value = nodo.derecha.value;
                       return temp;
                   }
               }
               else
               {
                   return LADODERECHO(nodo.derecha);
               }
           }


           protected nodo<T> Get(nodo<T> nodo, T getvalue)
           {
               if (getvalue.CompareTo(nodo.value) == 0)
               {
                   return nodo;
               }
               else if (getvalue.CompareTo(nodo.value) == -1)
               {
                   if (nodo.izquierda.value == null)
                   {
                       return null;
                   }
                   else
                   {
                       return Get(nodo.izquierda, getvalue);//devuelve el nodo izquierdo
                   }
               }
               else
               {
                   if (nodo.derecha.value == null)//el nodo derecho esta vacio
                   {
                       return null;
                   }
                   else
                   {
                       return Get(nodo.derecha, getvalue);//devuelve el nodo derecho
                   }
               }
           }


           public nodo<T> InsertarEnArbol(nodo<T> nodo, nodo<T> temporal) //Se inserta el elvalor en el arbol y se verifico si está ordenado
           {
               try
               {
                   nodo<T> nuevoNodo = temporal;

                   if (nodo.value.CompareTo(temporal.value) == -1)//si el nodo es menor al actual
                   {
                       if (temporal.izquierda.value == null)//si el hijo izquerdo esta vacio
                       {
                           temporal.izquierda = nodo;//se asigna el nuevo nodo en el hijo izquierdo

                       }
                       else
                       {

                           temporal.izquierda = InsertarEnArbol(nodo, temporal.izquierda);//verificar si se puede insertar en el hijo izquierdo

                           if ((ObtenerAltura(temporal.izquierda) - ObtenerAltura(temporal.derecha) == 2))//si el Factor de equilibrio es 2
                           {
                               if (nodo.value.CompareTo(temporal.izquierda.value) == -1)//si el el factor de equilibrio es -1
                               {
                                   nuevoNodo = RotarIzquierda(temporal);//se hace una rotacion izquierda
                               }
                               else
                               {
                                   nuevoNodo = RotarDerechaIzquierda(temporal);//se hace una doble rotacion izquierda
                               }
                           }

                       }
                   }
                   else if (nodo.value.CompareTo(temporal.value) == 1)//si el nodo a insertar es mayor al actual
                   {
                       if (temporal.derecha.value== null)//si el hijo derecho esta vacio
                       {
                           temporal.derecha = nodo;//se asigna el nuevo nodo en el hijo derecho
                       }
                       else
                       {

                           temporal.derecha= InsertarEnArbol(nodo, temporal.derecha);//se revisa si se puede insertar en el hijo derecho
                           if ((ObtenerAltura(temporal.derecha) - ObtenerAltura(temporal.izquierda) == 2))//si el factor de equilibrio es 2
                           {
                               if (nodo.value.CompareTo(temporal.derecha.value) == 1)//si el factor de equilibrio es -1
                               {
                                   nuevoNodo = RotarDerecha(temporal);//se hace una rotacion derecha
                               }
                               else
                               {
                                   nuevoNodo = RotarDerechaDerecha(temporal);//se hace una doble rotacion derecha
                               }
                           }

                       }
                   }
                   //altura
                   if ((temporal.izquierda == null) && (temporal.derecha!= null))//si solo tiene un hijo derecho
                   {
                       temporal.altura = temporal.derecha.altura + 1;//se le agrega 1 al FE 
                   }
                   else if ((temporal.izquierda != null) && (temporal.derecha== null))// si solo tiene hijo izquierdo
                   {
                       temporal.altura = temporal.izquierda.altura+ 1;//se le agrega 1 al FE
                   }
                   else
                   {
                       temporal.altura = Math.Max(ObtenerAltura(temporal.izquierda), ObtenerAltura(nodo.derecha)) + 1;//se le suma uno al FE mas alto
                   }
                   return nuevoNodo;
               }
               catch
               {
                   throw;
               }
           }


           public nodo<T> CrearNodoAVL(T valuecreado)//metodo para creat un nuevo nodo con un valor
           {
               nodo<T> nodo = new nodo<T>();//se crea un nodo
               nodo.value = valuecreado;//se le asignan los valores
               nodo.altura = 0;
               nodo.izquierda = new nodo<T>();
               nodo.derecha = new nodo<T>();
               return nodo;
           }

           public void Insert(T valueinsertado)//metodo para insertar un valor al arbol
           {
               try
               {
                   nodo<T> nuevo = CrearNodoAVL(valueinsertado);//se crea el nodo con el valor a isertar

                   if (root.value == null)//si la raiz esta vacia
                   {
                       root = nuevo;  //el nodo se convierte en la raiz
                   }
                   else
                   {
                       root = InsertarEnArbol(nuevo, root);//se inserta el nodo al arbol
                   }
               }
               catch
               {
                   throw;
               }
           }


           protected nodo<T> Insert(nodo<T> nodo, T valueinsertado)//metodo para insertar nodo al arbol
           {
               try
               {
                   nodo<T> nuevo = CrearNodoAVL(valueinsertado);//se crea el nodo a insertar

                   if (nodo == null)//si el nodo esta vacio
                   {
                       nodo = nuevo;//se inserta en el nodo
                   }
                   else
                   {
                       nodo = InsertarEnArbol(nuevo, nodo);//se revisa si se puede insertar en uno de los dos hijos

                   }
                   return nodo;
               }
               catch
               {
                   throw;
               }
           }


           public nodo<T> RotarIzquierda(nodo<T> nodo)//rotacion izquierda
           {
               rotacion++;
               nodo<T> temp = nodo.izquierda;

               nodo.izquierda = temp.derecha;
               temp.derecha = nodo;
               nodo.altura = Math.Max(ObtenerAltura(nodo.izquierda), ObtenerAltura(nodo.derecha)) + 1;//Devuelve el mayor
               temp.altura = Math.Max(ObtenerAltura(temp.izquierda), ObtenerAltura(temp.derecha)) + 1;// Devuelve el mayor
               return temp;
           }

           public nodo<T> RotarDerecha(nodo<T> nodo)//rotacion derecha
           {
               rotacion++;
               nodo<T> temp = nodo.derecha;
               nodo.derecha = temp.izquierda;
               temp.izquierda = nodo;
               nodo.altura = Math.Max(ObtenerAltura(nodo.izquierda), ObtenerAltura(nodo.derecha)) + 1;//Devuelve el mayor
               temp.altura = Math.Max(ObtenerAltura(temp.izquierda), ObtenerAltura(temp.derecha)) + 1;// Devuelve el mayor
               return temp;
           }

           public nodo<T> RotarDerechaIzquierda(nodo<T> nodo)// Rotación Doble Izquierda
           {
               rotacion++;
               nodo<T> temp = new nodo<T>();
               nodo.izquierda = RotarDerecha(nodo.izquierda);
               temp = RotarIzquierda(nodo);
               return temp;
           }

           public nodo<T> RotarDerechaDerecha(nodo<T> nodo)// Rotación Doble Nododerecho
           {
               rotacion++;
               nodo<T> temp = new nodo<T>();
               nodo.derecha= RotarIzquierda(nodo.derecha);
               temp = RotarDerecha(nodo);
               return temp;
           }

           private void InOrder(nodo<T> nodo)//metodo que agrega los valores a la lista ordenada
           {//recorre el arbol en InOrder y va agregando los valores uno por uno
               if (nodo.value != null)
               {
                   InOrder(nodo.izquierda);
                   list_orden.Add(nodo.value);
                   InOrder(nodo.derecha);
               }
           }


           public List<T> ObtenerListaOrdenada()//metodo para obtener la lista ordenada
           {
               list_orden.Clear();//se limpian la lista
               InOrder(root);//se agregan los valores a la lista
               return list_orden;
           }



           public List<T> ObtenerDatosLista(Func<T, bool> Predicate)//metodo para obtener ciertos datos de la lista ordenada
           {
               List<T> prov = new List<T>();
               n = 0;
               ObtenerListaOrdenada();
               for (int i = 0; i < list_orden.Count(); i++)
               {
                   if (Predicate(list_orden[i]))
                   {
                       n = i;
                       prov.Add(list_orden[i]);
                   }
               }
               return prov;
           }


           public List<T> ObtenerDatos2(Func<T, bool> Predicate, Func<T, bool> Predicate2)//metodo para obtener ciertos datos de la lista ordenada
           {
               List<T> prov = new List<T>();
               nodo<T> cmp = new nodo<T>();
               n = 0;
               ObtenerListaOrdenada();
               for (int i = 0; i < list_orden.Count(); i++)
               {
                   if (Predicate2(list_orden[i]))
                   {
                       if (Predicate(list_orden[i]))
                       {
                           n = i;
                           prov.Add(list_orden[i]);
                       }
                   }
               }
               return prov;
           }


           private int ObtenerAlturaNodo(nodo<T> nodo)//metodo para obtener la altura de un nodo 
           {
               if (nodo == null)
               {
                   return -1;
               }
               else
               {
                   int izquierda = ObtenerAlturaNodo(nodo.izquierda);
                   int derecha = ObtenerAlturaNodo(nodo.derecha);
                   return Math.Max(izquierda, derecha) + 1;
               }
           }

           public nodo<T> GetDPI(long dpi)
           {
               return GetDPI(root, dpi);
           }

           private nodo<T> GetDPI(nodo<T> nodo, long dpi)
           {
               if (nodo == null)
               {
                   return null;
               }

               // Supongamos que "elvalor" tiene una propiedad "DPI"
               long nodoDPI = ((dynamic)nodo.value).DPI;

               if (dpi == nodoDPI)
               {
                   return nodo;
               }
               else if (dpi < nodoDPI)
               {
                   return GetDPI(nodo.izquierda, dpi);
               }
               else
               {
                   return GetDPI(nodo.derecha, dpi);
               }
           }


           public bool Update(T persona, long dpi)
           {
               nodo<T> nodo = Get(root, persona); // Buscar el nodo con el valor DPI a actualizar
               if (nodo != null)
               {
                   // Eliminar el nodo con el valor antiguo
                   Remove(persona);

                   // Actualizar el valor DPI en el objeto persona
                   // (esto depende de la estructura de tu clase T)
                   // Por ejemplo, supongamos que tu clase T tiene una propiedad llamada "DPI":
                   // persona.DPI = dpi;

                   // Volver a insertar el nodo actualizado en el árbol
                   Insert(persona);

                   return true; // Actualización exitosa
               }
               else
               {
                   return false; 
               }
           }

       }
   
}