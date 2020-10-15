Imports System.Net
Imports System.Data.SqlClient

Namespace pruebademo
    Public Class ClientesController
        Inherits Controller

        Private ReadOnly conexion As New SqlConnection(ConfigurationManager.ConnectionStrings("SQLConn").ConnectionString)
        Private comando As SqlCommand
        Private resultado As SqlDataReader

        Function Ordenar(ByVal orden As String) As ActionResult
            Dim listaClientes = New List(Of Cliente)

        End Function
        ' GET: clientes
        Function Index(ByVal orden As String) As ActionResult

            Dim listaClientes = New List(Of Cliente)
            If (Not String.IsNullOrEmpty(orden)) Then
                If (orden = "nombre") Then
                    comando = conexion.CreateCommand
                    comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM clientes where eliminado=0 ORDER BY Cliente DESC"

                Else
                    comando = conexion.CreateCommand
                    comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM clientes where eliminado=0 ORDER BY Cliente ASC"
                End If

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim cliente = New Cliente With {
                        .ID = resultado.GetInt32(0),
                        .Nombre = resultado.GetString(1),
                        .Telefono = resultado.GetString(2),
                        .Correo = resultado.GetString(3)
                    }
                    listaClientes.Add(cliente)
                Loop
                conexion.Close()

                Return View(listaClientes)
            End If

            Dim busquedaNombre = Request("busquedaNombre")
            Dim busquedaCorreo = Request("busquedaCorreo")

            If (Not String.IsNullOrEmpty(busquedaNombre)) Then
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where Cliente LIKE '%" + busquedaNombre + "%'"

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim cliente = New Cliente With {
                        .ID = resultado.GetInt32(0),
                        .Nombre = resultado.GetString(1),
                        .Telefono = resultado.GetString(2),
                        .Correo = resultado.GetString(3)
                    }
                    listaClientes.Add(cliente)
                Loop
                conexion.Close()

                Return View(listaClientes)

            End If

            If (Not String.IsNullOrEmpty(busquedaCorreo)) Then
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where Correo LIKE '%" + busquedaCorreo + "%'"

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim cliente = New Cliente With {
                        .ID = resultado.GetInt32(0),
                        .Nombre = resultado.GetString(1),
                        .Telefono = resultado.GetString(2),
                        .Correo = resultado.GetString(3)
                    }
                    listaClientes.Add(cliente)
                Loop
                conexion.Close()

                Return View(listaClientes)
            End If

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where eliminado=0"

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim cliente = New Cliente With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Telefono = resultado.GetString(2),
                    .Correo = resultado.GetString(3)
                }
                listaClientes.Add(cliente)
            Loop
            conexion.Close()

            Return View(listaClientes)
        End Function

        ' GET: clientes/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: clientes/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Nombre,Telefono,Correo")> ByVal cliente As Cliente) As ActionResult
            If ModelState.IsValid Then
                comando = conexion.CreateCommand
                comando.CommandText = "INSERT INTO dbo.clientes (Cliente, Telefono, Correo) VALUES ('" + cliente.Nombre + "','" + cliente.Telefono + "','" + cliente.Correo + "')"
                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()
                Return RedirectToAction("Index")
            End If
            Return View(cliente)
        End Function

        ' GET: clientes/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim cliente = New Cliente()

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where ID=" + id.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                cliente = New Cliente With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Telefono = resultado.GetString(2),
                    .Correo = resultado.GetString(3)
                }
            Loop
            conexion.Close()
            If IsNothing(cliente) Then
                Return HttpNotFound()
            End If
            Return View(cliente)
        End Function

        ' POST: clientes/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID,Nombre,Telefono,Correo")> ByVal cliente As Cliente) As ActionResult
            If ModelState.IsValid Then
                comando = conexion.CreateCommand
                comando.CommandText = "UPDATE dbo.clientes SET Cliente ='" + cliente.Nombre + "', Telefono='" + cliente.Telefono + "', Correo='" + cliente.Correo + "' WHERE ID=" + cliente.ID.ToString
                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()
            End If
            Return RedirectToAction("Index")
        End Function

        ' GET: clientes/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cliente = New Cliente()

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where ID=" + id.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                cliente = New Cliente With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Telefono = resultado.GetString(2),
                    .Correo = resultado.GetString(3)
                }
            Loop
            conexion.Close()
            If IsNothing(cliente) Then
                Return HttpNotFound()
            End If
            Return View(cliente)
        End Function

        ' POST: clientes/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            comando = conexion.CreateCommand
            comando.CommandText = "UPDATE dbo.clientes SET eliminado=1 WHERE ID=" + id.ToString
            conexion.Open()
            comando.ExecuteNonQuery()
            conexion.Close()
            Return RedirectToAction("Index")
        End Function
        Function DeleteGroup(arr As Integer()) As ActionResult
            Dim lista = Request("ID").Split(",")

            Dim values As String = ""
            For Each id As String In lista
                values += id + ","
            Next
            values = values.Remove(values.Length - 1)
            comando = conexion.CreateCommand
            comando.CommandText = "UPDATE Clientes SET eliminado=1 where ID IN(" + values + ")"
            conexion.Open()
            comando.ExecuteNonQuery()
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace
