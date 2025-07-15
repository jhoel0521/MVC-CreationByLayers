var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblProductos").DataTable({
        "ajax": {
            "url": "/admin/productos/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            {
                "data": "urlImagen",
                "render": function (data, type, row) {
                    if (data)
                        return `<img src="${data}" alt="Producto ${row.nombre}" width="100" class="img-thumbnail" />`;
                    else
                        return `<img src="/images/no-image.png" alt="Sin imagen" width="100" class="img-thumbnail" />`;
                },
                "width": "15%"
            },
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "20%" },
            { "data": "precio", "width": "10%" },
            { "data": "stock", "width": "10%" },
            { "data": "almacen.nombreAlmacen", "width": "10%" }, // nombre almacén de propiedad navegacional
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group">
                        <a href="/Admin/Productos/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                            <i class="far fa-edit"></i> Editar
                        </a>
                        <a onclick=Delete("/Admin/Productos/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                            <i class="far fa-trash-alt"></i> Borrar
                        </a>
                    </div>`;
                },
                "width": "10%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 Entradas",
            "infoFiltered": "(filtrado de _MAX_ total de entradas)",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron resultados",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de borrar?",
        text: "Este contenido no se puede recuperar.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
