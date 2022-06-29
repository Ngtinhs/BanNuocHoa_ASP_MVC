$(".in-de-crement").on("click", function () {

    var $button = $(this);
    var $input = $button.parent().find(".input-crement");
    var oldValue = $input.text();

    if ($button.text() == "+") {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        // Don't allow decrementing below zero
        if (oldValue > 0) {
            var newVal = parseFloat(oldValue) - 1;
        } else {
            newVal = 0;
        }
    }

    $input.text(newVal);

});

$("input.mx-2.checked-box").click((e) => {
    var list = $("input.mx-2.checked-box:checked").map((index, item) => {
        return $(item).attr("name");
    }).get();

    var temp = short = "";
    var array = [];
    list.map((item, index) => {
        var split = item.split('_');
        if (split[1] != temp) {
            if (temp != "")
                array.push(short);
            temp = split[1];
            short = temp;
        }
        short += "_" + split[0];
        if (index == list.length - 1)
            array.push(short);
    });

    list = temp = short = null;

    var ajax = {
        type: "POST",
        url: "DanhSach",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        success: (res) => $("#DanhSachSanPhamPartial").html(res),
        error: () => alert("Error")
    };

    if (array.length == 0) {
        array.push("000");
        ajax.data = JSON.stringify({ array: array });
        $.ajax(ajax);
        return;
    }

    ajax.data = JSON.stringify({ array: array });
    $.ajax(ajax);
});

$(".menu-admin").css({ height: "calc(100% - " + $(".bg-nav-admin").height() + "px" });

$("#input_search").on("input", function() {
    var text = $(this).val();
    console.log(text);

    var ajax = {
        type: "POST",
        url: "/SanPham/ThanhLoc",
        //url: '@Url.Action("ThanhLoc", "SanPham")',
        data: "{ text : '" + text + "' }",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        success: (res) => $("#search_bar").html(res),
        error: () => console.log("Error")
    };

    $.ajax(ajax);
});
