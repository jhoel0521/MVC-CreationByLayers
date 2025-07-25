﻿var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    console.log("cargarDatatable");
    dataTable = $("#tblAlmacenes").DataTable({
        "ajax": {
            "url": "/admin/almacenes/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            {
                "data": "urlImagen",
                "render": function (data, type, row) {
                    return `<img src="${data}" alt="Almacén ${row.nombreAlmacen}" width="100" class="img-thumbnail" />`;
                },
                "width": "20%"
            },
            { "data": "nombreAlmacen", "width": "40%" },
            { "data": "direccion", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group">
                        <a href="/Admin/Almacenes/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                        <i class="far fa-edit"></i> Editar
                        </a>
                        <a onclick=Delete("/Admin/Almacenes/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                        <i class="far fa-trash-alt"></i> Borrar
                        </a>
                    </div>
                    `;
                }, "width": "40%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas",
            "infoPostFix": "",
            "thousands": ",",
            "lenghtMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "width": "100%"
        }
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,

            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}