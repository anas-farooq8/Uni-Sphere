var dataTableCourse;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTableCourse = $('#tblDataCourse').DataTable({
        "ajax": {
            "url": "/admin/admincourse/getall",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "code" },
            { "data": "creditHours" },
            { "data": "courseType" },
            { "data": "isLab" },
            { "data": "description" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/admincourse/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            &nbsp;
                            <a onClick=Delete('/admin/admincourse/Delete/${data}') class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
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
                        dataTableCourse.ajax.reload();
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