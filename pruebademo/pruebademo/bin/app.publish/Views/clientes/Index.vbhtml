@ModelType IEnumerable(Of pruebademo.cliente)
@Code
    ViewData("Title") = "Index"
    Dim orden = ""
    ViewBag.OrdenNombre = "PorNombre"
End Code

<p>
    @Html.ActionLink("Crear Cliente", "Create")
</p>
@Using Html.BeginForm()
    @<p align="left">
        Buscar por Nombre: @Html.TextBox("busquedaNombre")
        <input type="submit" value="Buscar" />

        Buscar por Correo: @Html.TextBox("busquedaCorreo")
        <input type="submit" value="Buscar" />
    </p>
End Using
<p>
    <input type="button" onclick="deleteRow()" value="Eliminar seleccionados" />
</p>
<table class="table">
    <tr>
        <th>
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Nombre)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Telefono)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Correo)
        </th>
        <th>
        </th>
    </tr>

    @For Each item In Model
        @<tr>
            <th>
                 <input type='checkbox' id='myCheckbox' name='ID' value='@item.ID' />
            </th>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Nombre)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Telefono)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Correo)
            </td>
            <td>
                @Html.ActionLink("Editar", "Edit", New With {item.ID}) |
                @Html.ActionLink("Eliminar", "Delete", New With {item.ID})
            </td>
        </tr>
    Next

</table>
<script type="text/javascript">
    var count = 0;
    function deleteRow() {
        if ($(':checkbox:checked')) {
            var postData = $('input[type="checkbox"]').serialize();
            console.log(postData)
            $.post('/Clientes/DeleteGroup', postData,
                function () {
                    $(':checkbox:checked').each(function () {
                        if (this.checked) {
                            $(this).closest('tr').detach();
                        }
                    });
                });
        }
    };
    function Ordenar(tipo) {
        var envio = {
            "orden" : tipo
        }        
        count = count + 1;
        var resto = (count % 2)        
        if (resto === 0) {
            envio.orden = "desc"            
        }

        console.log(envio)
        $.post('/Clientes/', envio,
            function () {

            });
    }
</script>



