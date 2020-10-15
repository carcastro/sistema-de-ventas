@ModelType IEnumerable(Of pruebademo.Producto)
@Code
    ViewData("Title") = "Index"
End Code

<p>
    @Html.ActionLink("Crear Producto", "Create")
</p>
@Using Html.BeginForm()
    @<p align="left">
    Buscar por Nombre: @Html.TextBox("busquedaPorNombre")
    <input type="submit" value="Busqueda" />

    Buscar por Categoria: @Html.TextBox("busquedaPorCategoria")
    <input type="submit" value="Busqueda" />
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
            @Html.DisplayNameFor(Function(model) model.Precio)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Categoria)
        </th>
        <th>
            Accion
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
                @Html.DisplayFor(Function(modelItem) item.Precio)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Categoria)
            </td>
            <td>
                @Html.ActionLink("Editar", "Edit", New With {.id = item.ID}) |
                @Html.ActionLink("Eliminar", "Delete", New With {.id = item.ID})
            </td>
        </tr>
    Next

</table>
<!-- Modal -->
<div class="modal fade" id="modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="titulo">Informacion</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body" id="bodymodal">
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Aceptar</button>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function deleteRow() {
        if ($(':checkbox:checked')) {
            var checkboxes = $('input[type="checkbox"]')
            var count = 0
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked)
                    count =+1
            }
            console.log(count)
            if (count>0) {
                var postData = $('input[type="checkbox"]').serialize();
                $.post('/Productos/DeleteGroup', postData,
                    function () {
                        $(':checkbox:checked').each(function () {
                            if (this.checked) {
                                $(this).closest('tr').detach();
                            }
                        });
                    });
            } else {
                var bodymodal = document.getElementById("bodymodal")
                bodymodal.innerHTML = ''
                var msj = document.createTextNode("Debe seleccionar un producto a eliminar");
                $(".modal-body").append(msj)
                $('#modal').modal('show') 
            }
        }
    };
</script>
