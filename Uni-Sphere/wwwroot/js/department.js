var dataTableDepartment;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTableDepartment = $('#tblDataDepartment').DataTable({
        "ajax": {
            "url": "/admin/admindepartment/getall",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "20%" },
            { "data": "code", "width": "15%" },
            { "data": "description", "width": "40%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/admindepartment/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            &nbsp;
                            <a onClick=Delete('/admin/admindepartment/Delete/${data}') class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-trash-alt"></i> Delete
                            </a>
                        </div>
                        `;
                },
                width: "15%"
            }
        ],
        "language": {
            "emptyTable": "No data available in table"
        }
    });
}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        dataTableDepartment.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}