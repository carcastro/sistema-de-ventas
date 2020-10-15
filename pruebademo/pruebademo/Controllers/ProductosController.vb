Imports System.Net
Imports System.Data.SqlClient

Namespace pruebademo
    Public Class ProductosController
        Inherits Controller

        Private ReadOnly conexion As New SqlConnection(ConfigurationManager.ConnectionStrings("SQLConn").ConnectionString)
        Private comando As SqlCommand
        Private resultado As SqlDataReader

        ' GET: Productos
        Function Index(ByVal busqueda As String) As ActionResult
            Dim listaProductos = New List(Of Producto)

            Dim busquedaNombre = Request("busquedaPorNombre")
            Dim busquedaCategoria = Request("busquedaPorCategoria")

            If (Not String.IsNullOrEmpty(busquedaNombre)) Then
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos WHERE Nombre LIKE '%" + busquedaNombre + "%' and eliminado=0"

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim Producto = New Producto With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Precio = resultado.GetDouble(2).ToString,
                    .Categoria = resultado.GetString(3)
                }
                    listaProductos.Add(Producto)
                Loop
                conexion.Close()

                Return View(listaProductos)
            End If
            If (Not String.IsNullOrEmpty(busquedaCategoria)) Then

                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos WHERE Categoria LIKE '%" + busquedaCategoria + "%' and eliminado=0"

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim Producto = New Producto With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Precio = resultado.GetDouble(2).ToString,
                    .Categoria = resultado.GetString(3)
                }
                    listaProductos.Add(Producto)
                Loop
                conexion.Close()

                Return View(listaProductos)
            End If

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos Where eliminado=0"

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim Producto = New Producto With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Precio = resultado.GetDouble(2).ToString,
                    .Categoria = resultado.GetString(3)
                }
                listaProductos.Add(Producto)
            Loop
            conexion.Close()
            Return View(listaProductos)
        End Function

        ' GET: productos/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Productos/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Nombre,Precio,Categoria")> ByVal producto As Producto) As ActionResult
            If ModelState.IsValid Then
                comando = conexion.CreateCommand
                comando.CommandText = "INSERT INTO dbo.productos (Nombre, Precio, Categoria) VALUES ('" + producto.Nombre + "'," + producto.Precio.ToString + ",'" + producto.Categoria + "')"
                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()
                Return RedirectToAction("Index")
            End If
            Return View(producto)
        End Function

        ' GET: Productos/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim producto = New Producto()

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos where ID=" + id.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                producto.ID = resultado.GetInt32(0)
                producto.Nombre = resultado.GetString(1)
                producto.Precio = resultado.GetDouble(2).ToString
                producto.Categoria = resultado.GetString(3)
            Loop
            conexion.Close()
            If IsNothing(producto) Then
                Return HttpNotFound()
            End If
            Return View(producto)
        End Function

        ' POST: Productos/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID,Nombre,Precio,Categoria")> ByVal producto As Producto) As ActionResult
            If ModelState.IsValid Then
                comando = conexion.CreateCommand
                comando.CommandText = "UPDATE dbo.Productos SET Nombre ='" + producto.Nombre + "', Precio=" + producto.Precio.ToString + ", Categoria='" + producto.Categoria + "' WHERE ID=" + producto.ID.ToString
                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()
            End If
            Return RedirectToAction("Index")
        End Function

        ' GET: productos/Delete/5
        Function Delete(producto As Producto) As ActionResult
            If IsNothing(producto.ID) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos where ID=" + producto.ID.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                producto = New Producto With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Precio = resultado.GetDouble(2).ToString,
                    .Categoria = resultado.GetString(3)
                }
            Loop
            conexion.Close()
            If IsNothing(producto) Then
                Return HttpNotFound()
            End If
            Return View(producto)
        End Function

        ' POST: productos/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            comando = conexion.CreateCommand
            comando.CommandText = "Update dbo.productos SET eliminado=1 WHERE ID=" + id.ToString
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
            comando.CommandText = "UPDATE productos SET eliminado=1 where ID IN(" + values + ")"
            conexion.Open()
            comando.ExecuteNonQuery()
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace
