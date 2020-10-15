@ModelType pruebademo.producto
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Eliminar</h2>

<h3>¿Estás seguro de eliminar este producto?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Nombre)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Nombre)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Precio)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Precio)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Categoria)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Categoria)
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
