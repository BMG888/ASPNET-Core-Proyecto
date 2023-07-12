var dataTable;

$(document).ready(function () {
    CargarDataTable();
});

function CargarDataTable() {
    dataTable = $('#CargarDT').DataTable({
        "ajax": {
            "url": "/libros/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "autor", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Libros/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:80px;'>
                            Editar
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:80px;'
                            onclick=Delete('/Libros/Delete?id='+${data})>
                            Eliminar
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No se encontraron datos"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro?",
        text: "Una vez eliminada la información, no podrá ser recuperada.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
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
        }
    });
}