$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblDataStudent').DataTable({
        "ajax": {
            "url": "/admin/adminstudent/getall",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id" },
            { "data": "fullName"},
            { "data": "rollNo" },
            {
                "data": "gender", "render": function (data) {
                    return data === 0 ? 'Male' : 'Female';  // 0 is Male, 1 is Female
                }
            },
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
                            <a href="/admin/adminstudent/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
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
