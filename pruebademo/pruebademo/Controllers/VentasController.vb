Imports System.Data.SqlClient
Imports System.Net
Imports Newtonsoft.Json

Namespace Controllers
    Public Class VentasController
        Inherits Controller

        Private ReadOnly conexion As New SqlConnection(ConfigurationManager.ConnectionStrings("SQLConn").ConnectionString)
        Private comando As SqlCommand
        Private resultado As SqlDataReader

        ' GET: Ventas
        Function Index() As ActionResult
            Dim ventas = New List(Of Venta)
            comando = conexion.CreateCommand
            comando.CommandText = "select ID, IDCliente, Fecha, Total
								from ventas where eliminado = 0"

            conexion.Open()
            resultado = comando.ExecuteReader()

            Do While resultado.Read()
                Dim venta = New Venta()
                venta.ID = resultado.GetInt32(0)
                venta.IDCliente = resultado.GetInt32(1)
                venta.Fecha = resultado.GetDateTime(2).ToString
                venta.Total = resultado.GetDouble(3).ToString
                ventas.Add(venta)
            Loop
            conexion.Close()

            Return View(ventas)

        End Function
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim venta = New Venta()

            comando = conexion.CreateCommand
            comando.CommandText = "select ventasitems.ID, IDProducto, PrecioUnitario, Cantidad, PrecioTotal, Nombre
								from ventasitems
								join productos on productos.ID = ventasitems.IDProducto
								Where IDVenta = " + id.ToString

            Dim itemsVenta = New List(Of VentaItem)

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim item = New VentaItem()
                item.ID = resultado.GetInt32(0)
                item.IDProducto = resultado.GetInt32(1)
                item.PrecioUnitario = resultado.GetDouble(2).ToString()
                item.Cantidad = resultado.GetDouble(3).ToString()
                item.SubTotal = resultado.GetDouble(4).ToString()
                Dim prod = New Producto With {
                    .Nombre = resultado.GetString(5)
                }
                item.Producto = prod
                itemsVenta.Add(item)
            Loop

            venta.Items = itemsVenta

            conexion.Close()
            comando.CommandText = "select ID, IDCliente, Fecha, Total
								from ventas
								Where ID =" + id.ToString
            conexion.Open()
            resultado = comando.ExecuteReader()

            Do While resultado.Read()
                venta.IDCliente = resultado.GetInt32(1).ToString
                venta.Total = resultado.GetDouble(3).ToString
            Loop
            conexion.Close()


            comando.CommandText = "select ID,Cliente, Telefono, Correo
								from clientes
								Where ID = " + venta.IDCliente.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()

            Do While resultado.Read()
                Dim cliente = New Cliente With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Telefono = resultado.GetString(2),
                    .Correo = resultado.GetString(3)
                }
                venta.Cliente = cliente
            Loop
            conexion.Close()



            If IsNothing(venta) Then
                Return HttpNotFound()
            End If
            Return View(venta)
        End Function

        ' GET: ventas/Create
        Function Create() As ActionResult
            Dim venta = CrearVenta()
            Return View(venta)
        End Function

        Function CrearVenta() As Venta
            Dim listaClientes = New List(Of Cliente)
            Dim listaProductos = New List(Of VentaItem)
            Dim venta = New Venta()

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

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Nombre, Precio, Categoria FROM Productos Where eliminado=0"

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim producto = New Producto With {
                    .ID = resultado.GetInt32(0),
                    .Nombre = resultado.GetString(1),
                    .Precio = resultado.GetDouble(2).ToString,
                    .Categoria = resultado.GetString(3)
                }
                Dim ventaItem = New VentaItem()
                ventaItem.IDProducto = producto.ID
                ventaItem.PrecioUnitario = producto.Precio
                ventaItem.Producto = producto

                listaProductos.Add(ventaItem)
            Loop
            conexion.Close()

            ViewBag.Clientes = listaClientes
            ViewBag.Productos = listaProductos

            Return venta
        End Function

        <HttpPost>
        Function Create(ventaIngresa As String) As ActionResult

            If (Not String.IsNullOrEmpty(ventaIngresa)) Then
                Dim ventaInput = JsonConvert.DeserializeObject(Of Venta)(ventaIngresa)

                Dim fechaAux = Convert.ToDateTime(ventaInput.Fecha).ToString("yyyy-MM-dd HH:mm:ss")
                ventaInput.Fecha = fechaAux

                comando = conexion.CreateCommand
                comando.CommandText = "INSERT INTO ventas (IDCliente, Fecha, Total) VALUES ('" + ventaInput.IDCliente.ToString + "','" + ventaInput.Fecha.ToString + "','" + ventaInput.Total.ToString + "')"

                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()


                comando = conexion.CreateCommand
                comando.CommandText = "Select TOP 1 ID From [dbo].[ventas] ORDER BY ID DESC "

                conexion.Open()
                resultado = comando.ExecuteReader()
                Dim ventaId = 0
                Do While resultado.Read()
                    ventaId = resultado.GetInt32(0)
                Loop

                conexion.Close()

                conexion.Open()

                For Each x As VentaItem In ventaInput.Items
                    comando = conexion.CreateCommand
                    comando.CommandText = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES ('" + ventaId.ToString + "','" + x.IDProducto.ToString + "','" + x.PrecioUnitario.ToString + "','" + x.Cantidad.ToString + "','" + x.SubTotal.ToString + "')"
                    comando.ExecuteNonQuery()
                Next

                conexion.Close()

                Dim venta = CrearVenta()
                Return View(venta)
            Else
                Dim venta = CrearVenta()
                Return View(venta)
            End If

        End Function

        <HttpPost>
        Function Edit(ventaIngresa As String) As ActionResult
            If (Not String.IsNullOrEmpty(ventaIngresa)) Then
                Dim ventaInput = JsonConvert.DeserializeObject(Of Venta)(ventaIngresa)

                Dim fechaAux = Convert.ToDateTime(ventaInput.Fecha).ToString("yyyy-MM-dd HH:mm:ss")
                ventaInput.Fecha = fechaAux
                Dim venta = New Venta()

                comando = conexion.CreateCommand
                comando.CommandText = "UPDATE ventas SET Total='" + ventaInput.Total.ToString + "' WHERE ID='" + ventaInput.ID.ToString + "'"

                conexion.Open()
                resultado = comando.ExecuteReader()
                conexion.Close()

                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, IDCliente, Fecha,Total FROM ventas where ID=" + ventaInput.ID.ToString

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    venta = New Venta With {
                    .ID = resultado.GetInt32(0),
                    .IDCliente = resultado.GetInt32(1),
                    .Fecha = resultado.GetDateTime(2).ToString("yyyy-MM-ddThh:mm"),
                    .Total = resultado.GetDouble(3).ToString
                }
                Loop
                conexion.Close()

                conexion.Open()

                For Each x As VentaItem In ventaInput.Items
                    comando = conexion.CreateCommand
                    comando.CommandText = "UPDATE ventasitems  SET PrecioUnitario ='" + x.PrecioUnitario.ToString + "', Cantidad ='" + x.Cantidad.ToString + "', PrecioTotal='" + x.SubTotal.ToString + "' WHERE IDProducto='" + x.IDProducto.ToString + "'"
                    comando.ExecuteNonQuery()
                Next

                conexion.Close()

                Dim clientes = New List(Of Cliente)()
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where eliminado=0 and ID=" + venta.IDCliente.ToString

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim cliente = New Cliente With {
                        .ID = resultado.GetInt32(0),
                        .Nombre = resultado.GetString(1),
                        .Telefono = resultado.GetString(2),
                        .Correo = resultado.GetString(3)
                    }
                    If resultado.GetInt32(0) = venta.IDCliente Then
                        cliente.Checked = True
                    End If
                    clientes.Add(cliente)
                Loop
                conexion.Close()


                conexion.Open()
                Dim listaItem = New List(Of VentaItem)
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT ID,IDVenta,IDProducto,PrecioUnitario,Cantidad,PrecioTotal FROM ventasitems where IDVenta=" + venta.ID.ToString

                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    Dim ventaItem = New VentaItem()
                    ventaItem.IDProducto = resultado.GetInt32(2)
                    ventaItem.PrecioUnitario = resultado.GetDouble(3).ToString
                    ventaItem.Cantidad = resultado.GetDouble(4).ToString
                    ventaItem.SubTotal = resultado.GetDouble(5).ToString
                    ventaItem.Producto = New Producto()

                    listaItem.Add(ventaItem)
                Loop
                conexion.Close()

                For Each x As VentaItem In listaItem
                    comando = conexion.CreateCommand
                    comando.CommandText = "SELECT Nombre FROM productos WHERE ID=" + x.IDProducto.ToString

                    conexion.Open()
                    resultado = comando.ExecuteReader()
                    Do While resultado.Read()
                        x.Producto.Nombre = resultado.GetString(0)
                    Loop
                    conexion.Close()
                Next


                ViewBag.Clientes = clientes
                ViewBag.Productos = listaItem

                Return View(venta)
            Else
                Dim venta = CrearVenta()
                Return View(venta)
            End If
        End Function


        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim venta = New Venta()

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, IDCliente, Fecha,Total FROM ventas where ID=" + id.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                venta = New Venta With {
                    .ID = resultado.GetInt32(0),
                    .IDCliente = resultado.GetInt32(1),
                    .Fecha = resultado.GetDateTime(2).ToString("yyyy-MM-ddThh:mm"),
                    .Total = resultado.GetDouble(3).ToString
                }
            Loop
            conexion.Close()

            Dim clientes = New List(Of Cliente)()
            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where eliminado=0 and ID=" + venta.IDCliente.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim cliente = New Cliente With {
                        .ID = resultado.GetInt32(0),
                        .Nombre = resultado.GetString(1),
                        .Telefono = resultado.GetString(2),
                        .Correo = resultado.GetString(3)
                    }
                If resultado.GetInt32(0) = venta.IDCliente Then
                    cliente.Checked = True
                End If
                clientes.Add(cliente)
            Loop
            conexion.Close()

            Dim listaItem = New List(Of VentaItem)
            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID,IDVenta,IDProducto,PrecioUnitario,Cantidad,PrecioTotal FROM ventasitems where IDVenta=" + venta.ID.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                Dim ventaItem = New VentaItem()
                ventaItem.IDProducto = resultado.GetInt32(2)
                ventaItem.PrecioUnitario = resultado.GetDouble(3).ToString
                ventaItem.Cantidad = resultado.GetDouble(4).ToString
                ventaItem.SubTotal = resultado.GetDouble(5).ToString
                ventaItem.Producto = New Producto()

                listaItem.Add(ventaItem)
            Loop
            conexion.Close()

            For Each x As VentaItem In listaItem
                comando = conexion.CreateCommand
                comando.CommandText = "SELECT Nombre FROM productos WHERE ID=" + x.IDProducto.ToString

                conexion.Open()
                resultado = comando.ExecuteReader()
                Do While resultado.Read()
                    x.Producto.Nombre = resultado.GetString(0)
                Loop
                conexion.Close()
            Next


            ViewBag.Clientes = clientes
            ViewBag.Productos = listaItem
            Return View(venta)
        End Function

        ' GET: Ventas/Delete/5
        Function Delete(ventas As Venta) As ActionResult
            If IsNothing(ventas.ID) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, IDCliente, Fecha, Total FROM Ventas where ID=" + ventas.ID.ToString

            conexion.Open()
            resultado = comando.ExecuteReader()
            Dim ventaEliminar = New Venta()
            Do While resultado.Read()
                ventaEliminar.ID = resultado.GetInt32(0)
                ventaEliminar.IDCliente = resultado.GetInt32(1)
                ventaEliminar.Fecha = resultado.GetDateTime(2).ToString
                ventaEliminar.Total = resultado.GetDouble(3).ToString
            Loop
            conexion.Close()

            comando = conexion.CreateCommand
            comando.CommandText = "SELECT ID, Cliente, Telefono, Correo FROM Clientes where eliminado=0 and ID=" + ventaEliminar.IDCliente.ToString

            Dim cliente = New Cliente()
            conexion.Open()
            resultado = comando.ExecuteReader()
            Do While resultado.Read()
                cliente.ID = resultado.GetInt32(0)
                cliente.Nombre = resultado.GetString(1)
                cliente.Telefono = resultado.GetString(2)
                cliente.Correo = resultado.GetString(3)
            Loop
            conexion.Close()
            ventaEliminar.Cliente = cliente

            If IsNothing(ventaEliminar) Then
                Return HttpNotFound()
            End If
            Return View(ventaEliminar)
        End Function

        ' POST: Ventas/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            comando = conexion.CreateCommand
            comando.CommandText = "Update dbo.Ventas SET eliminado=1 WHERE ID=" + id.ToString
            conexion.Open()
            comando.ExecuteNonQuery()
            conexion.Close()
            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace
