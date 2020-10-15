@ModelType pruebademo.cliente
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>cliente</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Cliente1)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Cliente1)
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
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
