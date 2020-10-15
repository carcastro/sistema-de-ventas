@ModelType pruebademo.Venta
@Code
    ViewData("Title") = "Editar"
End Code


<h2>Editar</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Venta</h4>
        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    @Html.Label("Fecha")
                    <input type="datetime-local" id="fechaVenta" onchange="ActualizarFecha()" value='' />
                    @Html.EditorFor(Function(model) model.Fecha, New With {.htmlAttributes = New With {.class = "hidden"}})

                </div>
                <div class="col-sm-6">
                    <input class="btn btn-default" type="button" value="Guardar" onclick="EnviarVenta()" />

                </div>

            </div>
        </div>
        <br />

        <div class="container">

            <div class="row">
                <div class="col-sm-6">
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

                        @For Each item As Cliente In ViewBag.Clientes
                            @<tr>
                                <td>
                                    @Html.DisplayFor(Function(modelItem) item.Nombre)
                                </td>
                                <td>
                                    @Html.DisplayFor(Function(modelItem) item.Telefono)
                                </td>
                                <td>
                                    @Html.DisplayFor(Function(modelItem) item.Correo)
                                </td>
                            </tr>
                        Next

                    </table>
                </div>
                <div class="col-sm-6">
                    <table class="table" style="width: 100%">
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

                        @For Each item As VentaItem In ViewBag.Productos
                            @<tr>
                                <td>
                                    @Html.DisplayFor(Function(modelItem) item.Producto.Nombre)
                                </td>
                                <td>
                                    <input type="text" disabled="disabled" id=@($"{item.IDProducto}PrecioUnitario") value="@item.PrecioUnitario" />
                                </td>
                                <td>
                                    <input type="number" id=@($"{item.IDProducto}Cantidad") name="cantidad" onblur="CalcularSubTotal(value, @item.IDProducto)" value='@item.Cantidad' min="0" />
                                </td>
                                <td>
                                    <input type="text" id=@($"{item.IDProducto}SubTotal") name="subtotal" value='@item.SubTotal' contenteditable="false" />
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
                                @Html.EditorFor(Function(model) model.Total, New With {.htmlAttributes = New With {.class = "hidden"}})

                                <input type="text" disabled="disabled" id="totalVenta" value="0" />
                                @Html.HiddenFor(Function(model) model.ID)

                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </div>  End Using

<div>
    @Html.ActionLink("Volver a la lista", "Index")
</div>
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
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

<style>
    .table {
        display: table;
        border-collapse: collapse;
    }

        .table .table-row {
            display: table-row;
        }

        .table .table-cell {
            display: table-cell;
            text-align: left;
            vertical-align: top;
            border: 1px solid black;
        }
</style>

<script>
    var listaItems = []
    var venta = {
        "ID":0,
        "IDCliente": 0,
        "Fecha": '',
        "Total": 0,
        "Items": listaItems
    }

    var fecha = document.getElementById("fechaVenta")
    var fechaHidden = document.getElementById("Fecha")
    fecha.value = fechaHidden.value
    venta.Fecha = fechaHidden.value
    console.log(venta)


    var total = document.getElementById("totalVenta")
    var totalIngresado = document.getElementById("Total")
    total.value = totalIngresado.value

    function ActualizarFecha() {
        var fecha = document.getElementById("fechaVenta")
        venta.Fecha = fecha.value
    }

    function AgregarCliente(id) {
        var radios = document.getElementsByName("radioCliente");
        for (var i = 0; i < radios.length; i++) {

            if (radios[i].value == id) {
                console.log(id)
                venta.IDCliente = id
                radios[i].checked = true
            } else {
                radios[i].checked = false
            }
        }

    }

    function LimparData() {
        var radios = document.getElementsByName("radioCliente");
        for (var i = 0; i < radios.length; i++) {
            radios[i].checked = false
        }

        var cantidades = document.getElementsByName("cantidad");
        for (var i = 0; i < cantidades.length; i++) {
            cantidades[i].value = 0
        }

        totalVenta.value = 0

        var subtotal = document.getElementsByName("subtotal");
        for (var i = 0; i < subtotal.length; i++) {
            subtotal[i].value = 0
        }

        var fecha = document.getElementById("fechaVenta")
        var fechaMostrar = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0]
        fecha.value = fechaMostrar
        venta.Fecha = fechaMostrar

        var msj = document.createTextNode("Venta Editada Exitosamente!");
        $(".modal-body").append(msj)
        $('#modal').modal('show')

    }

    function CalcularSubTotal(cantidad, id) {

        //console.log(cantidad, id)
        document.getElementById('totalVenta').value = 0
        var totalVenta = document.getElementById('totalVenta')

        var precioUnitario = document.getElementById(id + 'PrecioUnitario')
        var cant = document.getElementById(id + 'Cantidad')
        var precioSubTotal = document.getElementById(id + 'SubTotal')
        var total = 0;

        if (cantidad != "") {
            precioSubTotal.value = parseFloat(precioUnitario.value) * parseFloat(cantidad);
        } else {
            cant.value = 0
        }

        if (precioSubTotal.value > 0) {
            var item = {
                "IDProducto": id,
                "PrecioUnitario": precioUnitario.value,
                "Cantidad": cantidad,
                "SubTotal": precioSubTotal.value
            }
            listaItems.push(item)
        }

        total = parseFloat(totalVenta.value) + parseFloat(precioSubTotal.value);
        venta.Total = total;

        totalVenta.value = total


    }

    function EnviarVenta() {
        var error = 0;
        var bodymodal = document.getElementById("bodymodal")
        bodymodal.innerHTML = ''

        if (venta.Items.length == 0) {
            var msj = document.createTextNode("Debe agregar un producto");
            $(".modal-body").append("<br/>")
            $(".modal-body").append(msj)
            $('#modal').modal('show')
            error = + 1
        }

        if (error === 0) {
            {
                var ventaId = document.getElementById("ID")
                venta.ID = ventaId.value
                var postData = JSON.stringify(venta);
                console.log(postData)
                $.ajax({
                    type: 'POST',
                    dataType: 'text',
                    url: "/Ventas/Edit",
                    data: "ventaIngresa=" + postData,
                    success: function () {

                        //LimparData()
                        var msj = document.createTextNode("Venta Editada Exitosamente!");
                        $(".modal-body").append(msj)
                        $('#modal').modal('show')

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console && console.log("request failed");
                    }
                });
            }
        }
    };
</script>
