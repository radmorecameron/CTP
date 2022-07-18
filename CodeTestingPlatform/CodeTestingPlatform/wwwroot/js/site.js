// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    $(function () {
        $("[rel='tooltip']").tooltip({
            placement: "right",
            html: true
        });
    });
    

function show_hide_row(row, row_hide_id, btn_id, method_count) {

    // Toggle the arrow
    const btn = document.getElementById(btn_id);
    if (btn.innerHTML.search("arrow-circle-right") != -1) {
        btn.innerHTML = "<i class='fas fa-arrow-circle-down'></i>"
    } else {
        btn.innerHTML = "<i class='fas fa-arrow-circle-right'></i>"
    }
    // Set border on selected item
    if (document.getElementById(row_hide_id).classList.length > 0) {
        if (document.getElementById(row_hide_id).classList.contains("selected-activty")) {
            $("#" + row_hide_id).removeClass("selected-activty");
        } else {
            $("#" + row_hide_id).addClass("selected-activty");
        }
    } else {
        $("#" + row_hide_id).addClass("selected-activty");
    }

    // Display hidden row
    $("." + row).toggle();

    // Close any additional opened items (if you clode an activity, all the method signatures and test cases should collpase too) and reset arrows
    var activity_row_number = row.substring(0, row.indexOf('_'))

    for (let i = 1; i <= method_count; i++) {
        if ($("." + activity_row_number + "_" + i + "_testcase_row").is(":visible")) {
            $("." + activity_row_number + "_" + i + "_testcase_row").toggle()
        }

        console.log(document.getElementById(activity_row_number + "_" + i + "_method_btn").innerHTML)
        document.getElementById(activity_row_number + "_" + i + "_method_btn").innerHTML = "<i class='fas fa-arrow-circle-right' ></i>"

        if (document.getElementById(activity_row_number + "_" + i + "_method_row").classList.contains("selected-activty")) {
            $("#" + activity_row_number + "_" + i + "_method_row").removeClass("selected-activty");
        }
    }

}