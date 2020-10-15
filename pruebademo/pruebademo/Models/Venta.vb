Public Class Venta
    Public Property ID As Integer
    Public Property IDCliente As Integer
    Public Property Cliente As Cliente
    Public Property Fecha As String
    Public Property Total As Decimal
    Public Property Items As List(Of VentaItem)
End Class