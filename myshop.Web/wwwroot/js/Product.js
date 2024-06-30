﻿var dtble;
$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "filter": true,
        "ajax": {
            "url": "/Admin/Product/GetData"
        },
        "type": "GET",
        "datatype": "json",
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "createDate" },
            { "data": "tbCatagory.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Product/Update/${data}" class="btn btn-success">Update</a>`;
                },
                "orderable": false
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a onClick=DeleteItem("/Admin/Product/DeleteProduct/${data}") class="btn btn-danger">Delete</a>`;
                },
                "orderable": false
            }
        ]
    });
}

function DeleteItem(url) {
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
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dtble.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your product has been deleted.",
                icon: "success"
            });
        }
    });
}
