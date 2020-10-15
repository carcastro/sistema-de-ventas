@ModelType IEnumerable(Of pruebademo.Venta)
@Code
    ViewData("Title") = "Ventas"
End Code

<h3>Ventas</h3>

<p>
    @Html.ActionLink("Crear Venta", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Fecha)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Total)
        </th>
        <th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Fecha)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Total)
        </td>
        <td>
            @Html.ActionLink("Editar", "Edit", New With {.id = item.ID}) |
            @Html.ActionLink("Detalles", "Details", New With {.id = item.ID}) |
            @Html.ActionLink("Eliminar", "Delete", New With {.id = item.ID})
        </td>
    </tr>
Next

</table>
