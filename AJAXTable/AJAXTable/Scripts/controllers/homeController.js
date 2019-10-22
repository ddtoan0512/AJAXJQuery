var homeConfig = {
    pageSize: 20,
    pageIndex: 1
}

var homeController = {
    init: function () {
        homeController.loadData();
        homeController.registerEvent();
    },
    registerEvent: function () {

        $('.txtSalary').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var id = $(this).data('id');
                var value = $(this).val();

                homeController.updateSalary(id, value);
            }
        });

        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            homeController.resetForm();
        });

        $('#btnSave').off('click').on('click', function () {
            homeController.saveData();
        });
    },
    loadData: function () {
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
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
                    });

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
                    alert('Save success!');
                    $('#modalAddUpdate').modal('hide');
                    homeController.loadData();
                }
                else {
                    alert(res.Message);
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
                    alert('Update successed. ');
                }
                else {
                    alert('Update failed. ');
                }
            }
        })
    },
    pagination: function (totalRow, callback) {
        var totalPages = Math.ceil(totalRow / homeConfig.pageSize);

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