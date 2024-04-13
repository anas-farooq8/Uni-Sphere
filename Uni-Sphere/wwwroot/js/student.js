var dataTableStudent;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTableStudent = $('#tblDataStudent').DataTable({
        "ajax": {
            "url": "/admin/adminstudent/getall",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id" },
            { "data": "fullName"},
            { "data": "rollNo" },
            { "data": "gender" },
            { "data": "email"},
            { "data": "phoneNo" },
            { "data": "section" },
            { "data": "degree" },
            { "data": "department.name", },
            { "data": "batch" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/adminstudent/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            &nbsp;
                            <a onClick=Delete('/admin/adminstudent/Delete/${data}') class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
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
                        toastr.success(data.message);
                        dataTableStudent.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}