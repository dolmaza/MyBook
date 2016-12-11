$(function () {
    $(".save").click(function () {
        var roleID = RolesGrid.GetRowKey(RolesGrid.GetFocusedRowIndex());
        var permissions = PermissionsTree.GetVisibleSelectedNodeKeys();
        console.log(roleID,permissions);
        $.ajax({
            type: "POST",
            url: updateRolePermissionsUrl,
            data: {
                RoleID: roleID,
                Permissions: permissions
            },
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess) {
                    alert("Success");
                } else {
                    alert("Error");
                }
            }
        });

        return false;
    });
});

function OnGridViewFocusedRowChanged(s, e) {
    OnGetRowKey(RolesGrid.GetRowKey(RolesGrid.GetFocusedRowIndex()));
}

function OnGetRowKey(ID) {
    $.ajax({
        type: "POST",
        url: getRolePermissionsUrl,
        data: {
            ID: ID
        },
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess) {
                var nodes = PermissionsTree.GetVisibleNodeKeys();
                var newPermissions = JSON.parse(response.Data.Permissions);

                $.each(nodes, function (index, node) {
                    PermissionsTree.SelectNode(node, false);
                });

                $.each(newPermissions, function (index, permission) {
                    PermissionsTree.SelectNode(permission, true);
                });

            } else {
                alert("Error");
            }
        },
        error: function() {
            alert("Error");
        }
    });
}