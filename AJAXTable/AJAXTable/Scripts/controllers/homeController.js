var homeConfig = {
    pageSize: 5,
    pageIndex: 1
}

var homeController = {
    init: function () {
        homeController.loadData();
        homeController.registerEvent();
    },
    registerEvent: function () {
        // Update Salary
        $('.txtSalary').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var id = $(this).data('id');
                var value = $(this).val();

                homeController.updateSalary(id, value);
            }
        });

        // Show modal add new Employee
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            homeController.resetForm();
        });

        // Save Employee
        $('#btnSave').off('click').on('click', function () {
            homeController.saveData();
        });

        $('.btn-edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            homeController.loadDetail(id);
        });

        // Delete Employee
        $('.btn-delete').off('click').on('click', function () {
            console.log("1");
            var id = $(this).data('id');
            bootbox.confirm("Are you sure to delete this employee?", function (result) {
                if (result) {
                    homeController.deleteEmployee(id);
                }
            });
        });

        //Search Employee
        $('#btnSearch').off('click').on('click', function () {
            homeController.loadData(true);
        });

        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                console.log("1");
                homeController.loadData(true);
            }
        });

        $('#btnReset').off('click').on('click', function () {
            console.log("1");
            $('#txtNameS').val(" ");
            $('#ddlStatusS').val("All");
            homeController.loadData(true);
        })

    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var data = res.data;

                    $('#hidID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtSalary').val(data.Salary);
                    $('#ckStatus').prop('checked', data.Status);
                }
                else {
                    bootbox.alert(res.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function (changePageSize) {
        var name = $('#txtNameS').val();
        var status = $('#ddlStatusS').val();

        $.ajax({    
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                name: name,
                status: status,
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Salary: item.Salary,
                            Status: item.Status == true ? "<span class='label label-success'>Active</span>" : "<span class='label label-danger'>Locked</span>"
                        })
                    });

                    $('#tblData').html(html);

                    homeController.pagination(response.total, function () {
                        homeController.loadData();
                    }, changePageSize);

                    homeController.registerEvent();
                }
            }
        })
    },
    saveData: function () {
        var name = $('#txtName').val();
        var salary = parseFloat($('#txtSalary').val());
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidID').val());

        var employee = {
            Name: name,
            Salary: salary,
            Status: status,
            ID: id
        }

        $.ajax({
            url: '/Home/SaveData',
            data: {
                strEmployee: JSON.stringify(employee)
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    bootbox.alert("Save success", function () {
                        $('#modalAddUpdate').modal('hide');
                        homeController.loadData(true);
                    });
                }
                else {
                    bootbox.alert(res.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('#txtSalary').val(0);
        $('#ckStatus').prop('checked', true);
    },
    updateSalary: function (id, value) {
        var data = {
            ID: id,
            Salary: value
        };

        $.ajax({
            url: 'Home/Update',
            type: 'POST',
            dataType: "json",
            data: {
                model: JSON.stringify(data)
            },
            success: function (res) {
                if (res.status) {
                    alert("Update Success");
                }
                else {
                    bootbox.alert("Update failed");
                }
            }
        })
    },
    deleteEmployee: function (id) {
        $.ajax({
            url: 'Home/Delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    bootbox.alert("Delete success", function () {
                        homeController.loadData(true);
                    });
                }
            }
        });
    },
    pagination: function (totalRow, callback, changePageSize) {
        var totalPages = Math.ceil(totalRow / homeConfig.pageSize);

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPages,
            visiblePages: 10,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            pre: "Trước",
            onPageClick: function (event, page) {
                homeConfig.pageIndex = page;
                setTimeout(callback, 200)
            }
        });
    }
};

homeController.init();