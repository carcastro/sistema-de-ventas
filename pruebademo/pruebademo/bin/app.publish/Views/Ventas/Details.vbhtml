@ModelType pruebademo.Venta
@Code
    ViewData("Title") = "Detalles Venta"
End Code

<h2>Detalles venta</h2>

<h3>Cliente</h3>


<table class="table">
    <tr>
        <th>
            @Html.Label("Nombre")
        </th>
        <th>
            @Html.Label("Telefono")
        </th>
        <th>
            @Html.Label("Correo")
        </th>
    </tr>

    <tr>
        <td>
            @Html.DisplayFor(Function(model) model.Cliente.Nombre)
        </td>
        <td>
            @Html.DisplayFor(Function(model) model.Cliente.Telefono)
        </td>
        <td>
            @Html.DisplayFor(Function(model) model.Cliente.Correo)
        </td>
    </tr>

</table>

<h3>Items venta</h3>
<table class="table">
    <tr>
        <th>
            @Html.Label("Producto")
        </th>
        <th>
            @Html.Label("Precio Unitario")
        </th>
        <th>
            @Html.Label("Cantidad")
        </th>
        <th>
            @Html.Label("SubTotal")
        </th>
    </tr>

    @For Each item In Model.Items
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Producto.Nombre)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.PrecioUnitario)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Cantidad)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.SubTotal)
            </td>
        </tr>
    Next
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
            @Html.Label("TOTAL GENERAL")
        </td>
        <td>
            @Html.DisplayFor(Function(model) model.Total)
        </td>
    </tr>

</table>