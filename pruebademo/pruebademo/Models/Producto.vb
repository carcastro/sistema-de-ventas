Imports System.ComponentModel.DataAnnotations

Public Class Producto
    Public Property ID As Integer
    Public Property Checked As Boolean

    <Required(ErrorMessage:="Nombre producto es requerido")>
    Public Property Nombre As String

    <Required(ErrorMessage:="Precio producto es requerido")>
    <DataType(DataType.Currency)>
    Public Property Precio As Decimal
    <Required(ErrorMessage:="Categoria es requerido")>
    Public Property Categoria As String
End Class
