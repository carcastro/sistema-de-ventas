@ModelType pruebademo.Cliente
@Code
    ViewData("Title") = "Delete"
End Code

<h3>Eliminar</h3>

<h3>Estàs seguro de eliminar el cliente?</h3>
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
            @Html.DisplayNameFor(Function(model) model.Telefono)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Telefono)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Correo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Correo)
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
