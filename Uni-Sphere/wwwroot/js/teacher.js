var dataTableTeacher;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTableTeacher = $('#tblDataTeacher').DataTable({
        "ajax": {
            "url": "/admin/adminteacher/getall",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id" },
            { "data": "fullName" },
            { "data": "gender" },
            { "data": "email" },
            { "data": "phoneNo" },
            {
                "data": "dateOfBirth",
                "render": function (data) {
                    return data.split('T')[0]; // Date comes in ISO format
                }
            },
            { "data": "department.name" },
            { "data": "designation" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/adminteacher/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            &nbsp;
                            <a onClick=Delete('/admin/adminteacher/Delete/${data}') class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
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
                        dataTableTeacher.ajax.reload();
                    } else {
                        toastr.error(data.message || "Unknown error occurred");
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error('Server Error: ' + error);
                }
            });
        }
    });
}
