@ModelType pruebademo.Venta
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Eliminar</h2>

<h3>¿Estás seguro de eliminar esta venta?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.Label("Cliente")
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Cliente.Nombre)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Fecha)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Fecha)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Total)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Total)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Eliminar" class="btn btn-default" /> |
            @Html.ActionLink("Volver a la lista", "Index")
        </div>
    End Using
</div>
