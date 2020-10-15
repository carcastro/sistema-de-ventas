Imports System.ComponentModel.DataAnnotations

Public Class Cliente
    Public Property ID As Integer
    Public Property Checked As Boolean

    <Required(ErrorMessage:="Nombre cliente es requerido")>
    Public Property Nombre As String

    <Required(ErrorMessage:="Telefono es requerido")>
    Public Property Telefono As String

    <Required(ErrorMessage:="Correo es requerido")>
    Public Property Correo As String
End Class
