$(document).ready(function () {
    $("#FileUpload").change(function () {
        var str = "";
        $("select option:selected").each(function () {
            if ($(this).text() === "Folder Upload") {
                $("#file").attr("webkitdirectory", true);
                $("#file").attr("mozdirectory", true);

            }
            if ($(this).text() === "Multiple File Upload") {
                $("#file").removeAttr("webkitdirectory");
                $("#file").removeAttr("mozdirectory");
                $("#file").attr("multiple", true);
            }
            if ($(this).text() === "File Upload") {
                $("#file").removeAttr("webkitdirectory");
                $("#file").removeAttr("mozdirectory");
                $("#file").removeAttr("multiple");
            }
        });
    });
});

$(document).ready(function () {
    $('.filterable .btn-filter').click(function () {
        var $panel = $(this).parents('.filterable'),
            $filters = $panel.find('.filters input'),
            $tbody = $panel.find('.table tbody');
        if ($filters.prop('disabled') === true) {
            $filters.prop('disabled', false);
            $filters.first().focus();
        } else {
            $filters.val('').prop('disabled', true);
            $tbody.find('.no-result').remove();
            $tbody.find('tr').show();
        }
    });
    //https://bootsnipp.com/snippets/8G2Q

    $('.filterable .filters input').keyup(function (e) {
        /* Ignore tab key */
        var code = e.keyCode || e.which;
        if (code === '9') return;
        /* Useful DOM data and selectors */
        var $input = $(this),
            inputContent = $input.val().toLowerCase(),
            $panel = $input.parents('.filterable'),
            column = $panel.find('.filters th').index($input.parents('th')),
            $table = $panel.find('.table'),
            $rows = $table.find('tbody tr');
        /* Dirtiest filter function ever ;) */
        var $filteredRows = $rows.filter(function () {
            var value = $(this).find('td').eq(column).text().toLowerCase();
            return value.indexOf(inputContent) === -1;
        });
        /* Clean previous no-result if exist */
        $table.find('tbody .no-result').remove();
        /* Show all rows, hide filtered ones (never do that outside of a demo ! xD) */
        $rows.show();
        $filteredRows.hide();
        /* Prepend no-result row if all rows are filtered */
        if ($filteredRows.length === $rows.length) {
            $table.find('tbody').prepend($('<tr class="no-result text-center"><td colspan="' + $table.find('.filters th').length + '">No result found</td></tr>'));
        }
    });
});
$(document).ready(function () {
    $("#numberOfStudents").change(function () {
        var num = $("#numberOfStudents").val();
        var div = $("#DynamicEmail");
        var input="";
        for (var i = 1; i <= num; i++) {
             input+=
                "<div class="+"form-group"+"> " +
             "<label for=" + "User.EmailAddress(" + i + ")>EmailAddress" + i +"</label>" +
             "<input class='form-control' type='text' data-val='true' data-val-required='The EmailAddress field is required.' id='User.EmailAddress(" + i + ")' name='EmailAddress" + i + "' value='' />" +
             "<span class='text-danger field-validation-valid' data-valmsg-for='EmailAddress" + i +"' data-valmsg-replace='true'></span>" +
                "</div>"
                 ;
        }
        div.append(input);
    });
});
$(document).ready(function () {

    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#sidebarCollapse').on('click', function () {
        // open or close navbar
        $('#sidebar').toggleClass('active');
        // close dropdowns
        $('.collapse.in').toggleClass('in');
        // and also adjust aria-expanded attributes we use for the open/closed arrows
        // in our CSS
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});
