$(document).ready(function ()
{
    $("#customer-table").DataTable(
        {
            // To show progress bar
            "processing": true,

            // To process server side
            "serverSide": true,

            // To disable filter (search box )
            "filter": true,
            
            // To disable multiple column at once
            "orderMulti": false,

            // AJAX request
            "ajax":
            {
                "url": "/Home/LoadData",
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs":
            [{
                // Hiding Customer Id
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
            "columns":
            [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "Address", "name": "Address", "autoWidth": true },
                { "data": "Country", "name": "Country", "autoWidth": true },
                { "data": "City", "name": "City", "autoWidth": true },
                { "data": "PhoneNum", "name": "Phone Number", "autoWidth": true },
                {
                    "render": function(data, type, full, meta)
                    {
                        return "<a class=\"btn btn-info\" href=\"/Home/Edit/" +
                                full.CustomerID + "\">Edit</a>";
                    }
                },
                {
                    data: null, render: function(data, type, row)
                    {
                        return "<a href='#' class='btn btn-danger' onclick=DeleteData('" +
                                row.CustomerID + "'); >Delete</a>";
                    }
                },
            ]
        }
    );
});

function DeleteData(CustomerID)
{
    if (confirm("Are you sure you want to delete ...?"))
        Delete(CustomerID);
    else
        return false;
}


function Delete(CustomerID)
{
    var url = "@@Url.Content(\"~/\")" + "Home/Delete";

    $.post(url, { ID: CustomerID }, function(data)
    {
        if (data)
        {
            oTable = $("#customer-table").DataTable();
            oTable.draw();
        }
        else
            alert("Something Went Wrong!");
    });
}