using EstructurasDatos.Datos;
using System;
using System.Collections.Generic;

namespace EstructurasDatos.Lista
{
    public class ListaClientes
    {
        public Nodo primero;
        private int tamaño;
        public Nodo ultimo;

        public ListaClientes()
        {
            primero = null;
            ultimo = null;
            tamaño = 0;
        }

        // Método para insertar un cliente al inicio
        public void InsertarAlInicio(Cliente nuevoCliente)
        {
            Nodo nuevo = new Nodo(nuevoCliente);

            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                nuevo.enlace = primero;
                primero = nuevo;
            }

            tamaño++;
        }

        // Método para obtener la lista de clientes
        public List<Cliente> ObtenerListaClientes()
        {
            List<Cliente> clientesList = new List<Cliente>();

            if (primero == null)
                return clientesList;

            Nodo actual = primero;

            while (actual != null)
            {
                clientesList.Add(actual.dato);
                actual = actual.enlace;
            }

            return clientesList;
        }

        public Cliente BuscarPorID(int idCliente)
        {
            Nodo actual = primero;
            while (actual != null)
            {
                if (actual.dato.IdCliente == idCliente)
                    return actual.dato;

                actual = actual.enlace;
            }
            return null;
        }

        // Corregir: Eliminar por IdCliente (int)
        public void EliminarClientePorID(int idCliente)
        {
            if (primero == null) return;

            if (primero.dato.IdCliente == idCliente)
            {
                primero = primero.enlace;
                tamaño--;
                return;
            }

            Nodo actual = primero;
            while (actual.enlace != null)
            {
                if (actual.enlace.dato.IdCliente == idCliente)
                {
                    actual.enlace = actual.enlace.enlace;
                    tamaño--;
                    return;
                }
                actual = actual.enlace;
            }
        }

        // Corregir: Actualizar por IdCliente (int)
        public bool ActualizarCliente(int id, Cliente actualizado)
        {
            Nodo actual = primero;
            while (actual != null)
            {
                if (actual.dato.IdCliente == id)
                {
                    actual.dato.Nombre = actualizado.Nombre;
                    actual.dato.Direccion = actualizado.Direccion;
                    actual.dato.Email = actualizado.Email;
                    actual.dato.Telefono = actualizado.Telefono;
                    actual.dato.Nit = actualizado.Nit; // Asegúrate de actualizar todos los campos
                    actual.dato.FechaNacimiento = actualizado.FechaNacimiento;
                    return true;
                }
                actual = actual.enlace;
            }
            return false;
        }
    }
}